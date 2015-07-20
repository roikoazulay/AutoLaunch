using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Timers;
using AutomationCommon;

namespace AutomationServer
{
    public abstract class AbstractSerialCommunication
    {
        protected SerialPort _serialPort = new SerialPort();

        public string Port { get; set; }

        public string LogFileName { get; set; }

        protected bool _resetLog;
        protected bool _resetLogSucceed;
        private ConcurrentQueue<string> _dataReceived = new ConcurrentQueue<string>();//Incoming Data
        protected System.Timers.Timer _ReadBuffer = new System.Timers.Timer();//timer for retrieving messages from the server
        // protected bool blockLogWrite = false;

        public AbstractSerialCommunication()
        {
            _ReadBuffer.Interval = 1000;//changed from 2000
            _ReadBuffer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _ReadBuffer.Stop();

            if (_resetLog)
            {
                int delCount = 10;
                while (delCount > 0)
                {
                    delCount--;
                    _resetLogSucceed = CommonHelper.DeleteFile(LogFileName);
                    if (_resetLogSucceed)
                    {
                        AutoApp.Logger.WriteInfoLog("Log file was deleted");
                        break;
                    }
                        
                    new System.Threading.ManualResetEvent(false).WaitOne(500);
                    AutoApp.Logger.WriteWarningLog("Failed to rest log file, resume in 0.5 sec");
                }

                CommonHelper.AddToLogFile(LogFileName, "LogFile Reset at " + System.DateTime.Now.ToString() + System.Environment.NewLine);
                _resetLog = false;
            }

            StringBuilder sb = new StringBuilder();
            string data = string.Empty;
            int count = _dataReceived.Count;
            while (count > 0)
            {
                count--;
                _dataReceived.TryDequeue(out data);
                sb.Append(data);
            }

            if (!string.IsNullOrEmpty(sb.ToString()))
                CommonHelper.AddToLogFile(LogFileName, sb.ToString());

            _ReadBuffer.Start();
        }

        public void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _serialPort.ReadExisting();
            if (!string.IsNullOrEmpty(data))
                _dataReceived.Enqueue(data);
        }

        public string ReadDataBuffer()
        {
            StringBuilder sb = new StringBuilder();
            string data = string.Empty;
            int count = _dataReceived.Count;
            while (count > 0)
            {
                count--;
                _dataReceived.TryDequeue(out data);
                sb.Append(data);
            }

            return sb.ToString();
        }

        public abstract bool Connect();

        //this clean the ascii charcters
        public string CleanDisplay(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int pos = input.IndexOf("");
                if (pos != -1)
                {
                    int mPos = input.IndexOf("m", pos);
                    if (mPos != -1)
                        if (mPos < (input.Length - 1))
                            input = input.Remove(pos, ((mPos + 1) - pos));
                }
                else
                    break;
            }
            return input;
        }

        public bool DisConnect()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                return true;
            }
            return false;
        }

        public bool SendCommand(string command, bool asBytes = false, bool lineFeed = false, bool singleByte = false)
        {
            try
            {

               
                List<byte> message = new List<byte>();

                command = Singleton.Instance<SavedData>().GetVariableData(command);

                if (asBytes)
                {
                    var cmdBytes = command.Split(',');
                    foreach (string s in cmdBytes)
                    {
                        byte b = byte.Parse(s, System.Globalization.NumberStyles.HexNumber);
                        message.Add(b);
                    }

                    if (lineFeed)
                    {
                        message.Add(0x0d);
                        message.Add(0x0a);
                    }

                    _serialPort.Write(message.ToArray(), 0, message.Count);
                }
                else if (!singleByte)
                {
                    _serialPort.WriteLine(command);
                }
                else
                {
                    if (command.Contains("CHAR{"))
                    {
                        byte chr = byte.Parse(CommonHelper.ExtructValueBetween("{", "}", command));
                        message.Add(chr);
                    }
                    else
                        message.AddRange(Encoding.UTF8.GetBytes(command));

                    if (lineFeed)
                    {
                        message.Add(0x0d);
                        message.Add(0x0a);
                    }

                    byte[] character = new byte[1];
                    for (int i = 0; i < message.Count; i++)
                    {
                        character[0] = message[i];
                        Thread.Sleep(50);
                        _serialPort.Write(character, 0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
                return false;
            }

            return true;
        }

        public bool IsConnected()
        {
            return _serialPort.IsOpen;
        }

        public bool ClearData()
        {
            _dataReceived = new ConcurrentQueue<string>();
            return true;
        }
    }
}