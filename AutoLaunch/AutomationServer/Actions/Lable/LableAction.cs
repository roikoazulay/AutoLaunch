using System;
using System.Collections.Generic;

using AutomationCommon;

namespace AutomationServer
{
    //public class LableObj
    //{
    //    public string LableName { get; set; }
    //    public int LoopCount { get; set; }
    //    public int CurrentLoopCount { get; set; }
    //    public LableObj(string lableName, int loopCount)
    //    {
    //        LableName = lableName;
    //        LoopCount = loopCount;
    //    }
    //}

    public class LabelAction : ActionBase
    {
        public Dictionary<string, LableObject> LableList { get; set; }

        public ActionType _type;
        public ActionData _actionData;

        public enum ActionType
        {
            Create, GoTo
        }

        public struct ActionData
        {
            public string LableName { get; set; } //1

            public string Loops { get; set; } //2
        }

        public LabelAction()
            : base(Enums.ActionTypeId.Lable)
        {
        }

        public LabelAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.Lable)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.LableName);
            Details.Add(_actionData.Loops);
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog("Starting Label Action " + _type.ToString());

            //update the counter on the first time
            if (LableList[_actionData.LableName].LoopCount < 0)
            {
                try
                {
                    // LableList[_actionData.LableName].LoopCount = int.Parse(Singleton.Instance<SavedData>().GetVariableData(LableList[_actionData.LableName].LoopCountStr));
                    //  LableList[_actionData.LableName].LoopCount = int.Parse(Singleton.Instance<SavedData>().GetVariableData(LableList[_actionData.LableName].LoopCountStr));
                }
                catch (Exception ex)
                {
                    AutoApp.Logger.WriteFatalLog(ex.ToString());
                }
            }

            switch (_type)
            {
                case ActionType.Create:
                    AutoApp.Logger.WritePassLog(string.Format("Label {0} created", LableList[_actionData.LableName].LableName));
                    ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.GoTo:
                    if (!LableList.ContainsKey(_actionData.LableName))
                    {
                        AutoApp.Logger.WriteFailLog(string.Format("Label {0} does not exist", _actionData.LableName));
                        return;
                    }

                    AutoApp.Logger.WriteInfoLog(string.Format("Label {0} current loop is {1}", _actionData.LableName, LableList[_actionData.LableName].CurrentLoopCount));

                    if (LableList[_actionData.LableName].CurrentLoopCount < LableList[_actionData.LableName].LoopCount)
                    {
                        ////should fix here to send the label list of the  script which called
                        //if (Singleton.Instance<ActionHandler>().Flowhandler.FindLableIndex(_actionData.LableName))
                        //{
                        //    LableList[_actionData.LableName].CurrentLoopCount++;
                        //    AutoApp.Logger.WritePassLog(string.Format("Found Label index {0},Loop Count {1}",
                        //                                              Singleton.Instance<ActionHandler>().Flowhandler.LableIndex, LableList[_actionData.LableName].CurrentLoopCount.ToString()));
                        //    ActionStatus = Enums.Status.Pass;
                        //}
                        //else
                        //{
                        //    AutoApp.Logger.WriteFailLog(string.Format("Label {0} was not found on script {1}", _actionData.LableName,
                        //                                              Singleton.Instance<ActionHandler>().Flowhandler.ActiveScriptEntity.
                        //                                                  ShortName()));
                        //}
                    }
                    else
                    {
                        AutoApp.Logger.WritePassLog(string.Format("Label {0} reached final loop count {1}", LableList[_actionData.LableName].LableName, LableList[_actionData.LableName].CurrentLoopCount));
                        ActionStatus = Enums.Status.Pass;
                    }
                    break;
            }

            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("Label Action " + _type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("Label Action " + _type.ToString() + " Failed");
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { LableName = Details[1], Loops = Details.Count > 2 ? Details[2] : string.Empty };
        }
    }

    public class LableObject
    {
        public string LableName { get; set; }

        // public int LoopCount { get; set; }
        private int _loopCount;

        public string _loopCountStr { get; set; }

        public int CurrentLoopCount { get; set; }

        public LableObject(string lableName, string loopCountStr)
        {
            _loopCount = -1000;
            LableName = Singleton.Instance<SavedData>().GetVariableData(lableName);
            _loopCountStr = loopCountStr;

            // LoopCount = int.Parse(loopCountStr);
        }

        public int LoopCount
        {
            get
            {
                if (_loopCount < 0)
                    _loopCount = int.Parse(Singleton.Instance<SavedData>().GetVariableData(_loopCountStr));
                return _loopCount;
            }

            set
            {
                _loopCount = value;
            }
        }
    }
}