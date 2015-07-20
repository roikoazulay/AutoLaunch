using System;
using System.Collections.Generic;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class DictionaryAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            Create,
            ImportFromFile,
            SetValueByKey,
            GetValueByKey,
            GetLength,
            IsKeyExist,
            GetKeyByValue,
            Clear
        }

        public DictionaryAction()
            : base(Enums.ActionTypeId.DictionaryAction)
        {
        }

        public Dictionary<string, string> CreateOrGetDictionary(string dictionaryName)
        {
            if (!Singleton.Instance<SavedData>().DictionaryListObj.ContainsKey(dictionaryName))
                Singleton.Instance<SavedData>().DictionaryListObj.Add(dictionaryName, new Dictionary<string, string>());

            return Singleton.Instance<SavedData>().DictionaryListObj[dictionaryName];
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog("Starting Dictionary Action " + _type.ToString());
            var dict = CreateOrGetDictionary(_actionData.Name);
            var key = Singleton.Instance<SavedData>().GetVariableData(_actionData.Key);
            var value = Singleton.Instance<SavedData>().GetVariableData(_actionData.Value);
            try
            {
                switch (_type)
                {
                    case ActionType.Create:
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.Clear:
                        dict = new Dictionary<string, string>();
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.GetValueByKey:
                        if (dict.ContainsKey(key))
                            Singleton.Instance<SavedData>().Variables[_actionData.Target].SetValue(dict[key]);
                        else
                        {
                            AutoApp.Logger.WriteWarningLog(string.Format("Dictionary Action GetValueByKey - key {0} does not exist,Target variable change to Empty", key));
                            Singleton.Instance<SavedData>().Variables[_actionData.Target].SetValue(string.Empty);
                        }

                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.SetValueByKey:
                        if (dict.ContainsKey(key))
                        {
                            AutoApp.Logger.WriteWarningLog(string.Format("Dictionary Action SetValueByKey - key {0} already exist , overwriting key value", key));
                            dict.Remove(_actionData.Key);
                        }

                        dict.Add(_actionData.Key, value);
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.ImportFromFile:
                        var list = CommonHelper.ReadTextFileAsList(Singleton.Instance<SavedData>().GetVariableData(_actionData.FileName));
                        foreach (string item in list)
                        {
                            string[] val = item.Split(',');
                            if (dict.ContainsKey(val[0]))
                            {
                                AutoApp.Logger.WriteWarningLog(string.Format("Dictionary Action Import from file - key {0} already exist , overwriting key value", val[0]));
                                dict.Remove(val[0]);
                            }

                            dict.Add(val[0], val[1]);
                        }
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.GetLength:
                        Singleton.Instance<SavedData>().Variables[_actionData.Target].SetValue(dict.Count.ToString());
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.GetKeyByValue:
                        Singleton.Instance<SavedData>().Variables[_actionData.Target].SetValue(string.Empty);
                        foreach (var item in dict.Keys)
                        {
                            if (dict[item] == value)
                            {
                                Singleton.Instance<SavedData>().Variables[_actionData.Target].SetValue(item);
                                AutoApp.Logger.WritePassLog(string.Format("Found Dictionary Key for value{0}", value));
                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(Singleton.Instance<SavedData>().Variables[_actionData.Target].ToString()))
                            AutoApp.Logger.WriteWarningLog(string.Format("Did not find Dictionary Key for value{0}", value));

                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.IsKeyExist:
                        if (dict.ContainsKey(key))
                        {
                            Singleton.Instance<SavedData>().Variables[_actionData.Target].SetValue("True");
                            AutoApp.Logger.WritePassLog(string.Format("Dictionary Key {0} exist", key));
                        }
                        else
                        {
                            Singleton.Instance<SavedData>().Variables[_actionData.Target].SetValue("False");
                            AutoApp.Logger.WriteWarningLog(string.Format("Dictionary Key {0} does not exist", key));
                        }
                        ActionStatus = Enums.Status.Pass;
                        break;
                }
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
            }

            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("Dictionary Action " + _type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("Dictionary Action " + _type.ToString() + " Failed");
        }

        public DictionaryAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.DictionaryAction)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.Name); //1
            Details.Add(_actionData.Key); //1
            Details.Add(_actionData.Value); //2
            Details.Add(_actionData.Target); //3
            Details.Add(_actionData.FileName); //4
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { Name = Details[1], Key = Details[2], Value = Details[3], Target = Details[4], FileName = Details[5] };
        }

        public struct ActionData
        {
            public string Name { get; set; } //1

            public string Key { get; set; } //2

            public string Value { get; set; } //3

            public string Target { get; set; } //4

            public string FileName { get; set; } //5
        }
    }
}