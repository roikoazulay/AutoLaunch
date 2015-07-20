using System;
using System.ServiceModel;
using AutomationCommon;

namespace AutomationServer.Actions
{
    # region  Old

    //public class ServerComActionOld : ActionBase
    //{
    //    private ActionType _type;
    //    private ActionData _actionData;

    //    public enum ActionType
    //    {
    //        Connect,
    //        Dissconnect,
    //        ExecuteSuite,
    //        GetServerStatus,
    //        GetLastExecutionStatus,
    //        GetValue
    //    }

    //    public ServerComActionOld()
    //        : base(Enums.ActionTypeId.ServerComAction)
    //    {
    //    }

    //    private void RemoveObject()
    //    {
    //        if (Singleton.Instance<SavedData>().TelnetCommunications.ContainsKey(_actionData.Host))
    //            Singleton.Instance<SavedData>().TelnetCommunications.Remove(_actionData.Host);
    //    }

    //    private TelnetClass GetObject()
    //    {
    //        string host = Singleton.Instance<SavedData>().GetVariableData(_actionData.Host);
    //        if (!Singleton.Instance<SavedData>().TelnetCommunications.ContainsKey(_actionData.Host))
    //            Singleton.Instance<SavedData>().TelnetCommunications.Add(_actionData.Host, new TelnetClass(host, _actionData.Port));

    //        return Singleton.Instance<SavedData>().TelnetCommunications[_actionData.Host];
    //    }

    //    private bool Connect()
    //    {
    //        bool res = GetObject().Connect();
    //        return res;
    //    }

    //    public override void Execute()
    //    {
    //        bool res = false;
    //        GetObject().GetAndClearDate();
    //        AutoApp.Logger.WriteInfoLog(string.Format("Starting Server Com Action {0} for host {1}", _type, _actionData.Host));
    //        switch (_type)
    //        {
    //            case ActionType.Dissconnect:
    //                res = GetObject().Disconnect();
    //                if (res)
    //                    Singleton.Instance<SavedData>().TelnetCommunications.Remove(_actionData.Host);
    //                break;

    //            case ActionType.Connect:
    //                res = Connect();
    //                if (!res)
    //                {
    //                    RemoveObject();
    //                    new System.Threading.ManualResetEvent(false).WaitOne(5000);
    //                    Connect();
    //                }
    //                break;
    //            case ActionType.ExecuteSuite:
    //                res = SendCommandToServer(ServerComActionOld.ActionType.ExecuteSuite, _actionData.Value);
    //                break;
    //            case ActionType.GetLastExecutionStatus:
    //                res = SendCommandToServer(ServerComActionOld.ActionType.GetLastExecutionStatus,null);
    //                break;
    //            case ActionType.GetServerStatus:
    //                res = SendCommandToServer(ServerComActionOld.ActionType.GetServerStatus,null);
    //                break;
    //            case ActionType.GetValue:
    //                res = SendCommandToServer(ServerComActionOld.ActionType.GetValue,_actionData.Value);
    //                break;

    //        }

    //        if (res)
    //        {
    //            ActionStatus = Enums.Status.Pass;
    //            AutoApp.Logger.WritePassLog(string.Format("Server Com Passed for Host  {0} ", _actionData.Host));
    //        }
    //        else
    //            AutoApp.Logger.WriteFailLog(string.Format("Server Com failed for Host  {0} ", _actionData.Host));
    //    }

    //    private bool SendCommandToServer(ServerComActionOld.ActionType command,string value)
    //    {
    //        string serverCommand = command.ToString();
    //        if (!string.IsNullOrEmpty(value))
    //            serverCommand += ":" + value;

    //        bool res = GetObject().SendMessage(serverCommand);
    //        new System.Threading.ManualResetEvent(false).WaitOne(2000);
    //        if (!string.IsNullOrEmpty(_actionData.TargetVar))
    //            Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(GetObject().GetAndClearDate());

    //        return res;
    //    }
    //    public ServerComActionOld(ActionType type, ActionData actionData)
    //        : base(Enums.ActionTypeId.ServerComAction)
    //    {
    //        _actionData = actionData;
    //        _type = type;

