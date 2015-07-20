using System;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class SshAction : ActionBase
    {
        private SshActionType _type;
        private SshActionData _sshActionData;

        public enum SshActionType
        {
            Connect,
            Dissconnect,
            SendCommand,
            ResetLog,
            GetData
        }

        public SshAction()
            : base(Enums.ActionTypeId.SSH)
        {
        }

        private SshGranados GetSshObject()
        {
            if (!Singleton.Instance<SavedData>().SshCommunications.ContainsKey(_sshActionData.Host))
            {
                string hostAddress = Singleton.Instance<SavedData>().GetVariableData(_sshActionData.Host);
                string logFile = Singleton.Instance<SavedData>().GetVariableData(_sshActionData.LogFileName);
                Singleton.Instance<SavedData>().SshCommunications.Add(_sshActionData.Host, new SshGranados(hostAddress, _sshActionData.UserName, _sshActionData.Password, logFile));
            }
            return Singleton.Instance<SavedData>().SshCommunications[_sshActionData.Host];
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog("Starting SSH Action " + _type.ToString());

            switch (_type)
            {
                case SshActionType.Connect:
                    try
                    {
                        if (Singleton.Instance<SavedData>().SshCommunications.ContainsKey(_sshActionData.Host))
                            Singleton.Instance<SavedData>().SshCommunications.Remove(_sshActionData.Host);

                        ActionStatus = GetSshObject().Connect() ? Enums.Status.Pass : Enums.Status.Fail;
                    }
                    catch (Exception ex)
                    {
                        AutoApp.Logger.WriteFailLog(string.Format("Failed to Connect SSH for Host  {0} ",
                                                                 _sshActionData.Host));
                        AutoApp.Logger.WriteFailLog(ex.ToString());
                    }

                    break;

                case SshActionType.Dissconnect:
                    try
                    {
                        GetSshObject().DisConnect();
                        Singleton.Instance<SavedData>().SshCommunications.Remove(_sshActionData.Host);
                        ActionStatus = Enums.Status.Pass;
                    }
                    catch (Exception ex)
                    {
                        AutoApp.Logger.WriteFailLog(string.Format("Failed to Disconnect SSH for Host  {0} ",
                                                                 _sshActionData.Host));
                        AutoApp.Logger.WriteFailLog(ex.ToString());
                    }
                    break;

                case SshActionType.SendCommand:
                    try
                    {
                        if (GetSshObject().SendCommand(Singleton.Instance<SavedData>().GetVariableData(_sshActionData.Command)))
                            ActionStatus = Enums.Status.Pass;
                    }
                    catch (Exception ex)
                    {
                        AutoApp.Logger.WriteFailLog(string.Format("Failed to end Command for Host {0} ",
                                                                 _sshActionData.Host));
                        AutoApp.Logger.WriteFailLog(ex.ToString());
                    }
                    break;

                case SshActionType.ResetLog:
                    if (GetSshObject().ResetLog())
                        ActionStatus = Enums.Status.Pass;
                    break;

                case SshActionType.GetData:
                    if (GetSshObject().GetData())
                        ActionStatus = Enums.Status.Pass;
                    else
                    {
                        AutoApp.Logger.WriteFatalLog("SSH GetData response timed out , the connection has no data to send");
                        Singleton.Instance<SavedData>().SshCommunications[_sshActionData.Host].DisConnect();
                        Singleton.Instance<SavedData>().SshCommunications.Remove(_sshActionData.Host);
                    }
                    break;
            }

            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("SSH Action " + _type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("SSH Action " + _type.ToString() + " Failed");
        }

        public SshAction(SshActionType type, SshActionData actionData)
            : base(Enums.ActionTypeId.SSH)
        {
            _sshActionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_sshActionData.Host); //1
            Details.Add(_sshActionData.UserName); //2
            Details.Add(_sshActionData.Password); //3
            Details.Add(_sshActionData.Command); //4
            Details.Add(_sshActionData.LogFileName); //6
        }

        public override void Construct()
        {
            _type = (SshActionType)Enum.Parse(typeof(SshActionType), Details[0]);
            _sshActionData = new SshActionData() { Host = Details[1], UserName = Details[2], Password = Details[3], Command = Details[4], LogFileName = Details[5] };
        }

        public struct SshActionData
        {
            public string Host { get; set; } //1

            public string UserName { get; set; } //2

            public string Password { get; set; } //3

            public string Command { get; set; }//4

            public string LogFileName { get; set; }//6
        }
    }
}