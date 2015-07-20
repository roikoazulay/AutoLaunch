using System;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class SwitchAction : ActionBase
    {
        public static string SwitchDelimiter = "<<-->>";
        private ActionData _actionData;
        private ActionType _type;

        public SwitchAction()
            : base(Enums.ActionTypeId.SwitchAction)
        {
        }

        public SwitchAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.SwitchAction)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.Switch); //1
            Details.Add(_actionData.CaseList); //2
            Details.Add(_actionData.DefaultScript); //2
        }

        public enum ActionType
        {
            SwitchAction
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { Switch = Details[1], CaseList = Details[2], DefaultScript = Details[3] };
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog("Starting Switch Action");

            switch (_type)
            {
                case ActionType.SwitchAction:
                    string switchVal = Singleton.Instance<SavedData>().GetVariableData(_actionData.Switch);
                    string tmp = _actionData.CaseList;
                    var caseList = tmp.Split('\n');

                    //data[0]- the case
                    //data[1]- the script to execute
                    //data[2]- the script params
                    foreach (var item in caseList)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var data = item.Split(new string[] { SwitchDelimiter }, StringSplitOptions.None);
                            if (data[0] == switchVal)
                            {
                                AutoApp.Logger.WriteInfoLog("Starting Script " + data[1]);
                                var script = FileHandler.ExtructScriptFromFile(data[1]);
                                Singleton.Instance<SavedData>().UpdateParams(data[2]);
                                script.Execute();
                                ActionStatus = Enums.Status.Pass;
                                return;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(_actionData.DefaultScript))
                    {
                        var data = _actionData.DefaultScript.Split(new string[] { SwitchDelimiter }, StringSplitOptions.None);
                        AutoApp.Logger.WriteInfoLog("Starting Default Script " + data[0]);
                        var script = FileHandler.ExtructScriptFromFile(data[0]);
                        Singleton.Instance<SavedData>().UpdateParams(data[1]);
                        script.Execute();
                        ActionStatus = Enums.Status.Pass;
                    }

                    break;
            }
        }

        public struct ActionData
        {
            public string Switch { get; set; } //1

            public string CaseList { get; set; } //2

            public string DefaultScript { get; set; } //2
        }
    }
}