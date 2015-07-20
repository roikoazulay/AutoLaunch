using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using AutomationCommon;
using Routrek.SSHC;

namespace AutomationServer
{
    //public class SshObj1
    //{
    //    SshStream ssh;
    //    public string Host { get; set; }
    //    public string UserName { get; set; }
    //    public string Password { get; set; }
    //    public string Error { get; set; }
    //    public string LogFileName { get; set; }
    //    protected bool blockLogWrite = false;

    //    System.Timers.Timer _ReadBuffer = new  System.Timers.Timer();//timer for retrieving messages from the server

    //    public SshObj1(string host, string username, string password,string logFileName)
    //    {
    //        Host = host;
    //        UserName = username;
    //        Password = password;
    //        _ReadBuffer.Interval = 100;
    //        LogFileName = logFileName;//set log file name
    //        _ReadBuffer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);

    //    }

    //   // bool isReading = false;

    //    private void _timer_Elapsed(object sender, ElapsedEventArgs e)
    //    {
    //        _ReadBuffer.Stop();
    //       // isReading = true;
    //         string data=string.Empty;
    //         try
    //         {
    //            data = ssh.ReadResponse();
    //         }
    //         catch(Exception ex)
    //         {
    //             AutoApp.Logger.WriteFatalLog(ex.ToString());
    //         }

    //        if (!string.IsNullOrEmpty(data))
    //        {
    //            if (!blockLogWrite)
    //            {
    //                CommonHelper.AddToLogFile(LogFileName, data);
    //            }
    //        }

    //      //  isReading = false;
    //        _ReadBuffer.Start();
    //    }

    //    public bool GetData()
    //    {
    //        //ssh.Write("");//sending
    //        //_ReadBuffer.Start();
    //        //Thread.Sleep(5000);
    //        //if (isReading)
    //        //    return false;

    //        return true;

    //    }

    //    public bool ResetLog()
    //    {
    //        blockLogWrite = true;
    //        bool res = CommonHelper.DeleteFile(LogFileName);
    //        CommonHelper.AddToLogFile(LogFileName, "");
    //        Thread.Sleep(200);
    //        blockLogWrite = false;
    //        return res;
    //    }

    //    public bool Connect()
    //    {
    //        try
    //        {
    //            if (ssh == null)
    //            {
    //                ssh = new SshStream(Host, UserName, Password);
    //                CommonHelper.AddToLogFile(LogFileName, "");
    //                // ssh.RemoveTerminalEmulationCharacters = true;
    //                SendCommand("");
    //                _ReadBuffer.Start();
    //            }

    //            return true;
    //        }
    //        catch(Exception ex)
    //        {
    //            Error = ex.ToString();
    //        }

    //        return false;

    //    }

    //    public bool DisConnect()
    //    {
    //        try
    //        {
    //            _ReadBuffer.Stop();
    //            ssh = new SshStream(Host, UserName, Password);
    //            ssh.Dispose();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            Error = ex.ToString();
    //        }

    //        return false;

    //    }

    //    public bool SendCommand(string command)
    //    {
    //        try
    //        {
    //            AutoApp.Logger.WriteInfoLog(string.Format("Sending [{0}] command",command));
    //            ssh.Write(command);
    //           // new System.Threading.ManualResetEvent(false).WaitOne(1000);
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            Error = ex.ToString();
    //        }

    //        return false;
    //    }

    //}

    public class SshGranados
    {
        private SSHConnectionParameter _conParam = new SSHConnectionParameter();
        private Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private Reader reader = new Reader(null);

        public string Host { get; set; }

        public string FileName
        {
            get { return reader.LogFileName; }
        }

        public string PromptChar { get; set; }

        public SshGranados(string host, string username, string password, string logFileName)
        {
            PromptChar = "~#";
            Host = host;
            _conParam.UserName = username;
            _conParam.Password = password;
            _conParam.Protocol = SSHProtocol.SSH2;
            _conParam.AuthenticationType = AuthenticationType.Password;
            _conParam.WindowSize = 0x1000;

            reader.LogFileName = logFileName;//set log file name
        }

        public bool GetData()
        {
            return true;
        }

        public bool ResetLog()
        {
            bool timeout = false;
            reader.ResetLog = true;
            int count = 10;
            while (reader.ResetLog)
            {
                new System.Threading.ManualResetEvent(false).WaitOne(1000);
                count--;
                if (count < 0)
                {
                    timeout = true;
                    break;
                }
            }

            return timeout ? false : reader.ResetLogSucceed;
        }

        //public bool ResetLog()
        //{
        //    reader.BlockLogWrite  = true;
        //    bool res = CommonHelper.DeleteFile(reader.LogFileName);
        //    CommonHelper.AddToLogFile(reader.LogFileName, "");
        //    new System.Threading.ManualResetEvent(false).WaitOne(200);
        //    reader.BlockLogWrite = false;
        //    return res;
        //}

