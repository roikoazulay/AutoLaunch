using System;
using System.Collections.Generic;
using AutomationCommon;

namespace AutomationServer.Actions
{
    //public class ScriptActionOld : ActionBase
    //{
    //    private Dictionary<string, LableObject> _lableList;
    //    private ActionType _type;
    //    private ActionData _actionData;
    //    ScriptObj _script;
    //    public enum ActionType
    //    {
    //        Execute
    //    }

    //    public ScriptActionOld()
    //        : base(Enums.ActionTypeId.ScriptExecute)
    //    {
    //    }

    //    private void UpdateLables()
    //    {
    //        _script = FileHandler.ExtructScriptFromFile(_actionData.Name);
    //        if (_script == null)
    //        {
    //            AutoApp.Logger.WriteFailLog(string.Format("Script {0} does not exist", _actionData.Name));
    //            return;
    //        }
    //        int stepEntetiesCount = _script.Entities.Count;
    //        for (int i = 0; i < stepEntetiesCount; i++)
    //        {
    //            StepEntity s = _script.Entities[i];
    //            if (s.Action.TypeId == Enums.ActionTypeId.Lable)
    //            {
    //                var lable = (LabelAction)s.Action;
    //                lable.LableList = _lableList;
    //                if (lable._type == LabelAction.ActionType.Create)
    //                    _lableList.Add(lable._actionData.LableName, new LableObject(lable._actionData.LableName, lable._actionData.Loops));

    //                // _lableList.Add(lable._actionData.LableName, new LableObject(lable._actionData.LableName, int.Parse(Singleton.Instance<SavedData>().GetVariableData(lable._actionData.Loops))));
    //            }
    //        }
    //    }

    //    public override void Execute()
    //    {
    //        _lableList = new Dictionary<string, LableObject>();
    //        UpdateLables();

    //        AutoApp.Logger.WriteInfoLog(string.Format("Starting Script execution {0}",_actionData.Name));
    //      //  ScriptObj s = FileHandler.ExtructScriptFromFile(_actionData.Name);

    //        int stepEntetiesCount = _script.Entities.Count;

    //        Singleton.Instance<SavedData>().UpdateParams(_actionData.Params);

    //        _script.StartTime = DateTime.Now;

    //        for (int i = 0; i < stepEntetiesCount; i++)
    //        {
    //            StepEntity en = _script.Entities[i];

    //            if (Singleton.Instance<ActionHandler>().PauseStopOrContinueAction())//on stop return /on pause will pause inside "PauseStopOrContinueAction" method ..
    //                return;

    //            if (en.Enable)
    //            {
    //                try
    //                {
    //                    en.Execute();//execute the action of the set entity
    //                }
    //                catch (Exception ex)
    //                {
    //                    AutoApp.Logger.WriteFatalLog(ex.ToString());
    //                }

    //                en.Status = en.Action.ActionStatus;
    //                if (en.Status == Enums.Status.Fail)
    //                    _script.Status = Enums.Status.Fail;

    //                ////On Failure Label
    //                //if (!string.IsNullOrEmpty(en.OnFailureLabel))
    //                //{
    //                //    Singleton.Instance<ActionHandler>().Flowhandler.FindLableIndex(en.OnFailureLabel);
    //                //    AutoApp.Logger.WriteWarningLog(string.Format("Action failed ...Change status to Passed - GoTo Label exist {0}", en.OnFailureLabel));
    //                //    _script.Status = Enums.Status.Pass;
    //                //}
    //                //else
    //                //{
    //                    //validate action status and response
    //                    bool stopOrSkip = Singleton.Instance<ActionHandler>().StopSkipOrContinueOnFail(en.Action.ActionStatus);
    //                    if (stopOrSkip)
    //                        break;
    //               // }

    //                //if go to was called ,jump to the label index
    //                if (Singleton.Instance<ActionHandler>().Flowhandler.ActivateGoTo())
    //                {
    //                    i = Singleton.Instance<ActionHandler>().Flowhandler.LableIndex;
    //                    AutoApp.Logger.WriteInfoLog(string.Format("GoTo Label was activated, Skipping to step number {0}", i));
    //                    i -= 1;//decrement by 1
    //                    Singleton.Instance<ActionHandler>().Flowhandler.LableIndex = -1;//reseting the index for next step
    //                }

