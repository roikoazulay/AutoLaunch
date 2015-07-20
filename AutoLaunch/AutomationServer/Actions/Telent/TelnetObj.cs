using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using AutomationCommon;

namespace AutomationServer
{
    internal enum Verbs
    {
        WILL = 251,
        WONT = 252,
        DO = 253,
        DONT = 254,
        IAC = 255
    }

    internal enum Options
    {
        SGA = 3
    }

    public class TelnetObj
    {
        private TcpClient tcpSocket;

        private int TimeOutMs = 100;

        public TelnetObj(string Hostname, string Port)
        {
            tcpSocket = new TcpClient(Hostname, int.Parse(Port));
        }

        public void Connect(int LoginTimeOutMs = 100)
        {
            TimeOutMs = LoginTimeOutMs;
            string s = Read();
        }

        public void WriteLine(string cmd)
        {
            Write(cmd + "\r");
        }

        public void Write(string cmd)
        {
            if (!tcpSocket.Connected) return;
            byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(cmd.Replace("\0xFF", "\0xFF\0xFF"));
            tcpSocket.GetStream().Write(buf, 0, buf.Length);
        }

        public string Read()
        {
            if (!tcpSocket.Connected) return null;
            StringBuilder sb = new StringBuilder();
            do
            {
                ParseTelnet(sb);
                System.Threading.Thread.Sleep(TimeOutMs);
            } while (tcpSocket.Available > 0);
            return sb.ToString();
        }

        public bool IsConnected
        {
            get { return tcpSocket.Connected; }
        }

