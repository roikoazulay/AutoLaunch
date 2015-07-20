using System;
using System.Net.NetworkInformation;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class PingAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            Send,
        }

        public PingAction()
            : base(Enums.ActionTypeId.Ping)
        {
        }

        public override void Execute()
        {
            string hostname = Singleton.Instance<SavedData>().GetVariableData(_actionData.Host);
            AutoApp.Logger.WriteInfoLog("Starting Ping to host " + hostname);

            Ping pingSender = new Ping();
            byte[] buffer = new byte[32];
            PingReply pingReply;
            int replayCount = 0;
            for (int i = 0; i < int.Parse(_actionData.Loops); i++)
            {
                pingReply = pingSender.Send(hostname, 1000, buffer);
                if (pingReply.Status == IPStatus.Success)
                    replayCount++;
            }

            Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(replayCount.ToString());
            AutoApp.Logger.WriteInfoLog(string.Format("Ping Send Action to host {0} returned with success for {1} times", hostname, replayCount));

            ActionStatus = Enums.Status.Pass;
        }

        public PingAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.Ping)
        {
            _actionData = actionData;
            _type = type;
            Details.Add(type.ToString());
            Details.Add(_actionData.Host); //1
            Details.Add(_actionData.Loops); //2
            Details.Add(_actionData.TargetVar); //3
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { Host = Details[1], Loops = Details[2], TargetVar = Details[3] };
        }

        public struct ActionData
        {
            public string Host { get; set; } //1

            public string Loops { get; set; } //3

            public string TargetVar { get; set; } //2
        }
    }
}