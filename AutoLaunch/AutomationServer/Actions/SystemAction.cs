using System;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class SystemAction : ActionBase
    {
        private ActionData _actionData;
        private ActionType _type;

        public SystemAction()
            : base(Enums.ActionTypeId.SystemAction)
        {
        }

        public SystemAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.SystemAction)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.TargetVar); //1
        }

        public enum ActionType
        {
            GetSuiteStatus,
            StopExecutionAndFail,
            StopExecution,
            RestartExecution
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { TargetVar = Details[1] };
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog("System Action " + _type.ToString());
            var sts = Singleton.Instance<ActionHandler>().ActiveTestSuite;
            switch (_type)
            {
                case ActionType.GetSuiteStatus:
                    Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(sts.Status.ToString());
                    ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.StopExecution:
                    Singleton.Instance<ActionHandler>().StopExecution = true;
                    ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.StopExecutionAndFail:
                    Singleton.Instance<ActionHandler>().StopExecution = true;
                    ActionStatus = Enums.Status.Fail;
                    break;

                case ActionType.RestartExecution:
                    Singleton.Instance<ActionHandler>().RestartExecution = true;
                    ActionStatus = Enums.Status.Pass;
                    break;
            }
        }

        public struct ActionData
        {
            public string TargetVar { get; set; }
        }
    }
}