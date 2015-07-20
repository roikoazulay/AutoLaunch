using System;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Timers;
using AutomationCommon;

namespace AutomationServer
{
    public class AutomationManager
    {
        private bool closeOnFinish = false;
        private bool activateWcfServer = true;
        public ActionHandler handler = Singleton.Instance<ActionHandler>();
        private System.Timers.Timer actionScanTimer = new System.Timers.Timer();//timer for scanning new job
        private ManualResetEvent manualReset = new ManualResetEvent(false);
        private string _activeSuitName;
        //  TcpListenerObj _tcpListen;
        private Thread treadStart;

        public AutomationManager(string[] args)
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine(string.Format("Auto Launch server version {0}", version.ToString()));
            Console.WriteLine();
            Console.WriteLine("AutomationServer [-s:suiteName] [-c:True|False] [-l:loopCount] [-o:SkipTest,Continue,Stop]  [-r:retryCount]");
            Console.WriteLine("-s:suiteName                 Specifies the Suite Name Full Path");
            Console.WriteLine("-c:True|False                Close On Finish");
            Console.WriteLine("-l:loopCount                 Specifies How many loops");
            Console.WriteLine("-o:SkipTest,Continue,Stop    Specifies On Failure Action");
            Console.WriteLine("-r:retryCount                Specifies How many retries until failure");
            Console.WriteLine("");

            AutoApp.Logger.WriteInfoLog("Generating suite files table");
            CommonHelper.GenerateSuiteLookUpTable();

            actionScanTimer.Interval = 1000;
            actionScanTimer.Elapsed += new ElapsedEventHandler(action_ScanTimer_Elapsed);

            // _tcpListen = new TcpListenerObj(2222);

            treadStart = new Thread(StartRemoteServerWcfService);
            treadStart.Start();

            //when activating via command line , no need to activate WCF server
            if (args.Length == 0)
            {
                AutoApp.Logger.WriteInfoLog("Startup Suite is empty... Waiting for activation");
                treadStart = new Thread(StartClientServerWcfService);
                treadStart.Start();
            }
            else
            {
                //config the command line parameters
                foreach (var s in args)
                {
                    if (s.Contains("-s:"))
                    {
                        string suitName = s.Replace("-s:", string.Empty);
                        FileInfo f = new FileInfo(suitName);
                        if (!f.Exists)
                        {
                            suitName = CommonHelper.GetSuiteLocation(f.Name.Replace(".tsu", ""));
                            if (string.IsNullOrEmpty(suitName))
                                suitName = CommonHelper.GetSuiteLocation(suitName.Replace(".tsu", ""));
                        }

                        Singleton.Instance<ActionHandler>().ActiveTestSuite = FileHandler.ExtructTestSuiteFromFile(suitName);
                        Singleton.Instance<ActionHandler>().ActiveTestSuite.Name = suitName;
                        _activeSuitName = suitName;
                        AutoApp.Logger.WriteInfoLog(string.Format("Activating Startup Suite Name {0}", suitName));
                    }
                    if (s.Contains("-c:"))
                        closeOnFinish = Convert.ToBoolean(s.Replace("-c:", string.Empty));
                    if (s.Contains("-l:"))
                        Singleton.Instance<ActionHandler>().Cycles = Convert.ToInt32(s.Replace("-l:", string.Empty));
                    if (s.Contains("-o:"))
                    {
                        string onFailAction = s.Replace("-o:", string.Empty);
                        Enums.OnFailerAction action = Enums.OnFailerAction.SkipTest;
                        Enum.TryParse(onFailAction, true, out action);
                        Singleton.Instance<ActionHandler>().OnFailureAction = action;
                    }

                    if (s.Contains("-r:"))
                        Singleton.Instance<ActionHandler>().Retries = Convert.ToInt32(s.Replace("-r:", string.Empty));
                }
            }

            //======================================================================
            //start the timer for scanning new test to activate
            actionScanTimer.Start();
            //======================================================================

            manualReset.WaitOne();

            //if (!string.IsNullOrEmpty(Singleton.Instance<AppSettings>().TearDownScript))
            //{
            //     AutoApp.Logger.WriteInfoLog("Executing TearDown Script");
            //     ScriptObj s = FileHandler.ExtructScriptFromFile(Singleton.Instance<AppSettings>().TearDownScript);
            //     s.Execute();
            //}

            AutoApp.Logger.WriteInfoLog("AutoLunch block released");
            // Console.ReadLine();
            //while (true)
            //{
            //    if (handler.ActiveTestSuite.Entities.Count != 0)
            //    {
            //        handler.Excute();
            //        while (handler.IsActive)
            //        {
            //        }

