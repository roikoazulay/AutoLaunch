using System;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class EmailAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            Send
        }

        public EmailAction()
            : base(Enums.ActionTypeId.EmailAction)
        {
        }

        public override void Execute()
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(Singleton.Instance<SavedData>().GetVariableData(_actionData.Recipient));
            message.Subject = Singleton.Instance<SavedData>().GetVariableData(_actionData.Subject);
            message.From = new System.Net.Mail.MailAddress(Singleton.Instance<SavedData>().GetVariableData(_actionData.From));
            message.Body = Singleton.Instance<SavedData>().GetVariableData(_actionData.Body);
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(Singleton.Instance<SavedData>().GetVariableData(_actionData.MailServer));
            smtp.Send(message);
            ActionStatus = Enums.Status.Pass;
        }

        public EmailAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.EmailAction)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.Recipient);
            Details.Add(_actionData.From);
            Details.Add(_actionData.Subject);
            Details.Add(_actionData.Body);
            Details.Add(_actionData.MailServer);
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { Recipient = Details[1], From = Details[2], Subject = Details[3], Body = Details[4], MailServer = Details[5] };
        }

        public struct ActionData
        {
            public string Recipient { get; set; } //1

            public string From { get; set; } //2

            public string Subject { get; set; } //3

            public string Body { get; set; } //4

            public string MailServer { get; set; } //5
        }
    }
}