        private void ParseTelnet(StringBuilder sb)
        {
            while (tcpSocket.Available > 0)
            {
                int input = tcpSocket.GetStream().ReadByte();
                switch (input)
                {
                    case -1:
                        break;

                    case (int)Verbs.IAC:
                        // interpret as command
                        int inputverb = tcpSocket.GetStream().ReadByte();
                        if (inputverb == -1) break;
                        switch (inputverb)
                        {
                            case (int)Verbs.IAC:
                                //literal IAC = 255 escaped, so append char 255 to string
                                sb.Append(inputverb);
                                break;

                            case (int)Verbs.DO:
                            case (int)Verbs.DONT:
                            case (int)Verbs.WILL:
                            case (int)Verbs.WONT:
                                // reply to all commands with "WONT", unless it is SGA (suppres go ahead)
                                int inputoption = tcpSocket.GetStream().ReadByte();
                                if (inputoption == -1) break;
                                tcpSocket.GetStream().WriteByte((byte)Verbs.IAC);
                                if (inputoption == (int)Options.SGA)
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WILL : (byte)Verbs.DO);
                                else
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WONT : (byte)Verbs.DONT);
                                tcpSocket.GetStream().WriteByte((byte)inputoption);
                                break;

                            default:
                                break;
                        }
                        break;

                    default:
                        sb.Append((char)input);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Summary description for clsScriptingTelnet.
    /// </summary>
    public class TelnetClass
    {
        private IPEndPoint iep;

        //	private AsyncCallback callbackProc ;
        private string _address;

        private int _port;
        private int _timeout;
        private Socket s;
        private Byte[] m_byBuff = new Byte[32767];
        private string strWorkingData = "";	// Holds everything received from the server since our last processing
        private bool _startCaptureFlag;
        private string strFullLog;//
        private List<string> _recivedDate;//will hold the recived data
        private List<string> _strCapture;//will hold the recived data when _startCaptureFlag is on

        public TelnetClass(string address, string port, string CommandTimeout = "100")
        {
            _startCaptureFlag = false;
            _strCapture = new List<string>();
            _recivedDate = new List<string>();
            _address = address;
            _port = int.Parse(port);
            _timeout = int.Parse(CommandTimeout);
        }

        public string GetRecivedDate()
        {
            StringBuilder sb = new StringBuilder();
            // string retString = "";
            int lastRow = _recivedDate.Count;
            for (int i = 0; i < lastRow; i++)
            {
                sb.Append(_recivedDate[i]);
                //retString += _recivedDate[0];
                //remove the row from the saved data
                // _recivedDate.RemoveAt(0);
            }

            return sb.ToString();
            // return retString;
        }

        public string GetAndClearDate()
        {
            StringBuilder sb = new StringBuilder();

            int lastRow = _recivedDate.Count;
            for (int i = 0; i < lastRow; i++)
            {
                sb.Append(_recivedDate[0]);
                //remove the row from the saved data
                _recivedDate.RemoveAt(0);
            }

            return sb.ToString();
        }

        public void ClearData()
        {
            _recivedDate.Clear();
        }

        public string GetCaptureData()
        {
            string retString = "";
            int lastRow = _strCapture.Count;
            for (int i = 0; i < lastRow; i++)
                retString += _strCapture[i];

            return retString;
        }

        public void StartCapture()
        {
            _startCaptureFlag = true;
            _strCapture.Clear();
        }

        public void StopCapture()
        {
            _startCaptureFlag = false;
        }

        private void OnRecievedData(IAsyncResult ar)
        {
            // Get The connection socket from the callback
            Socket sock = (Socket)ar.AsyncState;
            try
            {
                // Get The data , if any
                int nBytesRec = sock.EndReceive(ar);

                if (nBytesRec > 0)
                {
                    // Decode the received data
                    // string sRecieved = CleanDisplay(Encoding.ASCII.GetString(m_byBuff, 0, nBytesRec));
                    string sRecieved = Encoding.ASCII.GetString(m_byBuff, 0, nBytesRec);

                    // Write out the data back to the server
                    //    if (sRecieved.IndexOf("[c") != -1) Negotiate(1);
                    //  if (sRecieved.IndexOf("[6n") != -1) Negotiate(2);

                    //save the recived data
                    _recivedDate.Add(sRecieved);

                    //save data for capturing
                    if (_startCaptureFlag)
                        _strCapture.Add(sRecieved);

                    // Launch another callback to listen for data
                    AsyncCallback recieveData = new AsyncCallback(OnRecievedData);
                    sock.BeginReceive(m_byBuff, 0, m_byBuff.Length, SocketFlags.None, recieveData, sock);
                }
                else
                {
                    // If no data was recieved then the connection is probably dead
                    //	Console.WriteLine( "Disconnected", sock.RemoteEndPoint );
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                    //Application.Exit();
                }
            }
            catch
            {
                // sock.Shutdown(SocketShutdown.Both);
                sock.Close();
            }
        }

        //~ScriptingTelnet()
        //{
        //    s.Close();
        //}

        private bool DoSend(string strText)
        {
            try
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                Byte[] smk = enc.GetBytes(strText);
                s.Send(smk, 0, smk.Length, SocketFlags.None);
                //s.Send(smk);//, 0, smk.Length, SocketFlags.None);
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ERROR IN RESPOND OPTIONS");
            }
            return false;
        }

        private void Negotiate(int WhichPart)
        {
            StringBuilder x;
            string neg;
            if (WhichPart == 1)
            {
                x = new StringBuilder();
                x.Append((char)27);
                x.Append((char)91);
                x.Append((char)63);
                x.Append((char)49);
                x.Append((char)59);
                x.Append((char)50);
                x.Append((char)99);
                neg = x.ToString();
            }
            else
            {
                x = new StringBuilder();
                x.Append((char)27);
                x.Append((char)91);
                x.Append((char)50);
                x.Append((char)52);
                x.Append((char)59);
                x.Append((char)56);
                x.Append((char)48);
                x.Append((char)82);
                neg = x.ToString();
            }
            SendMessage(neg, true);
        }

        private string CleanDisplay(string input)
        {
            input = input.Replace("(0x (B", "|");
            input = input.Replace("(0 x(B", "|");
            input = input.Replace(")0=>", "");
            input = input.Replace("[0m>", "");
            input = input.Replace("7[7m", "[");
            input = input.Replace("[0m*8[7m", "]");
            input = input.Replace("[0m", "");
            // input = input.Replace("[1;37m", "");//removes escape characters
            // input = input.Replace("[49;m", "");//removes escape characters
            // input = input.Replace("[1;33m", "");//removes escape characters
            //   input = input.Replace("[1;35m", "");//removes escape characters
            for (int i = 0; i < input.Length; i++)
            {
                int pos = input.IndexOf("");
                if (pos != -1)
                {
                    int mPos = input.IndexOf("m", pos);
                    input = input.Remove(pos, mPos - pos);
                }
            }
            return input;
        }

        /// <summary>
        /// Connects to the telnet server.
        /// </summary>
        /// <returns>True upon connection, False if connection fails</returns>
        public bool Connect()
        {
            //if (s != null)
            //    Disconnect();
            string[] address = _address.Split('.');
            byte[] tmpAddr = new byte[4];
            for (int i = 0; i < 4; i++)
                tmpAddr[i] = byte.Parse(address[i]);
            IPAddress hostIPAddress = new IPAddress(tmpAddr);

            //IPHostEntry IPHost = Dns.GetHostEntry(hostIPAddress);
            // IPAddress[] addr = IPHost.AddressList;

            try
            {
                // Try a blocking connection to the server
                if (s == null)
                {
                    s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    // s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                }

                iep = new IPEndPoint(hostIPAddress, _port);
                if (!s.Connected)
                {
                    s.Connect(iep);
                    // If the connect worked, setup a callback to start listening for incoming data
                    AsyncCallback recieveData = new AsyncCallback(OnRecievedData);
                    s.BeginReceive(m_byBuff, 0, m_byBuff.Length, SocketFlags.None, recieveData, s);

                    // All is good
                }

                return true;
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
                // Something failed
                return false;
            }
        }

        public bool Disconnect()
        {
            int timeoutCnt = 10;
            try
            {
                s.Disconnect(false);
                s.Dispose();
                while (timeoutCnt > 0)
                {
                    new System.Threading.ManualResetEvent(false).WaitOne(100);
                    if (!s.Connected)
                    {
                        s = null;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
            }

            // s.Close();

            if (timeoutCnt > 0)
            {
                // AutoApp.Logger.WriteInfoLog("Telnet Disconnected");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Waits for a specific string to be found in the stream from the server
        /// </summary>
        /// <param name="DataToWaitFor">The string to wait for</param>
        /// <returns>Always returns 0 once the string has been found</returns>
        public int WaitFor(string DataToWaitFor)
        {
            // Get the starting time
            long lngStart = DateTime.Now.AddSeconds(this._timeout).Ticks;
            long lngCurTime = 0;

            while (strWorkingData.ToLower().IndexOf(DataToWaitFor.ToLower()) == -1)
            {
                // Timeout logic
                lngCurTime = DateTime.Now.Ticks;
                if (lngCurTime > lngStart)
                {
                    throw new Exception("Timed Out waiting for : " + DataToWaitFor);
                }
                new System.Threading.ManualResetEvent(false).WaitOne(1);
            }
            strWorkingData = "";
            return 0;
        }

        /// <summary>
        /// Waits for one of several possible strings to be found in the stream from the server
        /// </summary>
        /// <param name="DataToWaitFor">A delimited list of strings to wait for</param>
        /// <param name="BreakCharacters">The character to break the delimited string with</param>
        /// <returns>The index (zero based) of the value in the delimited list which was matched</returns>
        public int WaitFor(string DataToWaitFor, string BreakCharacter)
        {
            // Get the starting time
            long lngStart = DateTime.Now.AddSeconds(this._timeout).Ticks;
            long lngCurTime = 0;

            string[] Breaks = DataToWaitFor.Split(BreakCharacter.ToCharArray());
            int intReturn = -1;

            while (intReturn == -1)
            {
                // Timeout logic
                lngCurTime = DateTime.Now.Ticks;
                if (lngCurTime > lngStart)
                {
                    throw new Exception("Timed Out waiting for : " + DataToWaitFor);
                }

                new System.Threading.ManualResetEvent(false).WaitOne(1);
                for (int i = 0; i < Breaks.Length; i++)
                {
                    if (strWorkingData.ToLower().IndexOf(Breaks[i].ToLower()) != -1)
                    {
                        intReturn = i;
                    }
                }
            }
            return intReturn;
        }

        /// <summary>
        /// Sends a message to the server
        /// </summary>
        /// <param name="Message">The message to send to the server</param>
        /// <param name="SuppressCarriageReturn">True if you do not want to end the message with a carriage return</param>
        public bool SendMessage(string Message, bool SuppressCarriageReturn)
        {
            // strFullLog += "\r\nSENDING DATA ====> " + Message.ToUpper() + "\r\n";
            //Console.WriteLine("SENDING DATA ====> " + Message.ToUpper());

            if (!SuppressCarriageReturn)
            {
                return DoSend(Message + "\r");
            }
            else
            {
                return DoSend(Message);
            }
            //Thread.Sleep(200);
        }

        /// <summary>
        /// Sends a message to the server, automatically appending a carriage return to it
        /// </summary>
        /// <param name="Message">The message to send to the server</param>
        public bool SendMessage(string Message)
        {
            //strFullLog += Message + "\r\n";
            // return DoSend(Message + "\r");
            return DoSend(Message + System.Environment.NewLine);
        }

        /// <summary>
        /// Waits for a specific string to be found in the stream from the server.
        /// Once that string is found, sends a message to the server
        /// </summary>
        /// <param name="WaitFor">The string to be found in the server stream</param>
        /// <param name="Message">The message to send to the server</param>
        /// <returns>Returns true once the string has been found, and the message has been sent</returns>
        public bool WaitAndSend(string WaitFor, string Message)
        {
            this.WaitFor(WaitFor);
            SendMessage(Message);
            return true;
        }

        /// <summary>
        /// Sends a message to the server, and waits until the designated
        /// response is received
        /// </summary>
        /// <param name="Message">The message to send to the server</param>
        /// <param name="WaitFor">The response to wait for</param>
        /// <returns>True if the process was successful</returns>
        public int SendAndWait(string Message, string WaitFor)
        {
            SendMessage(Message);
            this.WaitFor(WaitFor);
            return 0;
        }

        public int SendAndWait(string Message, string WaitFor, string BreakCharacter)
        {
            SendMessage(Message);
            int t = this.WaitFor(WaitFor, BreakCharacter);
            return t;
        }

        /// <summary>
        /// A full log of session activity
        /// </summary>
        public string GetFullSessionLog
        {
            get { return strFullLog; }
            set { strFullLog = value; }
        }

        public string GetCurrentSessionLog
        {
            get { return strWorkingData; }
            set { strWorkingData = value; }
        }

        /// <summary>
        /// Clears all data in the session log
        /// </summary>
        public void ClearSessionLog()
        {
            strFullLog = "";
        }

        /// <summary>
        /// Searches for two strings in the session log, and if both are found, returns
        /// all the data between them.
        /// </summary>
        /// <param name="StartingString">The first string to find</param>
        /// <param name="EndingString">The second string to find</param>
        /// <param name="ReturnIfNotFound">The string to be returned if a match is not found</param>
        /// <returns>All the data between the end of the starting string and the beginning of the end string</returns>
        public string FindStringBetween(string StartingString, string EndingString, string ReturnIfNotFound)
        {
            int intStart;
            int intEnd;

            intStart = strFullLog.ToLower().IndexOf(StartingString.ToLower());
            if (intStart == -1)
            {
                return ReturnIfNotFound;
            }
            intStart += StartingString.Length;

            intEnd = strFullLog.ToLower().IndexOf(EndingString.ToLower(), intStart);

            if (intEnd == -1)
            {
                // The string was not found
                return ReturnIfNotFound;
            }

            // The string was found, let's clean it up and return it
            return strFullLog.Substring(intStart, intEnd - intStart).Trim();
        }
    }
}