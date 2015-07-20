using System;
using System.Collections.Generic;
using System.Text;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class ListAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            Create,
            ImportFromFile,
            ImportFromVariable,
            ExportToFile,
            GetCount,
            AddItem,
            RemoveAt,
            SetValueAtIndex,
            GetValueFromIndex,
            Clear,
            Contains,
            Exists//verify the all strings in list exist in some data string (opposite of contains)
        }

        public ListAction()
            : base(Enums.ActionTypeId.ListAction)
        {
        }

        public List<string> GetOrCreateList(string listName)
        {
            if (!Singleton.Instance<SavedData>().ListObj.ContainsKey(listName))
                Singleton.Instance<SavedData>().ListObj.Add(listName, new List<string>());

            return Singleton.Instance<SavedData>().ListObj[listName];
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog("Starting List Action " + _type.ToString());
            var listObj = GetOrCreateList(_actionData.ListName);
            try
            {
                switch (_type)
                {
                    case ActionType.Create:
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.Clear:
                        Singleton.Instance<SavedData>().ListObj[_actionData.ListName].Clear();
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.RemoveAt:
                        Singleton.Instance<SavedData>().ListObj[_actionData.ListName].RemoveAt(int.Parse(Singleton.Instance<SavedData>().GetVariableData(_actionData.Index)));
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.GetCount:
                        Singleton.Instance<SavedData>().Variables[_actionData.Target].SetValue(listObj.Count.ToString());
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.SetValueAtIndex:
                        Singleton.Instance<SavedData>().ListObj[_actionData.ListName][int.Parse(Singleton.Instance<SavedData>().GetVariableData(_actionData.Index))] = Singleton.Instance<SavedData>().GetVariableData(_actionData.Value);
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.GetValueFromIndex:
                        Singleton.Instance<SavedData>().Variables[_actionData.Target].SetValue(listObj[int.Parse(Singleton.Instance<SavedData>().GetVariableData(_actionData.Index))]);
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.ImportFromFile:
                        listObj = CommonHelper.ReadTextFileAsList(Singleton.Instance<SavedData>().GetVariableData(_actionData.FileName));
                        Singleton.Instance<SavedData>().ListObj[_actionData.ListName].AddRange(listObj);
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.ImportFromVariable:
                        var data = Singleton.Instance<SavedData>().GetVariableData(_actionData.Value);
                        var dataArray = data.Split('\n');
                        foreach (var item in dataArray)
                            Singleton.Instance<SavedData>().ListObj[_actionData.ListName].Add(item);

                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.ExportToFile:
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in listObj)
                            sb.AppendLine(item);

                        CommonHelper.SaveStringToFile(sb.ToString(), Singleton.Instance<SavedData>().GetVariableData(_actionData.FileName));
                        ActionStatus = Enums.Status.Pass;
                        break;

                    case ActionType.AddItem:
                        Singleton.Instance<SavedData>().ListObj[_actionData.ListName].Add(Singleton.Instance<SavedData>().GetVariableData(_actionData.Value));
                        ActionStatus = Enums.Status.Pass;
                        break;

                    //case ActionType.CopyList:

                    //    listObj.Clear();
                    //    var oldList = GetOrCreateList(Singleton.Instance<SavedData>().GetVariableData(_actionData.Value));
                    //    foreach (var item in oldList)
                    //        listObj.Add(item);
                    //    break;

                    case ActionType.Contains:
                        var dataToFind = Singleton.Instance<SavedData>().GetVariableData(_actionData.Value);
                        foreach (var item in listObj)
                        {
                            if (item.Contains(dataToFind))
                            {
                                ActionStatus = Enums.Status.Pass;
                                break;
                            }
                        }
                        break;

                    case ActionType.Exists:
                        bool stringMissing = false;
                        var str = Singleton.Instance<SavedData>().GetVariableData(_actionData.Value);
                        foreach (var item in listObj)
                        {
                            if (!str.Contains(item))
                            {
                                AutoApp.Logger.WriteFailLog(string.Format("{0} does not exist in {1}", item, _actionData.Value));
                                stringMissing = true;
                            }
                        }
                        if (!stringMissing)
                            ActionStatus = Enums.Status.Pass;
                        break;
                }
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
            }

            //if (Singleton.Instance<SavedData>().Variables.ContainsKey(_actionData.Target))
            //{
            //    bool res = ActionStatus == Enums.Status.Pass ? true : false;
            //    Singleton.Instance<SavedData>().Variables[_actionData.Target].SetValue(res.ToString());
            //    AutoApp.Logger.WriteInfoLog(string.Format("Target variable {0} value was set to {1}",
            //                                              _actionData.Target,
            //                                              res));

            //    if (ActionStatus == Enums.Status.Fail)
            //    {
            //        ActionStatus = Enums.Status.Pass;
            //        AutoApp.Logger.WriteWarningLog("Action failed ...Change status to Passed - target variable exist");
            //    }

            //}

            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("List Action " + _type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("List Action " + _type.ToString() + " Failed");
        }

        public ListAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.ListAction)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.ListName); //1
            Details.Add(_actionData.Value); //2
            Details.Add(_actionData.Index); //3
            Details.Add(_actionData.Target); //4
            Details.Add(_actionData.FileName); //5
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { ListName = Details[1], Value = Details[2], Index = Details[3], Target = Details[4], FileName = Details[5] };
        }

        public struct ActionData
        {
            public string ListName { get; set; } //1

            public string Value { get; set; } //2

            public string Index { get; set; } //3

            public string Target { get; set; } //4

            public string FileName { get; set; } //5
        }
    }
}