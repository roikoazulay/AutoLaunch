using System;
using System.IO.Ports;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class RelayControlAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            Connect,
            RelayOn,
            RelayOff,
            SetRelayStatus,
            Disconnect
        }

        public RelayControlAction()
            : base(Enums.ActionTypeId.RelayControl)
        {
        }

        public ControledRelayObj GetRelayObject()
        {
            if (!Singleton.Instance<SavedData>().SerialCommunications.ContainsKey(_actionData.ComPort))
                Singleton.Instance<SavedData>().SerialCommunications.Add(_actionData.ComPort, new ControledRelayObj());

            return (ControledRelayObj)Singleton.Instance<SavedData>().SerialCommunications[_actionData.ComPort];
        }

        private static int COMMAND_DELAY = 300;

        public override void Execute()
        {
            switch (_type)
            {
                case ActionType.Connect:
                    if (GetRelayObject().ConnectRelay((vendor)Enum.Parse(typeof(vendor), _actionData.Vendor), Singleton.Instance<SavedData>().GetVariableData(_actionData.ComPort)))
                        ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.RelayOff:
                case ActionType.RelayOn:
                    if (GetRelayObject().SendRelayCommand((vendor)Enum.Parse(typeof(vendor), _actionData.Vendor), Singleton.Instance<SavedData>().GetVariableData(_actionData.RelayNumber), _type))
                        ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.SetRelayStatus:
                    if (GetRelayObject().SetRelayStatus(Singleton.Instance<SavedData>().GetVariableData(_actionData.RelayNumber)))
                        ActionStatus = Enums.Status.Pass;
                    break;
                case ActionType.Disconnect:
                    if (GetRelayObject().DisConnect())
                        ActionStatus = Enums.Status.Pass;
                    break;
            }
            //wait some time to command completion
            new System.Threading.ManualResetEvent(false).WaitOne(COMMAND_DELAY);

            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("Relay Action " + _type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("Relay Action " + _type.ToString() + " Failed");
        }

        public RelayControlAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.RelayControl)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.ComPort); //1
            Details.Add(_actionData.Vendor); //2
            Details.Add(_actionData.RelayNumber); //3
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { ComPort = Details[1], Vendor = Details[2], RelayNumber = Details[3] };
        }

        public struct ActionData
        {
            public string ComPort { get; set; } //1

            public string Vendor { get; set; } //2

            public string RelayNumber { get; set; } //3
        }

        public enum vendor
        {
            KMTRONIC8PORT,
            IA3174_32PORT
        }
    }

    public class ControledRelayObj : AbstractSerialCommunication
    {
        public override bool Connect()
        {
            return true;
        }

        

        public bool ConnectRelay(RelayControlAction.vendor vendor, string port)
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();

            _serialPort.PortName = port;
            if (vendor == RelayControlAction.vendor.KMTRONIC8PORT)
            {
                _serialPort.BaudRate = 9600;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1");
                _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
                _serialPort.ReceivedBytesThreshold = 1;
                _serialPort.NewLine = "\r";
                _serialPort.ReadTimeout = 100;
                _serialPort.WriteTimeout = 200;
            }
            else if (vendor == RelayControlAction.vendor.IA3174_32PORT)
            {
                _serialPort.BaudRate = 19200;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1");
                _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
                _serialPort.ReceivedBytesThreshold = 1;
                _serialPort.NewLine = "\r";
                _serialPort.ReadTimeout = 100;
                _serialPort.WriteTimeout = 200;
            }

            try
            {
                _serialPort.Open();
                _serialPort.WriteLine("");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ControledRelayObj()
        {
        }

        public bool SendRelayCommand(RelayControlAction.vendor vendor, string port, RelayControlAction.ActionType type)
        {
            //FIRST chanel commands:
            //"FF 01 00" - OFF command
            //"FF 01 01" - ON command
            //EIGHT chanel commands :
            //"FF 08 00" - OFF command
            //"FF 08 01" - ON command

            string command = string.Empty;
            if (vendor == RelayControlAction.vendor.KMTRONIC8PORT)
            {
                command = "FF," + port.PadLeft(2, '0');
                command = "FF," + port.PadLeft(2, '0');
                command += type == RelayControlAction.ActionType.RelayOn ? ",01" : ",00";
                return SendCommand(command, true, false, false);
            }
            else if (vendor == RelayControlAction.vendor.IA3174_32PORT)
            {
                //!aa3DD(cr) = !00301 - Close port 01   !aa30a(cr) - Close port 10  -  !aa40a(cr) - Disable port 10
                //
                command = "!00";
                command += type == RelayControlAction.ActionType.RelayOn ? "3" : "4";
                string p = int.Parse(port).ToString("X");

                command += p.PadLeft(2, '0');
                return SendCommand(command, false, true, false);
            }

            return false;
        }

        public bool SetRelayStatus(string port)
        {
            //Command: !00280008000(cr) This command will activate relay #32 and #16
            return SendCommand("!002" + port, false, true, false);
        }
    }
}