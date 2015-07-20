using System;
using AutomationCommon;

namespace AutomationServer.Actions.RS232
{
    public class Rs232Action : ActionBase
    {
        private Rs232ActionType _type;
        private Rs232ActionData _actionData;

        public enum Rs232ActionType
        {
            Connect,
            Disconnect,
            SendCommand,
            ResetLog,
            GetData
        }

        public Rs232Action()
            : base(Enums.ActionTypeId.Rs232Operations)
        {
        }

        public Rs232Action(Rs232ActionType type, Rs232ActionData actionData)
            : base(Enums.ActionTypeId.Rs232Operations)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.Port); //1
            Details.Add(_actionData.Handshake); //2
            Details.Add(_actionData.Rts); //3
            Details.Add(_actionData.Dtr); //4
            Details.Add(_actionData.Parity); //5
            Details.Add(_actionData.StopBits); //6
            Details.Add(_actionData.DataBits); //7
            Details.Add(_actionData.BaudRate); //8
            Details.Add(_actionData.TargetVar); //9
            Details.Add(_actionData.Command); //10
            Details.Add(_actionData.LogFileName); //11
            Details.Add(_actionData.AsBytes.ToString()); //12
            Details.Add(_actionData.SendLineFeed.ToString()); //13
            Details.Add(_actionData.SendSingleChar.ToString()); //14
        }

        public struct Rs232ActionData
        {
            public string Port { get; set; } //1

            public string Handshake { get; set; } //2

            public string Rts { get; set; } //3

            public string Dtr { get; set; } //4

            public string Parity { get; set; } //5

            public string StopBits { get; set; } //6

            public string DataBits { get; set; } //7

            public string BaudRate { get; set; } //8

            public string TargetVar { get; set; } //9

            public string Command { get; set; } //10

            public string LogFileName { get; set; } //11

            public string AsBytes { get; set; } //12

            public string SendLineFeed { get; set; } //13

            public string SendSingleChar { get; set; } //14
        }

        public override void Execute()
        {
            switch (_type)
            {
                //case Rs232ActionType.GetData:
                //    if (GetSerialObject())
                //    {
                //        ActionStatus = Enums.Status.Pass;
                //        AutoApp.Logger.WritePassLog(string.Format("Reset Serial Port Log on {0}", _actionData.Port));
                //    }
                //    else
                //        AutoApp.Logger.WriteFailLog(string.Format("Failed to reset serial log file on Port {0} ", _actionData.Port));
                //     break;

                case Rs232ActionType.ResetLog:

                    if (GetSerialObject().ResetLog())
                    {
                        ActionStatus = Enums.Status.Pass;
                        AutoApp.Logger.WritePassLog(string.Format("Reset Serial Port Log on {0}", _actionData.Port));
                    }
                    else
                        AutoApp.Logger.WriteFailLog(string.Format("Failed to reset serial log file on Port {0} ", _actionData.Port));
                    break;

                case Rs232ActionType.Connect:
                    if (GetSerialObject().Connect())
                    {
                        ActionStatus = Enums.Status.Pass;
                        AutoApp.Logger.WritePassLog(string.Format("Serial Port on {0} connected", _actionData.Port));
                    }
                    else
                        AutoApp.Logger.WriteFailLog(string.Format("Failed to Connect Serial Port on  {0} ",
                                                                  _actionData.Port));
                    break;

                case Rs232ActionType.Disconnect:
                    if (GetSerialObject().DisConnect())
                    {
                        ActionStatus = Enums.Status.Pass;
                        AutoApp.Logger.WritePassLog(string.Format("Serial Port on {0} Disconnected", _actionData.Port));
                    }
                    else
                        AutoApp.Logger.WriteFailLog(string.Format("Failed to Disconnected Serial Port on  {0} ",
                                                                  _actionData.Port));
                    break;
                //case Rs232ActionType.ClearData:
                //    GetSerialObject().ClearData();
                //    ActionStatus = Enums.Status.Pass;
                //    AutoApp.Logger.WritePassLog(string.Format("Serial Port on {0} - Data Cleared", _actionData.Port));
                //    break;
                //case Rs232ActionType.ExportData:
                //    if (Singleton.Instance<SavedData>().Variables.ContainsKey(_actionData.TargetVar))
                //    {
                //        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(GetSerialObject().GetData());
                //        ActionStatus = Enums.Status.Pass;
                //        AutoApp.Logger.WritePassLog(string.Format("Serial Port on {0} - Data saved to variable {1}",
                //                                                  _actionData.Port, _actionData.TargetVar));
                //    }
                //    else
                //        AutoApp.Logger.WriteFailLog(string.Format("Serial Port on {0} - failed saving data to variable {1}",
                //                                                _actionData.Port, _actionData.TargetVar));
                //    break;
                case Rs232ActionType.SendCommand:
                    if (GetSerialObject().SendCommand(_actionData.Command, bool.Parse(_actionData.AsBytes), bool.Parse(_actionData.SendLineFeed), bool.Parse(_actionData.SendSingleChar)))
                    {
                        ActionStatus = Enums.Status.Pass;
                        AutoApp.Logger.WritePassLog(string.Format("Serial Port on {0} - Send Command Pass - Command: {1}",
                                                                  _actionData.Port, _actionData.Command));
                    }
                    else
                        AutoApp.Logger.WriteFailLog(string.Format("Serial Port on {0} - Send Command failed ",
                                                                  _actionData.Port));
                    break;
            }
        }

        public override void Construct()
        {
            _type = (Rs232ActionType)Enum.Parse(typeof(Rs232ActionType), Details[0]);
            _actionData = new Rs232ActionData()
            {
                Port = Details[1],
                Handshake = Details[2],
                Rts = Details[3],
                Dtr = Details[4],
                Parity = Details[5],
                StopBits = Details[6],
                DataBits = Details[7],
                BaudRate = Details[8],
                TargetVar = Details[9],
                Command = Details[10],
                LogFileName = Details.Count > 11 ? Details[11] : string.Empty,
                AsBytes = Details.Count > 12 ? Details[12] : false.ToString(),
                SendLineFeed = Details.Count > 13 ? Details[13] : false.ToString(),
                SendSingleChar = Details.Count > 14 ? Details[14] : false.ToString(),
            };
        }

        private Rs232Obj GetSerialObject()
        {
            if (!Singleton.Instance<SavedData>().SerialCommunications.ContainsKey(_actionData.Port))
                Singleton.Instance<SavedData>().SerialCommunications.Add(_actionData.Port, new Rs232Obj(_actionData));

            return (Rs232Obj)Singleton.Instance<SavedData>().SerialCommunications[_actionData.Port];
        }
    }
}