using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using AutomationCommon;
using AutomationServer.Modules.Reports;

namespace AutomationServer
{
    public class ActionHandler
    {
        public List<BreakPointObj> BreakPoints { get; set; }

        public bool BreakPointsEnable { get; set; }

        public string ActiveScriptName { get; set; }//holds the current Script name (used for breakpoints)

        public FlowHandler Flowhandler;//flow handler for conditions & lable action

        public bool RestartExecution { get; set; }

        public bool StopExecution { get; set; }

        public bool PauseExecution { get; set; }

        public BackgroundWorker bw = new BackgroundWorker();

        public int ActiveActionIndex { get; private set; }

        public int Cycles { get; set; }//will hold the cycle times for the suite to run- default: one time

        public int Retries { get; set; }//will hold the retries count

        public StepEntity ActiveStep { get; set; }

        public ScriptObj ActiveScript { get; set; }

        public TestSuite ActiveTestSuite { get; set; }

        public int ActivatedCycles { get; private set; }//returns the current cycle which was activated

        public Enums.OnFailerAction OnFailureAction { get; set; }

        private readonly ReportManager ReportMgr = new ReportManager(new HtmlReportGenerator());

        public int ProgressPercentage { get; set; }//holds the current suite persantage progress

        public string ActiveTestName { get; set; }//holds the current test name

        public SavedData savedData { get; private set; }//holds All saved date (variables)

        public bool IsActive
        {
            get { return bw.IsBusy; }
        }

