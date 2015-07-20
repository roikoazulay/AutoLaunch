using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class MotorControllerAction : ActionBase
    {
        private static int COMMAND_DELAY = 200; //Processing delay
        private ActionType _type;
        private ActionData _actionData;
        private Stopwatch _motorMoveTimer = new Stopwatch();
        private bool isControllerRestarts;
        private bool _gotLimit = false;//will become true if driver send LMT

        public enum ActionType
        {
            Connect,
            Disconnect,
            SetSpeed,
            SetSpeedOld,
            MoveForward,
            MoveBackward,
            // SetAcceleration,
        }

        public MotorControllerAction()
            : base(Enums.ActionTypeId.MotorController)
        {
        }

        public MotorControlObj GetMotorControl()
        {
            if (!Singleton.Instance<SavedData>().SerialCommunications.ContainsKey(_actionData.ComPort))
            {
                var controller = new MotorControlObj();
                controller.Port = Singleton.Instance<SavedData>().GetVariableData(_actionData.ComPort);
                Singleton.Instance<SavedData>().SerialCommunications.Add(_actionData.ComPort, controller);
            }

            return (MotorControlObj)Singleton.Instance<SavedData>().SerialCommunications[_actionData.ComPort];
        }

        public override void Execute()
        {
            isControllerRestarts = false;
            AutoApp.Logger.WriteInfoLog("Starting Motor Action " + _type.ToString());
            double length;
            var value = Singleton.Instance<SavedData>().GetVariableData(_actionData.Value);
            var motor = GetMotorControl();
            motor.ClearData();
            switch (_type)
            {
                case ActionType.Connect:
                    if (motor.Connect())
                    {
                        motor.SendCommand("!00MAL0100");//Define LOW SPEED
                        new System.Threading.ManualResetEvent(false).WaitOne(COMMAND_DELAY);
                        motor.SendCommand("!00MA10000");//Define Acceleration/Deceleration value
                        new System.Threading.ManualResetEvent(false).WaitOne(COMMAND_DELAY);
                        motor.SendCommand("!00MH10000");//define high speed to 10000, working always on low speed - Acceleration is ignored in low speed
                        motor.SendCommand("!00MR");//Set Relative move NOT Continuous!!!
                        new System.Threading.ManualResetEvent(false).WaitOne(COMMAND_DELAY);
                        motor.SendCommand("?000");//Get Driver Name , need in order to verify connection to the motor
                        new System.Threading.ManualResetEvent(false).WaitOne(COMMAND_DELAY);
                        if (VerifyAction("PMD-51031-R"))
                            ActionStatus = Enums.Status.Pass;
                        else
                            AutoApp.Logger.WriteFailLog("Driver connect failed , No response from driver");
                    }
                    break;

                case ActionType.Disconnect:
                    if (motor.DisConnect())
                        ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.SetSpeedOld:

                    if (motor.SendCommand("!00MH" + value.PadLeft(5, '0')))
                    {
                        if (VerifyAction("|HI", "!00MH" + value.PadLeft(5, '0')))
                            ActionStatus = Enums.Status.Pass;
                    }
                    break;

                case ActionType.SetSpeed:
                    //if the seep is lower or equal to 1000, set the motor speed as LOW speed using !aaMLddddd(cr)
                    //otherwise set the lower speed to 1000 and the high speed as the desired value (the acceleration should stay 10000)
                    motor.ClearData();
                    if (int.Parse(value) > 1000)
                    {
                        if (motor.SendCommand("!00MH" + value.PadLeft(5, '0')))
                        {
                            if (VerifyAction("|HI"))
                                ActionStatus = Enums.Status.Pass;
                        }
                    }
                    else
                    {
                        if (motor.SendCommand("!00ML" + value.PadLeft(5, '0'))) //Define LO speed value & set LO speed
                        {
                            if (VerifyAction("|LO"))
                                ActionStatus = Enums.Status.Pass;
                        }
                    }

                    break;

                case ActionType.MoveBackward:
                    length = double.Parse(value);// * 7.5; //it is ~1.04 mm per round (100 rounds are equals to 13mm)
                    // length = Math.Round(length);
                    motor.ClearData();
                    if (motor.SendCommand("!00MM" + length.ToString().PadLeft(5, '0')))//!aaMMddddd(cr) Define move length value
                    {
                        if (VerifyAction("MOV" + length.ToString()))
                        {
                            motor.ClearData();
                            _motorMoveTimer.Restart();
                            if (motor.SendCommand("!00G+"))
                            {
                                if (VerifyAction("*RM"))
                                    AutoApp.Logger.WriteInfoLog("motor started moving backward");

                                if (_gotLimit || VerifyAction(@"\"))
                                {
                                    AutoApp.Logger.WriteInfoLog(string.Format("motor stopped moving backward after - {0} mSec", _motorMoveTimer.ElapsedMilliseconds));
                                    _motorMoveTimer.Stop();
                                    ActionStatus = Enums.Status.Pass;
                                }
                                else if (isControllerRestarts)//driver reset sometimes appends after the motor is touching the limiter after is finish the movement
                                    ActionStatus = Enums.Status.Pass;

                                new System.Threading.ManualResetEvent(false).WaitOne(200);//wait before next move
                            }
                        }
                    }

                    break;

                case ActionType.MoveForward:
                    length = double.Parse(value);// * 7.5; //it is ~1.04 mm per round (100 rounds are equals to 13mm)
                    // length = Math.Round(length);
                    motor.ClearData();
                    if (motor.SendCommand("!00MM" + length.ToString().PadLeft(5, '0')))//!aaMMddddd(cr) Define move length value
                    {
                        if (VerifyAction("MOV" + length.ToString()))
                        {
                            motor.ClearData();
                            _motorMoveTimer.Restart();
                            if (motor.SendCommand("!00G-"))
                            {
                                if (VerifyAction("*RM"))
                                    AutoApp.Logger.WriteInfoLog("motor started moving forward");

                                if (_gotLimit || VerifyAction(@"\"))
                                {
                                    AutoApp.Logger.WriteInfoLog(string.Format("motor stopped moving Forward after - {0} mSec", _motorMoveTimer.ElapsedMilliseconds));
                                    _motorMoveTimer.Stop();
                                    ActionStatus = Enums.Status.Pass;
                                }
                                else if (isControllerRestarts)//driver reset sometimes appends after the motor is touching the limiter after is finish the movement
                                    ActionStatus = Enums.Status.Pass;

                                new System.Threading.ManualResetEvent(false).WaitOne(200);//wait before next move
                            }
                        }
                    }
                    break;
            }

            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("Motor Action " + _type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("Motor Action " + _type.ToString() + " Failed");
        }

        private bool VerifyAction(string expected, string command = null)
        {
            _gotLimit = false;
            isControllerRestarts = false;
            int timeoutCount = 1500;//wait 15 sec for response
            var motor = GetMotorControl();
            StringBuilder sb = new StringBuilder();
            while (timeoutCount > 0)
            {
                new System.Threading.ManualResetEvent(false).WaitOne(10);
                sb.Append(motor.ReadDataBuffer());
                _gotLimit = sb.ToString().ToUpper().Contains("LMT") ? true : false;
                if (_gotLimit || (sb.ToString().ToUpper().Contains(expected.ToUpper())))
                {
                    AutoApp.Logger.WriteInfoLog(string.Format("Driver response {0}", sb.ToString()));
                    return true;
                }

                if (sb.ToString().ToUpper().Contains("LOAD 0"))
                {
                    AutoApp.Logger.WriteWarningLog("Driver Restart happened,Driver response Load 0");
                    return true;
                }
                timeoutCount--;
            }

            //if (sb.ToString().Contains("_Load 0"))
            //{
            //    isControllerRestarts = true;
            //    AutoApp.Logger.WriteFailLog("Driver Restart happened");
            //}

            AutoApp.Logger.WriteFailLog(string.Format("Driver response failure , Got - {0}", sb.ToString()));
            new System.Threading.ManualResetEvent(false).WaitOne(2000);

            //optional
            if (command != null)
            {
                timeoutCount = 20;
                if (motor.SendCommand(command))
                {
                    sb = new StringBuilder();
                    while (timeoutCount > 0)
                    {
                        new System.Threading.ManualResetEvent(false).WaitOne(100);
                        sb.Append(motor.ReadDataBuffer());
                        if (sb.ToString().ToUpper().Contains(expected.ToUpper()))
                        {
                            AutoApp.Logger.WriteInfoLog(string.Format("Driver response {0}", sb.ToString()));
                            return true;
                        }

                        timeoutCount--;
                    }
                    AutoApp.Logger.WriteFailLog(string.Format("Driver did not respond for second command"));
                }
                else
                    AutoApp.Logger.WriteFailLog(string.Format("Driver Send command failure"));
            }

            return false;
        }

        public MotorControllerAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.MotorController)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.ComPort); //1
            Details.Add(_actionData.Value); //2
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { ComPort = Details[1], Value = Details[2] };
        }

        public struct ActionData
        {
            public string ComPort { get; set; } //1

            public string Value { get; set; } //2
        }
    }

    public class MotorControlObj : AbstractSerialCommunication
    {
        public override bool Connect()
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();

            _serialPort.PortName = this.Port;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
            _serialPort.BaudRate = 19200;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1");
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
            _serialPort.ReceivedBytesThreshold = 1;
            _serialPort.NewLine = "\r";
            _serialPort.ReadTimeout = 100;
            _serialPort.WriteTimeout = 100;
            try
            {
                _serialPort.Open();
                return true;
            }
            catch
            {
            }

            return false;
        }

        public MotorControlObj()
        {
        }
    }
}