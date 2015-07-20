using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using AutomationCommon;

namespace AutomationServer
{
    public class TcpListenerObj
    {
        private int _portNum;

        public TcpListenerObj(int portNum)
        {
            _portNum = portNum;
            Thread treadStart = new Thread(Start);
            treadStart.Start();
        }

        public void Start()
        {
            TcpListener server = null;
            try
            {
                string serverip = Singleton.Instance<AppSettings>().ServerIp;
                if (string.IsNullOrEmpty(serverip))
                    serverip = "127.0.0.1";
                IPAddress localAddr = IPAddress.Parse(serverip);
                server = new TcpListener(localAddr, _portNum);

                // Start listening for client requests.
                server.Start();

                Byte[] bytes = new Byte[256];
                string data = null;

                // Enter listening
                while (true)
                {
                    AutoApp.Logger.WriteInfoLog(string.Format("Tcp Listener activated on IP Address {0} port {1}, Waiting for a connection.", serverip, _portNum));

                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    AutoApp.Logger.WriteInfoLog("Tcp Listener got connection.");
                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;
                    try
                    {
                        // Loop to receive all the data sent by the client.
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            // Translate data bytes to a ASCII string.
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                            AutoApp.Logger.WriteInfoLog(string.Format("Tcp Listener received message - {0}", data));

                            string retVal = ExecuteCommand(data);

                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(retVal);
                            // Send back a response.
                            stream.Write(msg, 0, msg.Length);
                            // Console.WriteLine("Sent: {0}", data);
                        }
                    }
                    catch
                    {
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                AutoApp.Logger.WriteFatalLog(string.Format("Tcp Listener SocketException {0}", e));
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }

        private string ExecuteCommand(string command)
        {
            string retVal = string.Empty;
            //    if (command.Contains(ServerComAction.ActionType.ExecuteSuite.ToString()))
            //        retVal = ExecuteSuite(command);
            //    else if (command.Contains(ServerComAction.ActionType.GetServerStatus.ToString()))
            //        retVal = GetRunningStatus();
            //    else if (command.Contains(ServerComAction.ActionType.GetLastExecutionStatus.ToString()))
            //        retVal = GetLastExecutionStatus();
            //    else if (command.Contains(ServerComAction.ActionType.GetValue.ToString()))
            //        retVal = GetValue(command);
            return retVal;
        }

        private string GetValue(string command)
        {
            var d = command.Split(':');
            var varName = d[1].TrimEnd('\r', '\n');
            string data = Singleton.Instance<SavedData>().GetVariableData(varName);
            return data;
        }

        private string GetLastExecutionStatus()
        {
            return string.Format("Last Execution Status {0}", Singleton.Instance<ActionHandler>().ActiveTestSuite.Status);
        }

        //ExecuteSuite -s:temp.tsu -c:true -o:Continue -l:1
        private string ExecuteSuite(string command)
        {
            //no close on finish the listener needs to stay open
            bool closeOnFinish = Convert.ToBoolean(CommonHelper.ExtructValueBetween("-c:", "-o:", command));

            Singleton.Instance<ActionHandler>().Cycles = Convert.ToInt32(CommonHelper.ExtructValueBetween("-l:", '\n'.ToString(), command));

            string onfail = CommonHelper.ExtructValueBetween("-o:", "-l:", command);

            Enums.OnFailerAction action = Enums.OnFailerAction.SkipTest;
            Enum.TryParse(onfail, true, out action);
            Singleton.Instance<ActionHandler>().OnFailureAction = action;

            string suitName = CommonHelper.ExtructValueBetween("-s:", "-c:", command).TrimEnd();

            FileInfo f = new FileInfo(suitName);
            if (!f.Exists)
            {
                suitName = CommonHelper.GetSuiteLocation(f.Name.Replace(".tsu", ""));
                if (string.IsNullOrEmpty(suitName))
                    suitName = CommonHelper.GetSuiteLocation(suitName.Replace(".tsu", ""));
            }

            AutoApp.Logger.WriteInfoLog(string.Format("Tcp Listener activating Suite Name {0}", suitName));
            Singleton.Instance<ActionHandler>().ActiveTestSuite = FileHandler.ExtructTestSuiteFromFile(suitName);
            Singleton.Instance<ActionHandler>().ActiveTestSuite.Name = suitName;

            return string.Format("Tcp Listener activating Suite Name {0}", suitName);
        }

        private string GetRunningStatus()
        {
            string retVal = string.Empty;
            if (Singleton.Instance<ActionHandler>().IsActive)
                retVal = "Automation is running";
            else
                retVal = "Automation in Idle state";

            return retVal;
        }
    }
}