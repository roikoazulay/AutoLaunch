using System;
using System.Collections.Generic;
using System.Data;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class TableAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            CreateTable,
            ImportTableFile,
            ExportTableFile,
            SetCellValue,
            GetCellValue,
            GetRowCount,
            GetColumnCount,
            ClearTable,
            DeleteTable,
            CopyTable
        }

        public TableAction()
            : base(Enums.ActionTypeId.Table)
        {
        }

        public TableObj GetOrCreateTable(string tableName)
        {
            if (!Singleton.Instance<SavedData>().Tables.ContainsKey(tableName))
                Singleton.Instance<SavedData>().Tables.Add(tableName, new TableObj());

            return Singleton.Instance<SavedData>().Tables[tableName];
        }

        public override void Execute()
        {
            string row = string.Empty, col = string.Empty;
            if (!string.IsNullOrEmpty(_actionData.Row) && !string.IsNullOrEmpty(_actionData.Column))
            {
                row = Singleton.Instance<SavedData>().GetVariableData(_actionData.Row);
                col = Singleton.Instance<SavedData>().GetVariableData(_actionData.Column);
            }

            AutoApp.Logger.WriteInfoLog("Starting Table Action " + _type.ToString());
            switch (_type)
            {
                case ActionType.CreateTable:
                    GetOrCreateTable(_actionData.TableName);
                    ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.ClearTable:
                    GetOrCreateTable(_actionData.TableName).Clear();
                    ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.ImportTableFile:
                    if (GetOrCreateTable(_actionData.TableName).ImportCsvTotable(Singleton.Instance<SavedData>().GetVariableData(_actionData.FileName)))
                        ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.ExportTableFile:
                    CommonHelper.SaveStringToFile(GetOrCreateTable(_actionData.TableName).GetTableAsCsv(), Singleton.Instance<SavedData>().GetVariableData(_actionData.FileName));
                    ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.SetCellValue:

                    if (GetOrCreateTable(_actionData.TableName).SetValue(int.Parse(row), col, Singleton.Instance<SavedData>().GetVariableData(_actionData.Value)))
                        ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.GetCellValue:
                    bool hasError = false;
                    string value = GetOrCreateTable(_actionData.TableName).GetValue(int.Parse(row), col, out hasError);
                    AutoApp.Logger.WriteInfoLog("Table GetCellValue got - " + value);
                    if (!hasError)
                    {
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(value);
                        ActionStatus = Enums.Status.Pass;
                    }
                    break;

                case ActionType.GetRowCount:
                    string columnIndex = Singleton.Instance<SavedData>().GetVariableData(_actionData.Column);
                    Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(GetOrCreateTable(_actionData.TableName).GetRowCount(columnIndex).ToString());
                    ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.GetColumnCount:
                    Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(GetOrCreateTable(_actionData.TableName).GetColumnCount().ToString());
                    ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.DeleteTable:
                    if (Singleton.Instance<SavedData>().Tables.ContainsKey(_actionData.TableName))
                    {
                        Singleton.Instance<SavedData>().Tables.Remove(_actionData.TableName);
                        ActionStatus = Enums.Status.Pass;
                    }
                    else
                        AutoApp.Logger.WriteFailLog("Table " + _actionData.TableName + " does not exit");

                    break;

                case ActionType.CopyTable:
                    GetOrCreateTable(_actionData.TargetVar).CopyTable(GetOrCreateTable(_actionData.TableName).GetDataTable());
                    ActionStatus = Enums.Status.Pass;
                    break;
            }
            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("Table Action " + _type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("Table Action " + _type.ToString() + " Failed");
        }

        public TableAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.Table)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.TableName); //1
            Details.Add(_actionData.Value); //2
            Details.Add(_actionData.Row); //3
            Details.Add(_actionData.Column); //4
            Details.Add(_actionData.FileName); //5
            Details.Add(_actionData.TargetVar); //6
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { TableName = Details[1], Value = Details[2], Row = Details[3], Column = Details[4], FileName = Details[5], TargetVar = Details[6] };
        }

        public struct ActionData
        {
            public string TableName { get; set; } //1

            public string Value { get; set; } //2

            public string Row { get; set; } //3

            public string Column { get; set; } //4

            public string FileName { get; set; } //5

            public string TargetVar { get; set; } //6
        }

        public class TableObj
        {
            private DataTable table;

            public void CopyTable(DataTable newTable)
            {
                table = newTable.Copy();
            }

            public DataTable GetDataTable()
            {
                return table;
            }

            public void Clear()
            {
                table.Clear();
            }

            public int GetRowCount(string columnIndex)
            {
                string data = string.Empty;
                int count = 0;

                if (string.IsNullOrEmpty(columnIndex))
                    return table.Rows.Count;
                else
                {
                    int maxRows = table.Rows.Count;
                    int col = int.Parse(columnIndex);
                    for (int i = 0; i < maxRows; i++)
                    {
                        data = table.Rows[count][col].ToString();
                        if (data != "")
                        {
                            count++;
                        }
                        else
                            break;
                    }
                }
                return count;
            }

            public int GetColumnCount()
            {
                return table.Columns.Count;
            }

            private int GetColumnIndex(string columName)
            {
                int index = -1;
                bool res = int.TryParse(columName, out index);
                if (res)
                    return index;

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (table.Columns[0].ColumnName == columName)
                    {
                        index = i;
                        break;
                    }
                }
                return index;
            }

            public bool SetValue(int row, string columnName, object value)
            {
                bool res = false;
                int index = GetColumnIndex(columnName);
                try
                {
                    table.Rows[row][index] = value;
                    res = true;
                }
                catch { }
                return res;
            }

            public string GetValue(int row, string columnName, out bool res)
            {
                string value = string.Empty;
                int index = GetColumnIndex(columnName);
                try
                {
                    value = table.Rows[row][index].ToString();
                    res = false;
                }
                catch
                {
                    res = true;
                }
                return value;
            }

            public string GetTableAsCsv()
            {
                return CommonHelper.DataTableToCSV(table);
            }

            public bool ImportCsvTotable(string fileName)
            {
                List<string> errors = new List<string>();

                CopyTable(CommonHelper.ConvertCSVtoDataTable(fileName, ","));
                //CopyTable(CommonHelper.ImportCsvToDataTable(fileName, ",", out errors));
                return errors.Count == 0;
            }
        }
    }
}