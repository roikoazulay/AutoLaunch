using System;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class Horison80PowerSupplyAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            Connect,
            Dissconnect,
            PowerOn,
            PowerOff,
            SetVoltage
        }

        public Horison80PowerSupplyAction()
            : base(Enums.ActionTypeId.Horison80PowerSupplyAction)
        {
        }

        private void RemoveObject()
        {
            string host = Singleton.Instance<SavedData>().GetVariableData(_actionData.Host);
            if (Singleton.Instance<SavedData>().TelnetCommunications.ContainsKey(host))
                Singleton.Instance<SavedData>().TelnetCommunications.Remove(host);
        }

        private TelnetClass GetObject()
        {
            string host = Singleton.Instance<SavedData>().GetVariableData(_actionData.Host);
            if (!Singleton.Instance<SavedData>().TelnetCommunications.ContainsKey(host))
                Singleton.Instance<SavedData>().TelnetCommunications.Add(host, new TelnetClass(host, _actionData.Port));

            return Singleton.Instance<SavedData>().TelnetCommunications[host];
        }

        private bool Connect()
        {
            bool res = GetObject().Connect();
            if (res)
            {
                res = SendCommandResult("SABC 1");//Set ABC Selection to 1 - up to 27 volt
                if (res)
                {
                    res = SendCommandResult("GABC", "1");//Get ABC Selection to 1
                    if (res)
                    {
                        res = SendCommandResult("SCHA 1");
                        if (res)
                            res = SendCommandResult("GCHA", "1");
                    }
                }
            }

            return res;
        }

        public override void Execute()
        {
            bool res = false;
            string data = Singleton.Instance<SavedData>().GetVariableData(_actionData.Parm1);
            AutoApp.Logger.WriteInfoLog(string.Format("Starting HR80 PS action {0} for host {1}", _type, _actionData.Host));
            switch (_type)
            {
                case ActionType.Dissconnect:
                    res = GetObject().Disconnect();
                    if (res)
                        Singleton.Instance<SavedData>().TelnetCommunications.Remove(_actionData.Host);
                    break;

                case ActionType.Connect:
                    res = Connect();
                    if (!res)
                    {
                        RemoveObject();
                        new System.Threading.ManualResetEvent(false).WaitOne(5000);
                        Connect();
                    }
                    break;

                case ActionType.PowerOff:
                    res = SendCommandResult("SOUT 0");
                    if (res)
                        res = SendCommandResult("GOUT", "0");
                    break;

                case ActionType.PowerOn:
                    res = SendCommandResult("SOUT 1");
                    if (res)
                        res = SendCommandResult("GOUT", "1");
                    break;

                case ActionType.SetVoltage:
                    string[] dig = data.Split('.');
                    if (dig.Length > 1)
                    {
                        res = SendCommandResult(string.Format("VOLT 1 {0}{1}", dig[0].PadLeft(2, '0'), dig[1].PadRight(2, '0')));
                        if (res)
                            res = SendCommandResult("GETS 1", string.Format("{0}{1}", dig[0].PadLeft(2, '0'), dig[1].PadRight(2, '0')));
                    }
                    else
                    {
                        res = SendCommandResult(string.Format("VOLT 1 {0}00", data.PadLeft(2, '0')));//VOLT 0 1200
                        if (res)
                            res = SendCommandResult("GETS 1", string.Format("{0}00", data.PadLeft(2, '0')));
                    }

                    break;
            }

            if (res)
            {
                ActionStatus = Enums.Status.Pass;
                AutoApp.Logger.WritePassLog(string.Format("HR 80 Action Passed for Host  {0} ", _actionData.Host));
            }
            else
                AutoApp.Logger.WriteFailLog(string.Format("HR 80 Action failed for Host  {0} ", _actionData.Host));
        }

        private bool SendCommandResult(string command, string result = "OK")
        {
            bool res = GetObject().SendMessage(command);
            if (!res)
            {
                AutoApp.Logger.WriteFailLog("HR80 SendMessage failed");
                return false;
            }

            new System.Threading.ManualResetEvent(false).WaitOne(1000);
            //  string retVal = GetObject().GetRecivedDate();
            string retVal = GetObject().GetAndClearDate();
            AutoApp.Logger.WriteInfoLog(string.Format("HR80 Returned {0}", retVal));
            if (retVal.ToUpper().Contains(result.ToUpper()))
                return true;

            return false;
        }

        public Horison80PowerSupplyAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.Horison80PowerSupplyAction)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.Host); //1
            Details.Add(_actionData.Port); //2
            Details.Add(_actionData.TargetVar); //3
            Details.Add(_actionData.Parm1);//4
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { Host = Details[1], Port = Details[2], TargetVar = Details[3], Parm1 = Details[4] };
        }

        public struct ActionData
        {
            public string Host { get; set; } //1

            public string Port { get; set; } //2

            public string TargetVar { get; set; } //3

            public string Parm1 { get; set; }//4 currently volt
        }
    }
}