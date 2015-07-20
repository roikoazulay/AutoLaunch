using System;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class DateTimeAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            GetCurrentTime,
            Equal,
            NotEqual,
            Greater,
            NotGreater,
            Substruct
        }

        public DateTimeAction()
            : base(Enums.ActionTypeId.DateTime)
        {
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog("Starting DateTime " + _type.ToString());

            switch (_type)
            {
                case ActionType.GetCurrentTime:
                    Singleton.Instance<SavedData>().Variables[_actionData.SourceVar].SetValue(DateTime.Now.ToString(_actionData.TimeFormat));
                    ActionStatus = Enums.Status.Pass;
                    AutoApp.Logger.WritePassLog("DateTime Action " + _type.ToString() + " Passed");
                    break;

                case ActionType.Equal:
                case ActionType.Greater:
                case ActionType.NotGreater:
                case ActionType.NotEqual:
                    CalcDiff();

                    //if target var is configured copy source var to target
                    if (Singleton.Instance<SavedData>().Variables.ContainsKey(_actionData.TargetVar))
                    {
                        bool res = ActionStatus == Enums.Status.Pass ? true : false;
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(res.ToString());
                        AutoApp.Logger.WriteInfoLog(string.Format("Target variable {0} value was set to {1}",
                                                                  _actionData.TargetVar,
                                                                  res));

                        if (ActionStatus == Enums.Status.Fail)
                        {
                            ActionStatus = Enums.Status.Pass;
                            AutoApp.Logger.WriteWarningLog("Action failed ...Change status to Passed - target variable exist");
                        }
                    }
                    break;

                case ActionType.Substruct:
                    DateTime srcDate = DateTime.ParseExact(Singleton.Instance<SavedData>().GetVariableData(_actionData.SourceVar), _actionData.TimeFormat, System.Globalization.CultureInfo.CurrentCulture);
                    DateTime trgDate = DateTime.ParseExact(Singleton.Instance<SavedData>().GetVariableData(_actionData.Value), _actionData.TimeFormat, System.Globalization.CultureInfo.CurrentCulture);
                    System.TimeSpan diff1 = srcDate.Subtract(trgDate);
                    Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(((diff1.TotalMilliseconds)).ToString());
                    ActionStatus = Enums.Status.Pass;
                    break;
            }

            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("DateTime Action " + _type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("DateTime Action " + _type.ToString() + " Failed");
        }

        private void CalcDiff()
        {
            DateTime srcDate = Convert.ToDateTime(Singleton.Instance<SavedData>().GetVariableData(_actionData.SourceVar));
            DateTime trgDate = Convert.ToDateTime(Singleton.Instance<SavedData>().GetVariableData(_actionData.Value));
            int res = srcDate.CompareTo(trgDate);
            switch (_type)
            {
                case ActionType.Equal:
                    ActionStatus = res == 0 ? Enums.Status.Pass : Enums.Status.Fail;
                    break;

                case ActionType.Greater:
                    ActionStatus = res == 1 ? Enums.Status.Pass : Enums.Status.Fail;
                    break;

                case ActionType.NotGreater:
                    ActionStatus = res == -1 ? Enums.Status.Pass : Enums.Status.Fail;
                    break;

                case ActionType.NotEqual:
                    ActionStatus = res != 0 ? Enums.Status.Pass : Enums.Status.Fail;
                    break;
            }
        }

        public DateTimeAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.DateTime)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.SourceVar); //1
            Details.Add(_actionData.TimeFormat); //2
            Details.Add(_actionData.TargetVar); //3
            Details.Add(_actionData.Value); //4
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { SourceVar = Details[1], TimeFormat = Details[2], TargetVar = Details[3], Value = string.Empty };
            if (Details.Count > 4)
                _actionData.Value = Details[4];
        }

        public struct ActionData
        {
            public string SourceVar { get; set; } //1

            public string TimeFormat { get; set; } //2

            public string TargetVar { get; set; } //3

            public string Value { get; set; } //3
        }
    }
}