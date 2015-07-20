using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using AutomationCommon;

namespace AutomationServer
{
    public class TestSuite : EntityBase
    {
        public string Name { get; set; }

        public ObservableCollection<TestSuiteEntity> Entities = new ObservableCollection<TestSuiteEntity>();

        public string Description { get; set; }
       
        public string TearDownScript { get; set; }
        
        public string ShortName()
        {
            return Name == null ? String.Empty : new FileInfo(Name).Name.Replace(StaticFields.SUITE_EXTENTION, string.Empty);
        }

        public override string ToString()
        {
            var sw = new System.IO.StringWriter();
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                writer.WriteStartDocument();
                writer.WriteStartElement("TestSuite");
                writer.WriteElementString("Description", Description);
                writer.WriteElementString("TearDownScript", TearDownScript);
                writer.WriteStartElement("Tests");
                foreach (TestSuiteEntity entity in Entities)
                    writer.WriteRaw(entity.ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            string retval = sw.ToString().Replace("utf-16", "utf-8"); ;
            sw.Close();
            sw.Dispose();
            return retval;
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog(string.Format("========= Start Executing TestSuite {0}  =========", Name));
            //   Status = Enums.Status.NoN;
            StartTime = DateTime.Now;
            //  AutoApp.Logger.WriteInfoLog(string.Format("##teamcity[testSuiteStarted name='{0}']", ShortName()));

            for (int index = 0; index < Entities.Count; index++)
            {
                //clearing variables before each test starts if the option in enabled
                if (AutoApp.Settings.ClearAllVars)
                    Singleton.Instance<SavedData>().Variables.Clear();

                TestSuiteEntity tse = Entities[index];
                Singleton.Instance<ActionHandler>().ProgressPercentage = (index * 100) / Entities.Count;
                Singleton.Instance<ActionHandler>().PauseStopOrContinueAction();

                //================= Stop Execution ==============================
                if (Singleton.Instance<ActionHandler>().StopExecution)
                {
                    AutoApp.Logger.WriteFailLog("Execution stopped by user request");
                    return;
                }

                //================ Test execution from suite ========================

                if (tse.Enable)
                {
                    AutoApp.Logger.WriteInfoLog(string.Format("========= Start Executing Test {0}  =========", tse.Name));

                    //AutoApp.Logger.WriteInfoLog(
                    //    string.Format("##teamcity[testStarted name='{0}' captureStandardOutput='false']", tse.Name));

                    Singleton.Instance<ActionHandler>().ActiveTestName = tse.Name;
                    tse.Execute();
                    tse.Status = tse.Test.Status;
                    if (tse.Status == Enums.Status.Pass)
                        AutoApp.Logger.WritePassLog(
                            string.Format("========= Finished Executing Test {0} with status {1} =========", tse.Name,
                                          tse.Status.ToString()));
                    else
                    {
                        AutoApp.Logger.WriteFailLog(string.Format("========= Finished Executing Test {0} with status {1} =========", tse.Name, tse.Status.ToString()));
                        string logName = AutoApp.Logger.GetLogFileName().Replace(".log", ".var");
                        AutoApp.Logger.WriteInfoLog(string.Format("Logging variables to file {0}", logName));
                        Singleton.Instance<SavedData>().ExportVariableToFile(logName);
                    }

                    if (tse.Status == Enums.Status.Fail)
                    {
                        Status = Enums.Status.Fail;
                        //      AutoApp.Logger.WriteInfoLog(string.Format( "##teamcity[testFailed name='{0}' ]",tse.Name));
                    }

                    //  AutoApp.Logger.WriteInfoLog(string.Format("##teamcity[testFinished name='{0}' captureStandardOutput='false']", tse.Name));

                    bool stopOrSkip = Singleton.Instance<ActionHandler>().StopSkipOrContinueOnFail(tse.Status);
                    if (stopOrSkip)
                    {
                        if (Singleton.Instance<ActionHandler>().OnFailureAction == Enums.OnFailerAction.Stop)
                        {
                            AutoApp.Logger.WriteFailLog("Stopping TestSuite execution on failure ");
                            break;
                        }
                    }
                }
                else
                {
                    tse.StartTime = DateTime.Now;
                    tse.EndTime = tse.StartTime;
                    tse.Status = Enums.Status.Skipped;
                    AutoApp.Logger.WriteSkipLog(string.Format("Skipping Test {0}", tse.Name));
                }

                //  AutoApp.Logger.WriteInfoLog(string.Format("##teamcity[testSuiteFinished name='{0}']", ShortName()));

                EndTime = DateTime.Now;
                //only if there are no failures on all stepentities then the script passes
                if (Status == Enums.Status.NoN)
                    Status = Enums.Status.Pass;

                //if (Status == Enums.Status.Fail)
                //    AutoApp.Logger.WriteFailLog(
                //        string.Format("========= Finished Executing TestSuite {0} with status {1} =========", Name,
                //                      Status.ToString()));
                //else
                //    AutoApp.Logger.WriteInfoLog(
                //        string.Format("========= Finished Executing TestSuite {0} with status {1} =========", Name,
                //                      Status.ToString()));
            }

            Singleton.Instance<ActionHandler>().ProgressPercentage = 100;
        }
    }
}