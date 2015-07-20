using System;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class TelentAction : ActionBase
    {
        private TelentActionType _type;
        private TelentActionData _telnetActionData;

        public enum TelentActionType
        {
            Connect,
            Dissconnect,
            SendCommand,
            GetData,
            ClearData,
            GetAndClear
        }

        public TelentAction()
            : base(Enums.ActionTypeId.Telnet)
        {
        }

        private TelnetClass GetObject()
        {
            string host = Singleton.Instance<SavedData>().GetVariableData(_telnetActionData.Host);
            string port = Singleton.Instance<SavedData>().GetVariableData(_telnetActionData.Port);
            if (!Singleton.Instance<SavedData>().TelnetCommunications.ContainsKey(_telnetActionData.Host))
                Singleton.Instance<SavedData>().TelnetCommunications.Add(_telnetActionData.Host, new TelnetClass(host, port));

            return Singleton.Instance<SavedData>().TelnetCommunications[_telnetActionData.Host];
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog(string.Format("Starting Telnet action {0} for host: {1} ", _type, _telnetActionData.Host));
            bool res = false;
            switch (_type)
            {
                case TelentActionType.Dissconnect:
                    res = GetObject().Disconnect();
                    break;

                case TelentActionType.Connect:
                    res = GetObject().Connect();
                    break;

                case TelentActionType.SendCommand:
                    res = GetObject().SendMessage(Singleton.Instance<SavedData>().GetVariableData(_telnetActionData.Command));
                    break;

                case TelentActionType.GetData:
                    string val = GetObject().GetRecivedDate();
                    if (Singleton.Instance<SavedData>().Variables.ContainsKey(_telnetActionData.TargetVar))
                    {
                        Singleton.Instance<SavedData>().Variables[_telnetActionData.TargetVar].SetValue(val);
                        res = true;
                    }
                    else
                        AutoApp.Logger.WriteWarningLog(string.Format("Target Variable {0} does not exist ", _telnetActionData.TargetVar));
                    break;

                case TelentActionType.GetAndClear:
                    string val1 = GetObject().GetAndClearDate();
                    if (Singleton.Instance<SavedData>().Variables.ContainsKey(_telnetActionData.TargetVar))
                    {
                        Singleton.Instance<SavedData>().Variables[_telnetActionData.TargetVar].SetValue(val1);
                        res = true;
                    }
                    else
                        AutoApp.Logger.WriteWarningLog(string.Format("Target Variable {0} does not exist ", _telnetActionData.TargetVar));
                    break;

                case TelentActionType.ClearData:
                    GetObject().ClearData();
                    res = true;
                    break;
            }

            if (res)
                ActionStatus = Enums.Status.Pass;
            else
                AutoApp.Logger.WriteFailLog(string.Format("Telnet action failed for Host  {0} ", _telnetActionData.Host));
        }

        public TelentAction(TelentActionType type, TelentActionData actionData)
            : base(Enums.ActionTypeId.Telnet)
        {
            _telnetActionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_telnetActionData.Host); //1
            Details.Add(_telnetActionData.Port); //2
            Details.Add(_telnetActionData.Command); //3
            Details.Add(_telnetActionData.TargetVar); //4
        }

        public override void Construct()
        {
            _type = (TelentActionType)Enum.Parse(typeof(TelentActionType), Details[0]);
            _telnetActionData = new TelentActionData() { Host = Details[1], Port = Details[2], Command = Details[3], TargetVar = Details[4] };
        }

        public struct TelentActionData
        {
            public string Host { get; set; } //1

            public string Port { get; set; } //2

            public string Command { get; set; }//3

            public string TargetVar { get; set; } //4
        }
    }
}