using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;
using AutomationCommon;

namespace AutomationServer
{
    public class ScriptObj : EntityBase
    {
        public ObservableCollection<StepEntity> Entities = new ObservableCollection<StepEntity>();

        public string Description { get; set; }

        public string Version { get; set; }

        public override string ToString()
        {
            var sw = new System.IO.StringWriter();
            using (var writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                writer.WriteStartDocument();
                writer.WriteStartElement("Script");
                //  writer.WriteElementString("Enable", Enable.ToString());
                writer.WriteElementString("Description", Description);
                writer.WriteStartElement("Steps");
                foreach (StepEntity entity in Entities)
                    writer.WriteRaw(entity.ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            string retval = sw.ToString().Replace("utf-16", "utf-8");
            sw.Close();
            sw.Dispose();
            return retval;
        }

        private Dictionary<string, LableObject> _lableList;
        public int NextepIndex = 0;//this can be updated when label goto action is executed

        public  void Execute1()
        {
            _lableList = new Dictionary<string, LableObject>();
            UpdateLables();
            StartTime = DateTime.Now;

            int stepEntetiesCount = Entities.Count;

            //clear labels before starting each script - (now you can duplicate labels between scripts)
            // label fix new version
            //  Singleton.Instance<ActionHandler>().savedData.LableList.Clear();

            for (int i = NextepIndex; i < stepEntetiesCount; i++)
            {
                StepEntity s = Entities[i];

                if (Singleton.Instance<ActionHandler>().PauseStopOrContinueAction())//on stop return /on pause will pause inside "PauseStopOrContinueAction" method ..
                    return;

                //BreakPoint Handling
                if (Singleton.Instance<ActionHandler>().BreakPointsEnable)
                    BreakPointExecute(i + 1);

                if (s.Enable)
                {
                    AutoApp.Logger.ActiveStepIndex = i;
                    Singleton.Instance<ActionHandler>().ActiveStep = s;//save the active step for future use (sleep duration for example)
                    try
                    {
                        if (s.Action.GetType() == typeof(LabelAction))
                        {
                            LableExecute(s, i);
                            i = NextepIndex;
                        }
                        else if (s.Action.GetType() == typeof(ConditionAction))
                        {
                            ((ConditionAction)s.Action).ActiveScript = this;
                            s.Execute();//execute the action of the set entity
                            if (NextepIndex != -1)
                               i = NextepIndex;
                        }
                        else
                            s.Execute();//execute the action of the set entity
                    }
                    catch (Exception ex)
                    {
                        AutoApp.Logger.WriteFatalLog(ex.ToString());
                        s.Status = Enums.Status.Fail;
                    }

                    s.Status = s.Action.ActionStatus;
                    if (s.Status == Enums.Status.Fail)
                        Status = Enums.Status.Fail;

                    ////On Failure Label
                    if (!string.IsNullOrEmpty(s.OnFailureLabel))
                    {
                        LableExecute(GetLableByName(s.OnFailureLabel), i);
                        i = NextepIndex;

                        AutoApp.Logger.WriteWarningLog(string.Format("Action failed ...Change status to Passed - GoTo Label exist {0}", s.OnFailureLabel));
                        Status = Enums.Status.Pass;
                    }

                    //validate action status and response
                    bool stopOrSkip = Singleton.Instance<ActionHandler>().StopSkipOrContinueOnFail(s.Action.ActionStatus);
                    if (stopOrSkip)
                        break;
                    //}

                    if (s.Action.GetType() == typeof(LabelAction))
                    {
                        if (((LabelAction)s.Action)._type == LabelAction.ActionType.GoTo)
                        {
                            AutoApp.Logger.WriteInfoLog(string.Format("GoTo Label was activated, Skipping to step number {0}", NextepIndex));
                        }
                    }
                }
                else
                {
                    s.Status = Enums.Status.Skipped;
                    AutoApp.Logger.WriteSkipLog(string.Format("Skipping step number {0}", i + 1));
                }
            }

            EndTime = DateTime.Now;
            //only if there are no failures on all step entities then the script passes
            if (Status == Enums.Status.NoN)
                Status = Enums.Status.Pass;
        }

        public override void Execute()
        {
            _lableList = new Dictionary<string, LableObject>();
            UpdateLables();
            StartTime = DateTime.Now;

            int stepEntetiesCount = Entities.Count;

            //clear labels before starting each script - (now you can duplicate labels between scripts)
            // label fix new version
            //  Singleton.Instance<ActionHandler>().savedData.LableList.Clear();

            for (int i = NextepIndex; i < stepEntetiesCount; i++)
            {
                StepEntity s = Entities[i];

                if (Singleton.Instance<ActionHandler>().PauseStopOrContinueAction())//on stop return /on pause will pause inside "PauseStopOrContinueAction" method ..
                    return;

                //BreakPoint Handling
                if (Singleton.Instance<ActionHandler>().BreakPointsEnable)
                    BreakPointExecute(i + 1);

                if (s.Enable)
                {
                    AutoApp.Logger.ActiveStepIndex = i;
                    Singleton.Instance<ActionHandler>().ActiveStep = s;//save the active step for future use (sleep duration for example)
                    try
                    {
                        if (s.Action.GetType() == typeof(LabelAction))
                        {
                            LableExecute(s, i);
                            i = NextepIndex;
                        }
                        else if (s.Action.GetType() == typeof(ConditionAction))
                        {
                            ((ConditionAction)s.Action).ActiveScript = this;
                            s.Execute();

                            //check if we need to go to label
                            if (((ConditionAction)s.Action).GoToLableshouldActivated)
                            {
                                var sss = GetLableByName(((ConditionAction)s.Action)._actionData.Lable);
                                i=  LableExecute(sss, i);
                            }
                        }
                        else
                            s.Execute();//execute the action of the set entity
                    }
                    catch (Exception ex)
                    {
                        AutoApp.Logger.WriteFatalLog(ex.ToString());
                        s.Status = Enums.Status.Fail;
                    }

                    s.Status = s.Action.ActionStatus;
                    if (s.Status == Enums.Status.Fail)
                        Status = Enums.Status.Fail;

                    ////On Failure Label
                    if (!string.IsNullOrEmpty(s.OnFailureLabel))
                    {
                        LableExecute(GetLableByName(s.OnFailureLabel), i);
                        i = NextepIndex;

                        AutoApp.Logger.WriteWarningLog(string.Format("Action failed ...Change status to Passed - GoTo Label exist {0}", s.OnFailureLabel));
                        Status = Enums.Status.Pass;
                    }

                    //validate action status and response
                    bool stopOrSkip = Singleton.Instance<ActionHandler>().StopSkipOrContinueOnFail(s.Action.ActionStatus);
                    if (stopOrSkip)
                        break;
                    //}

                    if (s.Action.GetType() == typeof(LabelAction))
                    {
                        if (((LabelAction)s.Action)._type == LabelAction.ActionType.GoTo)
                        {
                            AutoApp.Logger.WriteInfoLog(string.Format("GoTo Label was activated, Skipping to step number {0}", NextepIndex));
                        }
                    }
                }
                else
                {
                    s.Status = Enums.Status.Skipped;
                    AutoApp.Logger.WriteSkipLog(string.Format("Skipping step number {0}", i + 1));
                }
            }

            EndTime = DateTime.Now;
            //only if there are no failures on all step entities then the script passes
            if (Status == Enums.Status.NoN)
                Status = Enums.Status.Pass;
        }

        private void UpdateLables()
        {
            int stepEntetiesCount = Entities.Count;
            for (int i = 0; i < stepEntetiesCount; i++)
            {
                StepEntity s = Entities[i];
                if (s.Action.TypeId == Enums.ActionTypeId.Lable)
                {
                    var lable = (LabelAction)s.Action;
                    lable.LableList = _lableList;
                    if (lable._type == LabelAction.ActionType.Create)
                    {
                        if (_lableList.ContainsKey(lable._actionData.LableName))
                            AutoApp.Logger.WriteFatalLog(string.Format("Label Name already exist {0}", lable._actionData.LableName));
                        else
                            _lableList.Add(lable._actionData.LableName, new LableObject(lable._actionData.LableName, lable._actionData.Loops));
                    }

                    // _lableList.Add(lable._actionData.LableName, new LableObject(lable._actionData.LableName, int.Parse(Singleton.Instance<SavedData>().GetVariableData(lable._actionData.Loops))));
                }
            }
        }

        private StepEntity GetLableByName(string lableName)
        {
            StepEntity sl = null;
            int stepEntetiesCount = Entities.Count;
            for (int i = 0; i < stepEntetiesCount; i++)
            {
                StepEntity s = Entities[i];
                if (s.Action.TypeId == Enums.ActionTypeId.Lable)
                {
                    var lable = (LabelAction)s.Action;
                    if (lable._actionData.LableName == lableName)
                    {
                        //building label action
                        LabelAction lb = new LabelAction(LabelAction.ActionType.GoTo, new LabelAction.ActionData { LableName = lable._actionData.LableName, Loops = lable._actionData.Loops });
                        sl = new StepEntity(lb);
                        break;
                    }
                }
            }

            return sl;
        }

        //this update the next set if lable goto is activated
        public int LableExecute(StepEntity s, int currentStepIndex)
        {
            s.StartTime = DateTime.Now;
            s.Action.ActionStatus = Enums.Status.Fail;
            var acc = (LabelAction)s.Action;
            AutoApp.Logger.WriteInfoLog("Starting Label Action " + acc._type.ToString());

            switch (acc._type.ToString())
            {
                case "Create":
                    NextepIndex = currentStepIndex;
                    //update the loop count of the lable on create since it can be a variable which update before the lable is created
                    _lableList[acc._actionData.LableName].LoopCount = int.Parse(Singleton.Instance<SavedData>().GetVariableData(_lableList[acc._actionData.LableName]._loopCountStr));
                    AutoApp.Logger.WritePassLog(string.Format("Label {0} created", _lableList[acc._actionData.LableName].LableName));
                    s.Action.ActionStatus = Enums.Status.Pass;
                    break;

                case "GoTo":
                    if (!_lableList.ContainsKey(acc._actionData.LableName))
                    {
                        AutoApp.Logger.WriteFailLog(string.Format("Label {0} does not exist", acc._actionData.LableName));
                    }

                    AutoApp.Logger.WriteInfoLog(string.Format("Label {0} current loop is {1}", acc._actionData.LableName, _lableList[acc._actionData.LableName].CurrentLoopCount));

                    if (_lableList[acc._actionData.LableName].CurrentLoopCount < _lableList[acc._actionData.LableName].LoopCount)
                    {
                        Singleton.Instance<ActionHandler>().Flowhandler.FindLableIndex(acc._actionData.LableName, this);
                        //should fix here to send the label list of the  script which called
                        if (NextepIndex != -1)
                        {
                            currentStepIndex = NextepIndex;
                            _lableList[acc._actionData.LableName].CurrentLoopCount++;
                            AutoApp.Logger.WritePassLog(string.Format("Found Label index {0} for label name {1} ,Loop Count {2}",
                                                                     NextepIndex, acc._actionData.LableName, _lableList[acc._actionData.LableName].CurrentLoopCount.ToString()));
                            s.Action.ActionStatus = Enums.Status.Pass;
                        }
                        else
                            AutoApp.Logger.WriteFailLog(string.Format("Label {0} was not found", acc._actionData.LableName));
                    }
                    else
                    {
                        NextepIndex = currentStepIndex;
                        AutoApp.Logger.WritePassLog(string.Format("Label {0} reached final loop count {1}", _lableList[acc._actionData.LableName].LableName, _lableList[acc._actionData.LableName].CurrentLoopCount));
                        s.Action.ActionStatus = Enums.Status.Pass;
                    }
                    break;
            }

            if (s.Action.ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("Label Action " + acc._type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("Label Action " + acc._type.ToString() + " Failed");

            s.EndTime = DateTime.Now;

            return currentStepIndex;
        }

        private void BreakPointExecute(int index)
        {
            string scriptName = Singleton.Instance<ActionHandler>().ActiveScriptName;
            var bpoints = Singleton.Instance<ActionHandler>().BreakPoints;
            var bko = (from bk in bpoints where bk.SriptName.Contains(scriptName) select bk).FirstOrDefault();
            if (bko != null)
            {
                if (bko.GetStepsIndexs().IndexOf(index) != -1)
                {
                    AutoApp.Logger.WriteInfoLog("Break Point execute, press enter to continue", true);
                    Console.ReadLine();
                }
            }
        }
    }
}