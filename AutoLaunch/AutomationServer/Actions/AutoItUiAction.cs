using System;
using AutomationCommon;
using AutomationServer.Modules;

namespace AutomationServer.Actions
{
    public class AutoItUiAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            ControlClick,
            ControlSendText,
            ControlGetText,
        }

        public AutoItUiAction()
            : base(Enums.ActionTypeId.GuiAutomation)
        {
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog("Starting UI Automation " + _type.ToString());
            switch (_type)
            {
                case ActionType.ControlClick:
                    if (AutoItGuiAutomationObj.ControlClick(_actionData.WindowTitle, _actionData.ControlID))
                        ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.ControlSendText:
                    AutoApp.Logger.WriteInfoLog(string.Format("Sending text {0} ", Singleton.Instance<SavedData>().GetVariableData(_actionData.Text)));
                    if (AutoItGuiAutomationObj.ControlSendText(_actionData.WindowTitle, _actionData.ControlID, Singleton.Instance<SavedData>().GetVariableData(_actionData.Text)))
                        ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.ControlGetText:
                    string retval = string.Empty;
                    if (AutoItGuiAutomationObj.ControlGetText(_actionData.WindowTitle, _actionData.ControlID, ref retval))
                    {
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVariable].SetValue(retval);
                        ActionStatus = Enums.Status.Pass;
                    }
                    break;
            }
            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("UI Automation Action " + _type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("UI Automation Action " + _type.ToString() + " Failed");
        }

        public AutoItUiAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.GuiAutomation)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.WindowTitle); //1
            Details.Add(_actionData.ControlID); //2
            Details.Add(_actionData.Text); //3
            Details.Add(_actionData.TargetVariable); //4
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { WindowTitle = Details[1], ControlID = Details[2], Text = Details[3], TargetVariable = Details[4] };
        }

        public struct ActionData
        {
            public string WindowTitle { get; set; } //1

            public string ControlID { get; set; } //2

            public string Text { get; set; } //3

            public string TargetVariable { get; set; } //4
        }
    }
}