            //        handler.ActiveTestSuite.Entities.Clear();
            //    }
            //    Thread.Sleep(100);

            //    if (closeOnFinish)
            //    {
            //       // treadStart.Abort();
            //        AutoApp.Logger.WriteInfoLog("Closing Worker");
            //        handler.StopBwExecution();
            //        AutoApp.Logger.WriteInfoLog("Shutting Down AutoLunch");
            //        return;
            //        activateWcfServer = false;

            //    }
            //}
        }

        private void action_ScanTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Title = "Auto Launch - V" + Assembly.GetExecutingAssembly().GetName().Version + " " + DateTime.Now.ToString() + ", " + Singleton.Instance<ActionHandler>().GetActiveStepInfo();

            actionScanTimer.Stop();
            if (handler.ActiveTestSuite.Entities.Count != 0)
            {
                if (!handler.IsActive)
                    handler.Excute();

                actionScanTimer.Start();
                return;
            }

            Thread.Sleep(1000);

            if (closeOnFinish)
            {
                //Check failure Retries
                if (Singleton.Instance<ActionHandler>().Retries > 0)
                {
                    if (Singleton.Instance<ActionHandler>().ActiveTestSuite.Status != Enums.Status.Pass)
                    {
                        Singleton.Instance<ActionHandler>().ActiveTestSuite = FileHandler.ExtructTestSuiteFromFile(_activeSuitName);
                        Singleton.Instance<ActionHandler>().ActiveTestSuite.Name = _activeSuitName;

                        AutoApp.Logger.WriteInfoLog(string.Format("Current suite execution failed, performing suite retry {0}",Singleton.Instance<ActionHandler>().Retries), true);
                        Singleton.Instance<ActionHandler>().Retries--;
                        actionScanTimer.Start();
                        return;
                    }
                }
                   
                AutoApp.Logger.WriteInfoLog("Shutting Down AutoLunch");
                //closing the application
                manualReset.Set();
                //AutoApp.Logger.WriteInfoLog("AutoLunch releasing all modules");
                //  Singleton.Instance<SavedData>().ReleaseAll();
                return;
            }

            actionScanTimer.Start();
        }

        //====================== Important ========================================================
        //when received System.ServiceModel.AddressAccessDeniedException when activating the server
        //you should execute the server as administrator privileges
        //==========================================================================================
        public void StartClientServerWcfService()
        {
            string port = Singleton.Instance<AppSettings>().ClientServerPort.ToString();
            Uri httpUrl = new Uri("http://localhost:" + port + "/ServerCommunication");
            //Create ServiceHost
            ServiceHost host = new ServiceHost(typeof(ServerCommunication), httpUrl);
            //Add a service endpoint
            var wSHttpBinding = new WSHttpBinding();
            wSHttpBinding.MaxReceivedMessageSize = 2147483647;
            wSHttpBinding.MaxBufferPoolSize = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxDepth = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            host.AddServiceEndpoint(typeof(ICommunication), wSHttpBinding, "");
            // host.AddServiceEndpoint(typeof(ICommunication), new WSHttpBinding(), "");

            // Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;

            host.Description.Behaviors.Add(smb);
            //Start the Service
            host.Open();
            AutoApp.Logger.WriteInfoLog("Starting Auto Launch Server on port " + port);

            return;
            // string res = Console.ReadLine();

            //if (res.ToLower().Contains("exit"))
            //    {
            //        host.Close();
            //        return;
            //    }
            //using (ServiceHost host = new ServiceHost(typeof(AutomationServer.ServerCommunication)))
            //{
            //    host.Open();
            //    AutoApp.Logger.WriteInfoLog("Starting Auto Launch Server on port...");
            //    Console.ReadLine();
            //    host.Close();
            //}
        }

        public void StartRemoteServerWcfService()
        {
            string port = Singleton.Instance<AppSettings>().RemoteServerPort.ToString();
            Uri httpUrl = new Uri("http://localhost:" + port + "/ServerCommunication");
            //Create ServiceHost
            ServiceHost host = new ServiceHost(typeof(ServerCommunication), httpUrl);
            //Add a service endpoint
            var wSHttpBinding = new WSHttpBinding();
            wSHttpBinding.MaxReceivedMessageSize = 2147483647;
            wSHttpBinding.MaxBufferPoolSize = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxDepth = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            host.AddServiceEndpoint(typeof(ICommunication), wSHttpBinding, "");
            // host.AddServiceEndpoint(typeof(ICommunication), new WSHttpBinding(), "");

            // Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;

            host.Description.Behaviors.Add(smb);
            //Start the Service
            host.Open();
            AutoApp.Logger.WriteInfoLog("Starting Auto Launch Remote Server on port " + port);
            return;
        }
    }
}