    //        Details.Add(type.ToString());
    //        Details.Add(_actionData.Host); //1
    //        Details.Add(_actionData.Port); //2
    //        Details.Add(_actionData.Value); //3
    //        Details.Add(_actionData.TargetVar);//4
    //    }

    //    public override void Construct()
    //    {
    //        _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
    //        _actionData = new ActionData() { Host = Details[1], Port = Details[2], Value = Details[3], TargetVar = Details[4] };
    //    }

    //    public struct ActionData
    //    {
    //        public string Host { get; set; } //1

    //        public string Port { get; set; } //2

    //        public string Value { get; set; }//3

    //        public string TargetVar { get; set; } //4
    //    }
    //}
    #endregion

    public class RemoteServerAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            Connect,
            Dissconnect,
            ExecuteSuite,
            GetServerStatus,
            GetLastExecutionStatus,
            GetValue
        }

        public RemoteServerAction()
            : base(Enums.ActionTypeId.ServerComAction)
        {
        }

        private ICommunication GetObject()
        {
            string host = Singleton.Instance<SavedData>().GetVariableData(_actionData.Host);
            if (!Singleton.Instance<SavedData>().RemoteServerCommunications.ContainsKey(_actionData.Host))
            {
                var com = ConnectRemote();
                Singleton.Instance<SavedData>().RemoteServerCommunications.Add(_actionData.Host, com);
            }

            return Singleton.Instance<SavedData>().RemoteServerCommunications[_actionData.Host];
        }

        public override void Execute()
        {
            bool res = false;
            var comm = GetObject();
            AutoApp.Logger.WriteInfoLog(string.Format("Starting Remote Server Action {0} for host {1}", _type, _actionData.Host));
            switch (_type)
            {
                case ActionType.Connect:
                    res = comm.IsConnected();
                    break;

                case ActionType.ExecuteSuite:
                    string[] data = _actionData.Value.Split(',');
                    var onfail = (Enums.OnFailerAction)Enum.Parse(typeof(Enums.OnFailerAction), data[2]);
                    comm.ExecuteSuite(data[0], int.Parse(data[1]), onfail);
                    res = true;
                    break;

                case ActionType.GetLastExecutionStatus:
                    string status = comm.GetLastExecutionStatus();
                    if (!string.IsNullOrEmpty(status))
                    {
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(status);
                        res = true;
                    }
                    break;

                case ActionType.GetServerStatus:
                    var state = comm.GetSuiteProgressInfo().ToString();
                    if (!string.IsNullOrEmpty(state))
                    {
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(state);
                        res = true;
                    }

                    break;

                case ActionType.GetValue:
                    Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(comm.GetVariableData(_actionData.Value));
                    res = true;
                    break;
            }

            if (res)
            {
                ActionStatus = Enums.Status.Pass;
                AutoApp.Logger.WritePassLog(string.Format("Remote Server Action Passed for Host  {0} ", _actionData.Host));
            }
            else
                AutoApp.Logger.WriteFailLog(string.Format("Remote Server Action failed for Host  {0} ", _actionData.Host));
        }

        public RemoteServerAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.ServerComAction)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.Host); //1
            Details.Add(_actionData.Port); //2
            Details.Add(_actionData.Value); //3
            Details.Add(_actionData.TargetVar);//4
        }

        private ICommunication ConnectRemote()
        {
            //172.16.7.184
            string host = Singleton.Instance<SavedData>().GetVariableData(_actionData.Host);
            string uri = @"http://" + host + ":" + AutoApp.Settings.RemoteServerPort.ToString() + @"/ServerCommunication";//"http://localhost:2090/ServerCommunication"
            var wSHttpBinding = new WSHttpBinding();
            wSHttpBinding.MaxReceivedMessageSize = 2147483647;
            wSHttpBinding.MaxBufferPoolSize = 2147483647;

            wSHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            wSHttpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

            var comProxy = ChannelFactory<ICommunication>.CreateChannel(wSHttpBinding, new EndpointAddress(uri));

            return comProxy;
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { Host = Details[1], Port = Details[2], Value = Details[3], TargetVar = Details[4] };
        }

        public struct ActionData
        {
            public string Host { get; set; } //1

            public string Port { get; set; } //2

            public string Value { get; set; }//3

            public string TargetVar { get; set; } //4
        }
    }
}