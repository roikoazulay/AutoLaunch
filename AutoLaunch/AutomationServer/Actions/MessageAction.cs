using System;
using System.Threading;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class MessageAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            ShowMessage
        }

        public MessageAction()
            : base(Enums.ActionTypeId.MessageBox)
        {
        }

        public override void Execute()
        {
            string message = Singleton.Instance<SavedData>().GetVariableData(_actionData.Message);
            double delay = double.Parse(Singleton.Instance<SavedData>().GetVariableData(_actionData.TimeOut));
            AutoApp.Logger.WriteInfoLog("Starting Message Show " + _type.ToString());
            switch (_type)
            {
                case ActionType.ShowMessage:

                    if (delay == 0)
                    {
                        AutoApp.Logger.WriteInfoLog(message);
                        AutoApp.Logger.WriteWarningLog("Please press Enter to continue");
                        Console.ReadLine();
                    }
                    else
                    {
                        AutoApp.Logger.WriteInfoLog("message show:" + message, true);
                        AutoApp.Logger.WriteInfoLog(string.Format("Waiting {0} Sec for closing message", delay));
                        Thread.Sleep((int)(delay * 1000));
                    }
                    break;
            }

            //always pass
            ActionStatus = Enums.Status.Pass;
            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("Message  " + _type.ToString() + " Passed");
        }

        public MessageAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.MessageBox)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.Message); //1
            Details.Add(_actionData.TimeOut); //2
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { Message = Details[1], TimeOut = Details[2] };
        }

        public struct ActionData
        {
            public string Message { get; set; } //1

            public string TimeOut { get; set; } //2
        }
    }
}