        private ActionHandler()
        {
            BreakPoints = new List<BreakPointObj>();
            Flowhandler = Singleton.Instance<FlowHandler>();
            savedData = Singleton.Instance<SavedData>();
            OnFailureAction = Enums.OnFailerAction.SkipTest;
            Cycles = 1;//default: one time
            ActiveTestSuite = new TestSuite();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        public void StopBwExecution()
        {
            bw.CancelAsync();
        }

        public void Excute()
        {
            StopExecution = false;
            PauseExecution = false;
            RestartExecution = false;
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Singleton.Instance<AppSettings>().Refresh();
            var worker = sender as BackgroundWorker;

            if ((worker.CancellationPending == true))
            {
                e.Cancel = true;
                return;
            }
            else
            {
                for (int loopTimes = 0; loopTimes < Cycles; loopTimes++)
                {
                    //================= Stop Execution ==============================
                    if (Singleton.Instance<ActionHandler>().StopExecution)
                        break;

                    Singleton.Instance<ActionHandler>().savedData.ReleaseAll();

                    //setting new log file name for each suite cycle
                    string fileName = string.Format("{0:dd-MM-yyyy_HH-mm-ss}_{1}{2}", DateTime.Now,
                                                    ActiveTestSuite.ShortName(), ".log");
                    AutoApp.Logger.SetLogFileName(StaticFields.LOG_PATH + "\\" + fileName);

                    //=============================================================================
                    ProgressPercentage = 0;
                    ActivatedCycles = loopTimes + 1;
                    AutoApp.Logger.WriteInfoLog(string.Format("Starting Cycle {0}", ActivatedCycles));
                    ActiveTestSuite.Execute();

                    //============= RestartExecution ============
                    if (Singleton.Instance<ActionHandler>().RestartExecution)
                    {
                        AutoApp.Logger.WriteInfoLog("Restarting execution");
                        Singleton.Instance<ActionHandler>().RestartExecution = false;
                    }

                    //stop loop on failure
                    else if (StopSkipOrContinueOnFail(ActiveTestSuite.Status))
                    {
                        AutoApp.Logger.WriteInfoLog(string.Format("Ending execution after {0} cycles", loopTimes + 1));
                        loopTimes = Cycles - 1;
                    }

                    if (!string.IsNullOrEmpty(ActiveTestSuite.TearDownScript))
                    {
                        AutoApp.Logger.WriteInfoLog(string.Format("Executing Suite TearDown Script: {0}", ActiveTestSuite.TearDownScript), true);
                        ScriptObj ts = FileHandler.ExtructScriptFromFile(ActiveTestSuite.TearDownScript);
                        ts.Execute();
                        AutoApp.Logger.WriteInfoLog(string.Format("Suite TearDown Script: {0} Completed", ActiveTestSuite.TearDownScript), true);
                    }

                    //Generate Report
                    AutoApp.Logger.CloseLogFile();
                    string report = ReportMgr.GenerateReport(ActiveTestSuite);
                    CommonHelper.SaveStringToFile(report, AutoApp.Logger.GetLogFileName().Replace(".log", ".htm"));
                    CommonHelper.AppandStringToFile(ActiveTestSuite.StartTime + "," + Singleton.Instance<ActionHandler>().ActiveTestSuite.ShortName() + "," + ActiveTestSuite.Status + "," + ActiveTestSuite.ExecutionDuration(), AutoApp.Settings.ApplicationDirectory + "Result.log");

                   
                }

                if (!string.IsNullOrEmpty(Singleton.Instance<AppSettings>().TearDownScript))
                {
                    bool shouldStopExecution = Singleton.Instance<ActionHandler>().StopExecution;

                    //if execution stopped by user request , disable it for tear down to run
                    if (shouldStopExecution)
                        Singleton.Instance<ActionHandler>().StopExecution = false;

                    AutoApp.Logger.WriteInfoLog(string.Format("Executing Global TearDown Script: {0}", Singleton.Instance<AppSettings>().TearDownScript), true);
                    ScriptObj s = FileHandler.ExtructScriptFromFile(Singleton.Instance<AppSettings>().TearDownScript);

                    s.Execute();
                    //if the teardown was activated because of failure of stopped by user request do not restart it
                    if (shouldStopExecution)
                        Singleton.Instance<ActionHandler>().StopExecution = true;

                    AutoApp.Logger.WriteInfoLog(string.Format("Global TearDown Script: {0} Completed", Singleton.Instance<AppSettings>().TearDownScript), true);
                }
            }

            //!!!!!!!!!important !!!!!! clear all test entities when finish
           // ActiveTestSuite.Entities.Clear();

            Release();
        }

        private void Release()
        {
            AutoApp.Logger.WriteInfoLog("Release all resources", true);
            //!!!!!!!!!important !!!!!! clear all test entities when finish
            ActiveTestSuite.Entities.Clear();
            //foreach (var serial in Singleton.Instance<SavedData>().SerialCommunications.Keys)
            //{
            //    try{
            //            AutoApp.Logger.WriteInfoLog(string.Format("Disconnect serial port {0}",Singleton.Instance<SavedData>().SerialCommunications[serial].Port));
            //            Singleton.Instance<SavedData>().SerialCommunications[serial].DisConnect();
            //    }
            //    catch
            //    {}
            //}
            AutoApp.Logger.WriteInfoLog("Release all resources completed",true);
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                // "Canceled!";
            }
            else if (!(e.Error == null))
            {
                //"Error:
            }
            else
            {
                // "Done!";
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // ProgressPercentage = e.ProgressPercentage;
        }

        public bool PauseStopOrContinueAction()
        {
            //on stop break the test flow
            if (Singleton.Instance<ActionHandler>().StopExecution)
                return true;

            //================= pause Execution ==============================
            if (PauseExecution)
            {
                AutoApp.Logger.WriteInfoLog("Pausing Execution started");

                while (PauseExecution)
                {
                    if (StopExecution)
                        break;
                    Thread.Sleep(1000);
                }
                AutoApp.Logger.WriteInfoLog("Pausing Execution stopped");
            }

            return false;
        }

        public bool StopSkipOrContinueOnFail(Enums.Status actionStatus)
        {
            if (Singleton.Instance<ActionHandler>().RestartExecution)
                return true;
            if (actionStatus == Enums.Status.Fail)
            {
                if (OnFailureAction == Enums.OnFailerAction.Continue)
                    return false;

                if ((OnFailureAction == Enums.OnFailerAction.SkipTest) || (OnFailureAction == Enums.OnFailerAction.Stop))
                    return true;
            }

            return false;
        }

        public string GetActiveStepInfo()
        {
            if (!Singleton.Instance<AppSettings>().ShowStepInfo)
                return string.Empty;

            string retVal = string.Empty;
            try
            {
                var step = Singleton.Instance<ActionHandler>().ActiveStep;
                if (step == null)
                    return retVal;
                if (step.Action.TypeId == Enums.ActionTypeId.Sleep)
                    retVal = string.Format("Sleep Action finishes in {0} seconds", ((SleepAction)step.Action).GetRemainingDuration());
            }
            catch
            {
            }

            return retVal;
        }
    }
}