        public bool Connect()
        {
            // SSHConnection _conn=null;
            // if (_conn == null)
            // {
            //s.Connect(new IPEndPoint(IPAddress.Parse(Host), 22));
            //_conn = SSHConnection.Connect(_conParam, reader, s);

            //s.Disconnect(false);
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // _conn = null;
            s.Connect(new IPEndPoint(IPAddress.Parse(Host), 22));
            SSHConnection _conn = SSHConnection.Connect(_conParam, reader, s);
            //  }
            //else
            //{
            //    _conn = SSHConnection.Connect(_conParam, reader, s);
            //}

            reader._conn = _conn;
            //	_conn.ListenForwardedPort("0.0.0.0", 29472);
            SSHChannel ch = _conn.OpenShell(reader);
            reader._pf = ch;

            int connTimeout = 10;

            string data = string.Empty;

            while (connTimeout > 0)
            {
                bool fileExist = CommonHelper.ReadTextFile(FileName, ref data);
                if (fileExist)
                {
                    if (data.Contains(PromptChar))
                        return true;
                }
                new System.Threading.ManualResetEvent(false).WaitOne(1000);
                connTimeout--;
            }

            return false;
        }

        public bool DisConnect()
        {
            try
            {
                s.Disconnect(false);
                new System.Threading.ManualResetEvent(false).WaitOne(2000);
                s.Dispose();
                // _conn.Disconnect("bye");

                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public bool SendCommand(string command)
        {
            try
            {
                AutoApp.Logger.WriteInfoLog(string.Format("Sending [{0}] command", command));

                byte[] array = Encoding.ASCII.GetBytes(command + System.Environment.NewLine);
                byte[] b = new byte[1];
                foreach (byte byteChar in array)
                {
                    b[0] = byteChar;
                    reader._pf.Transmit(b);
                    new System.Threading.ManualResetEvent(false).WaitOne(10);
                }

                new System.Threading.ManualResetEvent(false).WaitOne(1000);
                return true;
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
            }

            return false;
        }
    }

    public class Reader : ISSHConnectionEventReceiver, ISSHChannelEventReceiver
    {
        public SSHConnection _conn;
        public bool _ready;
        protected System.Timers.Timer _ReadBuffer = new System.Timers.Timer();//timer for retrieving messages from the server
        private ConcurrentQueue<string> _dataReceived = new ConcurrentQueue<string>();//Incoming Data

        public bool ResetLogSucceed { get; set; }

        public bool ResetLog { get; set; }

        public string LogFileName { get; set; }

        public Reader(string fileName)
        {
            ResetLog = false;
            LogFileName = fileName;
            _ReadBuffer.Interval = 2000;
            _ReadBuffer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _ReadBuffer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _ReadBuffer.Stop();
            ResetLogSucceed = false;
            if (ResetLog)
            {
                ResetLogSucceed = CommonHelper.DeleteFile(LogFileName);
                CommonHelper.AddToLogFile(LogFileName, "LogFile Reset at " + System.DateTime.Now.ToString() + System.Environment.NewLine);
            }

            ResetLog = false;
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

        public void OnData(byte[] data, int offset, int length)
        {
            string dataToDisplay = Encoding.ASCII.GetString(data, offset, length);

            if (!string.IsNullOrEmpty(dataToDisplay))
                _dataReceived.Enqueue(dataToDisplay);
        }

        public void OnDebugMessage(bool always_display, byte[] data)
        {
            Debug.WriteLine("DEBUG: " + Encoding.ASCII.GetString(data));
        }

        public void OnIgnoreMessage(byte[] data)
        {
            Debug.WriteLine("Ignore: " + Encoding.ASCII.GetString(data));
        }

        public void OnAuthenticationPrompt(string[] msg)
        {
            Debug.WriteLine("Auth Prompt " + msg[0]);
        }

        public void OnError(Exception error, string msg)
        {
            Debug.WriteLine("ERROR: " + msg);
        }

        public void OnChannelClosed()
        {
            Debug.WriteLine("Channel closed");
            _conn.Disconnect("");
            //_conn.AsyncReceive(this);
        }

        public void OnChannelEOF()
        {
            _pf.Close();
            Debug.WriteLine("Channel EOF");
        }

        public void OnExtendedData(int type, byte[] data)
        {
            Debug.WriteLine("EXTENDED DATA");
        }

        public void OnConnectionClosed()
        {
            Debug.WriteLine("Connection closed");
        }

        public void OnUnknownMessage(byte type, byte[] data)
        {
            Debug.WriteLine("Unknown Message " + type);
        }

        public void OnChannelReady()
        {
            _ready = true;
        }

        public void OnChannelError(Exception error, string msg)
        {
            Debug.WriteLine("Channel ERROR: " + msg);
        }

        public void OnMiscPacket(byte type, byte[] data, int offset, int length)
        {
        }

        public PortForwardingCheckResult CheckPortForwardingRequest(string host, int port, string originator_host, int originator_port)
        {
            PortForwardingCheckResult r = new PortForwardingCheckResult();
            r.allowed = true;
            r.channel = this;
            return r;
        }

        public void EstablishPortforwarding(ISSHChannelEventReceiver rec, SSHChannel channel)
        {
            _pf = channel;
        }

        public SSHChannel _pf;
    }
}