using System;
using System.IO.Ports;
using AutomationCommon;
using AutomationServer.Actions.RS232;

namespace AutomationServer
{
    public class Rs232Obj : AbstractSerialCommunication
    {
        private bool Connect(Rs232Action.Rs232ActionData data)
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();

            _resetLog = false;
            Port = Singleton.Instance<SavedData>().GetVariableData(data.Port);
            _serialPort.PortName = Port;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
            // Set the port's settings
            _serialPort.BaudRate = int.Parse(data.BaudRate);
            _serialPort.DataBits = int.Parse(data.DataBits);
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), data.StopBits);
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), data.Parity);
            _serialPort.DtrEnable = Convert.ToBoolean(data.Dtr); //some devices requires this
            _serialPort.RtsEnable = Convert.ToBoolean(data.Rts);
            _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), data.Handshake);

            _serialPort.ReceivedBytesThreshold = 1;
            _serialPort.NewLine = "\r";
            // _serialPort.Encoding = System.Text.Encoding.UTF8;
            _serialPort.ReadTimeout = 100;
            _serialPort.WriteTimeout = 200;
            try
            {
                _serialPort.Open();
                LogFileName = Singleton.Instance<SavedData>().GetVariableData(data.LogFileName); //set log file name
                AutoApp.Logger.WriteInfoLog("Open Log file on " + LogFileName);
                CommonHelper.AddToLogFile(LogFileName, "Connecting COM Port - " + Port + System.Environment.NewLine);

                _ReadBuffer.Start();//start read buffer
                return true;
            }
            catch
            {
                return false;
            }
        }

        private Rs232Action.Rs232ActionData _data;

        public Rs232Obj(Rs232Action.Rs232ActionData data)
        {
            _data = data;
        }

        public override bool Connect()
        {
            return Connect(_data);
        }

        //public bool ResetLog()
        //{
        //    _blockWrite = true;
        //    _resetLog = true;
        //    int timeout = 10;
        //    while (_resetLog)
        //    {
        //        new System.Threading.ManualResetEvent(false).WaitOne(1000);
        //        timeout--;
        //        if (timeout<0)

        //    }

        //    Thread.Sleep(1000);
        //   bool res = CommonHelper.DeleteFile(LogFileName);
        //   _blockWrite = false;
        //   CommonHelper.AddToLogFile(LogFileName, "LogFile Reset at " +System.DateTime.Now.ToString() + System.Environment.NewLine);
        //   return res;
        //}

        public bool ResetLog()
        {
            bool timeout = false;
            _resetLog = true;
            int count = 10;
            while (_resetLog)
            {
                AutoApp.Logger.WriteInfoLog("Wait for rest log to complete");
                new System.Threading.ManualResetEvent(false).WaitOne(1000);
                count--;
                if (count < 0)
                {
                    timeout = true;
                    break;
                }
            }
            
            return timeout ? false : _resetLogSucceed;
        }
    }
}