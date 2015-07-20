using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutomationCommon;

namespace AutomationServer
{
    public class ServerCommunication : ICommunication
    {
        public bool IsActive()
        {
            return Singleton.Instance<ActionHandler>().IsActive;
        }

        public void ExecuteSuite(string suiteName, int cycles, Enums.OnFailerAction action)
        {
            AutoApp.Logger.WriteInfoLog(string.Format("Server received activate command for Suite Name {0}", suiteName), true);
            if (!CommonHelper.FileExist(suiteName))
            {
                AutoApp.Logger.WriteFailLog(string.Format("Suite file {0} does not exist", suiteName, true));
                return;
            }
            TestSuite t = FileHandler.ExtructTestSuiteFromFile(suiteName);
            t.Name = suiteName;
            Singleton.Instance<ActionHandler>().OnFailureAction = action;
            Singleton.Instance<ActionHandler>().Cycles = cycles == 0 ? int.MaxValue : cycles;//setting the cycles
            Singleton.Instance<ActionHandler>().ActiveTestSuite = t;
        }

        public List<ClientMessage> GetMailBox()
        {
            return Singleton.Instance<ClientReportMailBox>().GetMailBox();
        }

        public bool IsConnected()
        {
            AutoApp.Logger.WriteInfoLog("Server received connection request", true);
            return true;
        }

        public void StopExecution()
        {
            AutoApp.Logger.WriteInfoLog("Server received Stop Execution request", true);
            Singleton.Instance<ActionHandler>().StopExecution = true;

            if (Singleton.Instance<ActionHandler>().ActiveStep != null)
            {
                if (Singleton.Instance<ActionHandler>().ActiveStep.Action.TypeId == Enums.ActionTypeId.Sleep)
                    ((SleepAction)Singleton.Instance<ActionHandler>().ActiveStep.Action).StopSleep();

                Singleton.Instance<ActionHandler>().ActiveStep = null;
            }
            //foreach (var s in Singleton.Instance<ActionHandler>().ActiveScript.Entities)
            //{
            //    try
            //    {
            //        if (s.Action.TypeId == Enums.ActionTypeId.Sleep)
            //            ((SleepAction)s.Action).StopSleep();
            //    }
            //    catch
            //    {
            //    }
            //}

            new System.Threading.ManualResetEvent(false).Set();
        }

        public void PauseExecution()
        {
            AutoApp.Logger.WriteInfoLog("Server received Pause Execution request", true);
            Singleton.Instance<ActionHandler>().PauseExecution = !Singleton.Instance<ActionHandler>().PauseExecution;
        }

        public int GetCycles()
        {
            return Singleton.Instance<ActionHandler>().ActivatedCycles;
        }

        public int GetProgressPercentage()
        {
            return Singleton.Instance<ActionHandler>().ProgressPercentage;
        }

        public SuiteProgressInfo GetSuiteProgressInfo()
        {
            var suiteProgressInfo = new SuiteProgressInfo
                        {
                            SuiteName = Singleton.Instance<ActionHandler>().ActiveTestSuite.ShortName(),
                            PassedCycles = Singleton.Instance<ActionHandler>().ActivatedCycles.ToString(),
                            SuiteProgressPersantage = Singleton.Instance<ActionHandler>().ProgressPercentage.ToString(),
                            ActiveTestName = Singleton.Instance<ActionHandler>().ActiveTestName,
                        };

            return suiteProgressInfo;
        }

        public List<string> GetVariablesNames()
        {
            List<string> retval = new List<string>();
            retval.AddRange(Singleton.Instance<SavedData>().Variables.Keys.ToList<string>());
            retval.AddRange(Singleton.Instance<SavedData>().ListObj.Keys.ToList<string>());
            retval.AddRange(Singleton.Instance<SavedData>().DictionaryListObj.Keys.ToList<string>());
            return retval;
            // return Singleton.Instance<SavedData>().Variables.Keys.ToList<string>();
        }

        public string GetVariableData(string variableName)
        {
            AutoApp.Logger.WriteInfoLog("Server received Get Variable Data request", true);
            string data = string.Empty;
            if (Singleton.Instance<SavedData>().Variables.ContainsKey(variableName))
                data = Singleton.Instance<SavedData>().Variables[variableName].GetValue();
            else if (Singleton.Instance<SavedData>().ListObj.ContainsKey(variableName))
                data = string.Join(System.Environment.NewLine, Singleton.Instance<SavedData>().ListObj[variableName].ToArray());
            else if (Singleton.Instance<SavedData>().DictionaryListObj.ContainsKey(variableName))
            {
                StringBuilder sb = new StringBuilder();
                var dict = Singleton.Instance<SavedData>().DictionaryListObj[variableName];
                foreach (string k in dict.Keys)
                {
                    string s = k + "-" + dict[k];
                    sb.AppendLine(s);
                }
                data = sb.ToString();
            }

            return data;
        }

        public string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public string GetActiveStepInfo()
        {
            return Singleton.Instance<ActionHandler>().GetActiveStepInfo();
            //string retVal = string.Empty;
            //try
            //{
            //    var step = Singleton.Instance<ActionHandler>().ActiveStep;
            //    if (step == null)
            //        return retVal;
            //    if (step.Action.TypeId == Enums.ActionTypeId.Sleep)
            //        retVal = string.Format("Sleep Action finishes in {0} seconds", ((SleepAction)step.Action).GetRemainingDuration());
            //}
            //catch
            //{
            //}

            //return retVal;
        }

        public void SetBreakPointList(List<BreakPointObj> bpList, bool isEnable)
        {
            Singleton.Instance<ActionHandler>().BreakPoints = bpList;
            Singleton.Instance<ActionHandler>().BreakPointsEnable = isEnable;
        }

        public string GetLastExecutionStatus()
        {
            AutoApp.Logger.WriteInfoLog("Server received Get Last Execution Status request", true);
            return Singleton.Instance<ActionHandler>().ActiveTestSuite.Status.ToString();
        }
    }
}