    //            }
    //            else
    //            {
    //                _script.Status = Enums.Status.Skipped;
    //                AutoApp.Logger.WriteSkipLog(string.Format("Skipping step number {0}", i + 1));
    //            }

    //        }

    //        _script.EndTime = DateTime.Now;
    //        //only if there are no failures on all step entities then the script passes

    //        if (_script.Status == Enums.Status.NoN)
    //              ActionStatus = Enums.Status.Pass;
    //        else if (_script.Status == Enums.Status.Pass)
    //            ActionStatus = Enums.Status.Pass;
    //        AutoApp.Logger.WriteInfoLog(string.Format("Finished Group Steps execution {0}", _actionData.Name));
    //    }

    //    public ScriptActionOld(ActionType type, ActionData actionData)
    //        : base(Enums.ActionTypeId.ScriptExecute)
    //    {
    //        _actionData = actionData;
    //        _type = type;

    //        Details.Add(type.ToString());
    //        Details.Add(_actionData.Name); //1
    //        Details.Add(_actionData.Params); //1
    //    }

    //    public override void Construct()
    //    {
    //        _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
    //        _actionData = new ActionData() { Name = Details[1], Params = Details.Count>2?Details[2]:string.Empty };
    //    }

    //    public struct ActionData
    //    {
    //        public string Name { get; set; } //1
    //        public string Params { get; set; } //2
    //    }
    //}

    public class ScriptAction : ActionBase
    {
        private Dictionary<string, LableObject> _lableList;
        private ActionType _type;
        private ActionData _actionData;
        private ScriptObj _script;

        public enum ActionType
        {
            Execute
        }

        public ScriptAction()
            : base(Enums.ActionTypeId.ScriptExecute)
        {
        }

        private void UpdateLables()
        {
            _script = FileHandler.ExtructScriptFromFile(_actionData.Name);
            if (_script == null)
            {
                AutoApp.Logger.WriteFailLog(string.Format("Script {0} does not exist", _actionData.Name));
                return;
            }
            int stepEntetiesCount = _script.Entities.Count;
            for (int i = 0; i < stepEntetiesCount; i++)
            {
                StepEntity s = _script.Entities[i];
                if (s.Action.TypeId == Enums.ActionTypeId.Lable)
                {
                    var lable = (LabelAction)s.Action;
                    lable.LableList = _lableList;
                    if (lable._type == LabelAction.ActionType.Create)
                        _lableList.Add(lable._actionData.LableName, new LableObject(lable._actionData.LableName, lable._actionData.Loops));

                    // _lableList.Add(lable._actionData.LableName, new LableObject(lable._actionData.LableName, int.Parse(Singleton.Instance<SavedData>().GetVariableData(lable._actionData.Loops))));
                }
            }
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog(string.Format("Starting Group Steps execution {0}", _actionData.Name));

            Singleton.Instance<SavedData>().UpdateParams(_actionData.Params);
            _script = FileHandler.ExtructScriptFromFile(Singleton.Instance<SavedData>().GetVariableData(_actionData.Name));
            AutoApp.Logger.WriteInfoLog(string.Format("Starting script execution: {0}", Singleton.Instance<SavedData>().GetVariableData(_actionData.Name)));
            _script.Execute();

            if (_script.Status == Enums.Status.NoN)
                ActionStatus = Enums.Status.Pass;
            else if (_script.Status == Enums.Status.Pass)
                ActionStatus = Enums.Status.Pass;
            AutoApp.Logger.WriteInfoLog(string.Format("Finished Group Steps execution {0}", _actionData.Name));
        }

        public ScriptAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.ScriptExecute)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.Name); //1
            Details.Add(_actionData.Params); //1
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { Name = Details[1], Params = Details.Count > 2 ? Details[2] : string.Empty };
        }

        public struct ActionData
        {
            public string Name { get; set; } //1

            public string Params { get; set; } //2
        }
    }
}