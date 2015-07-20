using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AutomationCommon;

namespace AutomationServer
{
    public class VariablesAction : ActionBase
    {
        private IVariableData _sourceVar;
        private VariablesActionType _type;
        private OperationData _operationData;
        private string _value = string.Empty;//holds the value for the actions (before the action starts)

        public enum VariablesActionType
        {
            Create, SetVariable, Delete, DeleteAll, Copy, Increment, Decrement, RandomNumber, RandomDouble, RandomText, Equals, NotEqual, LessThan, GreaterThan, LessThanOrEqual, GreaterThanOrEqual, ImportFileToVariable, ExportVariableToFile, AppendToFile, LoadVariableFile, IsEmpty, Div, Multiply, Modulo, RoundUp, RoundDown, Return
        }

        public VariablesAction()
            : base(Enums.ActionTypeId.VariablesOperations)
        {
        }

        public VariablesAction(VariablesActionType type, IVariableData variableData, OperationData operationData)
            : base(Enums.ActionTypeId.VariablesOperations)
        {
            _value = operationData.Value;
            _sourceVar = variableData;
            _operationData = operationData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_operationData.TargetVar);//1
            Details.Add(_operationData.FileName);//2
            Details.Add(_operationData.Value);//3
            Details.Add(variableData.GetName());//4
            // Details.Add(variableData.GetValue());//5
            Details.Add(variableData.IsPermanent().ToString(CultureInfo.InvariantCulture));//5 (not used)
        }

        public bool Init()
        {
            _value = Singleton.Instance<SavedData>().GetVariableData(_operationData.Value);
            if (Singleton.Instance<SavedData>().Variables.ContainsKey(_sourceVar.GetName()))
            {
                _sourceVar = Singleton.Instance<SavedData>().Variables[_sourceVar.GetName()];
                return true;
            }

            return false;
        }

        public override void Execute()
        {
            if (!Init())
            {
                if ((_type != VariablesActionType.Create) && (_type != VariablesActionType.LoadVariableFile))
                {
                    AutoApp.Logger.WriteFailLog(string.Format("Variable {0} does not exist ", _sourceVar.GetName()));
                    return;
                }
            }

            AutoApp.Logger.WriteInfoLog("Starting Variables Action " + _type.ToString());
            switch (_type)
            {
                case VariablesActionType.RandomNumber:
                    GenerateRandom();
                    break;

                case VariablesActionType.RandomDouble:
                    GenerateRandomDouble();
                    break;

                case VariablesActionType.RandomText:
                    GenerateRandomText();
                    break;

                case VariablesActionType.Delete:
                    DeleteVariable();
                    break;

                case VariablesActionType.DeleteAll:
                    DeleteAll();
                    break;

                case VariablesActionType.SetVariable:
                    SetVariable();
                    break;

                case VariablesActionType.Create:
                    Create();
                    break;

                case VariablesActionType.LoadVariableFile:
                    LoadVariablesFile();
                    break;

                case VariablesActionType.Increment:
                    IncrementVariable();
                    break;

                case VariablesActionType.Decrement:
                    DecrementVariable();
                    break;

                case VariablesActionType.Equals:
                    CompareVariables(true);
                    break;

                case VariablesActionType.NotEqual:
                    CompareVariables(false);
                    break;

                case VariablesActionType.LessThan:
                    CompareGreaterOrLess(false);
                    break;

                case VariablesActionType.GreaterThan:
                    CompareGreaterOrLess(true);
                    break;

                case VariablesActionType.LessThanOrEqual:
                    CompareGreaterOrLessEqual(false);
                    break;

                case VariablesActionType.GreaterThanOrEqual:
                    CompareGreaterOrLessEqual(true);
                    break;

                case VariablesActionType.ImportFileToVariable:
                    ImportExportFile(true);
                    break;

                case VariablesActionType.ExportVariableToFile:
                    ImportExportFile(false);
                    break;

                case VariablesActionType.Copy:
                    CopyVar();
                    break;

                case VariablesActionType.AppendToFile:
                    AppendToFile();
                    break;

                case VariablesActionType.IsEmpty:
                    IsEmpty();
                    break;

                case VariablesActionType.Div:
                    Div();
                    break;

                case VariablesActionType.Multiply:
                    Multiply();
                    break;

                case VariablesActionType.Modulo:
                    Modulo();
                    break;

                case VariablesActionType.RoundDown:
                    RoundDown();
                    break;

                case VariablesActionType.RoundUp:
                    RoundUp();
                    break;
            }

            //if target var is configured copy source var to target
            if (Singleton.Instance<SavedData>().Variables.ContainsKey(_operationData.TargetVar))
            {
                bool res = ActionStatus == Enums.Status.Pass ? true : false;
                Singleton.Instance<SavedData>().Variables[_operationData.TargetVar].SetValue(res.ToString());
                AutoApp.Logger.WriteInfoLog(string.Format("Target variable {0} value was set to {1}",
                                                          _operationData.TargetVar,
                                                          res));

                if (ActionStatus == Enums.Status.Fail)
                {
                    ActionStatus = Enums.Status.Pass;
                    AutoApp.Logger.WriteWarningLog("Action failed ...Change status to Passed - target variable exist");
                }
            }

            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("Variables Action " + _type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("Variables Action " + _type.ToString() + " Failed");
        }

        private void RoundUp()
        {
            // Math.Round(3.445, 2, MidpointRounding.AwayFromZero); //Returns 3.45
            decimal val = decimal.Parse(Singleton.Instance<SavedData>().GetVariableData(_sourceVar.GetName()));
            int digits = int.Parse(Singleton.Instance<SavedData>().GetVariableData(_operationData.Value));
            var retVal = Math.Round(val, digits, MidpointRounding.AwayFromZero);
            _sourceVar.SetValue(retVal.ToString());
            ActionStatus = Enums.Status.Pass;
        }

        private void RoundDown()
        {
            // Math.Round(3.445, 2, MidpointRounding.AwayFromZero); //Returns 3.45
            decimal val = decimal.Parse(Singleton.Instance<SavedData>().GetVariableData(_sourceVar.GetName()));
            int digits = int.Parse(Singleton.Instance<SavedData>().GetVariableData(_operationData.Value));
            var retVal = Math.Round(val, digits, MidpointRounding.ToEven);
            _sourceVar.SetValue(retVal.ToString());
            ActionStatus = Enums.Status.Pass;
        }

        private void Modulo()
        {
            decimal newVal = decimal.Parse(Singleton.Instance<SavedData>().GetVariableData(_sourceVar.GetName())) % decimal.Parse(Singleton.Instance<SavedData>().GetVariableData(_operationData.Value));
            _sourceVar.SetValue(newVal.ToString());
            ActionStatus = Enums.Status.Pass;
        }

        private void Multiply()
        {
            decimal newVal = decimal.Parse(Singleton.Instance<SavedData>().GetVariableData(_sourceVar.GetName())) * decimal.Parse(Singleton.Instance<SavedData>().GetVariableData(_operationData.Value));
            _sourceVar.SetValue(newVal.ToString());
            ActionStatus = Enums.Status.Pass;
        }

        private void Div()
        {
            decimal newVal = decimal.Parse(Singleton.Instance<SavedData>().GetVariableData(_sourceVar.GetName())) / decimal.Parse(Singleton.Instance<SavedData>().GetVariableData(_operationData.Value));
            _sourceVar.SetValue(newVal.ToString());
            ActionStatus = Enums.Status.Pass;
        }

        private void IsEmpty()
        {
            if (string.IsNullOrEmpty(Singleton.Instance<SavedData>().Variables[_sourceVar.GetName()].GetValue()))
            {
                AutoApp.Logger.WritePassLog(string.Format("Variable {0} is empty", _sourceVar.GetName()));
                ActionStatus = Enums.Status.Pass;
            }
            else
                AutoApp.Logger.WritePassLog(string.Format("Variable {0} is not empty", _sourceVar.GetName()));
        }

        private void GenerateRandomText()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            Singleton.Instance<SavedData>().Variables[_sourceVar.GetName()].SetValue(path);
            ActionStatus = Enums.Status.Pass;
        }

        private void GenerateRandom()
        {
            Random random = new Random();
            var num = _value.Split(',');
            int minValue = int.Parse(Singleton.Instance<SavedData>().GetVariableData(num[0]));
            int maxValue = int.Parse(Singleton.Instance<SavedData>().GetVariableData(num[1]));
            int randomNumber = random.Next(minValue, maxValue);
            Singleton.Instance<SavedData>().Variables[_sourceVar.GetName()].SetValue(randomNumber.ToString());
            ActionStatus = Enums.Status.Pass;
        }

        private void GenerateRandomDouble()
        {
            CultureInfo ci = new CultureInfo("en-us");
            Random random = new Random();
            var num = _value.Split(',');
            int minValue = int.Parse(Singleton.Instance<SavedData>().GetVariableData(num[0]));
            int maxValue = int.Parse(Singleton.Instance<SavedData>().GetVariableData(num[1]));
            int precision = int.Parse(Singleton.Instance<SavedData>().GetVariableData(num[2]));
            int randomNumber = random.Next(minValue, maxValue);

            double rndNum = randomNumber;
            if (randomNumber < maxValue)
                rndNum = randomNumber + random.NextDouble();

            var ret = rndNum.ToString("F0" + precision.ToString(), ci);
            Singleton.Instance<SavedData>().Variables[_sourceVar.GetName()].SetValue(ret.ToString());
            ActionStatus = Enums.Status.Pass;
        }

        private void CopyVar()
        {
            //this enables to copy variable content to another variable.
            //first we check if the target variable contains a name of another variable and copy it in to it (variable value can contain a name of another variable)
            string targetVarName = Singleton.Instance<SavedData>().GetVariableData(_operationData.Value);
            if (Singleton.Instance<SavedData>().Variables.ContainsKey(targetVarName))
            {
                Singleton.Instance<SavedData>().Variables[targetVarName].SetValue(_sourceVar.GetValue());
                AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} was copied to Variable {1}", _sourceVar.GetName(), targetVarName));
            }
            else
            {
                Singleton.Instance<SavedData>().Variables[_operationData.Value].SetValue(_sourceVar.GetValue());
                AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} was copied to Variable {1}", _sourceVar.GetName(), _operationData.Value));
            }

            ActionStatus = Enums.Status.Pass;
        }

        private void LoadVariablesFile()
        {
            string fileName = Singleton.Instance<SavedData>().GetVariableData(_operationData.FileName);
            List<string> errors = new List<string>();
            var table = CommonHelper.ImportCsvToDataTable(fileName, ",", out errors);

            string varName;
            string varValue;
            int rowCount = table.Rows.Count;
            int colCount = table.Columns.Count;
            if ((colCount < 2) || (errors.Count != 0))
            {
                AutoApp.Logger.WriteFailLog("Load Variables File failure , invalid file");
                ActionStatus = Enums.Status.Fail;
                return;
            }

            for (int row = 0; row < rowCount; row++)
            {
                varName = table.Rows[row][0].ToString();
                varValue = table.Rows[row][1].ToString();
                if (Singleton.Instance<SavedData>().Variables.ContainsKey(varName))
                {
                    AutoApp.Logger.WriteWarningLog(string.Format("Variable {0} already exist", varName));
                    Singleton.Instance<SavedData>().Variables[varName].SetValue(varValue);
                }
                else
                {
                    IVariableData iv = new SavedData.VariableData(varName, varValue);
                    Singleton.Instance<SavedData>().Variables.Add(varName, iv);
                }
            }

            ActionStatus = Enums.Status.Pass;
        }

        private void ImportExportFile(bool import)
        {
            string fileName = Singleton.Instance<SavedData>().GetVariableData(_operationData.FileName);
            if (import)
            {
                string data = string.Empty;
                bool fileExist = CommonHelper.ReadTextFile(fileName, ref data);
                if (fileExist)
                {
                    if (string.IsNullOrEmpty(data))
                        AutoApp.Logger.WriteWarningLog(fileName + "is empty");
                    _sourceVar.SetValue(data);
                    ActionStatus = Enums.Status.Pass;
                }
            }
            else
            {
                CommonHelper.SaveStringToFile(_sourceVar.GetValue(), fileName);
                ActionStatus = Enums.Status.Pass;
            }
        }

        private void AppendToFile()
        {
            string fileName = Singleton.Instance<SavedData>().GetVariableData(_operationData.FileName);
            bool res = CommonHelper.AppandStringToFile(_sourceVar.GetValue(), fileName);
            if (res)
                ActionStatus = Enums.Status.Pass;
        }

        private void CompareGreaterOrLessEqual(bool graterEqual)
        {
            if (graterEqual)
            {
                if (decimal.Parse(_sourceVar.GetValue()) >= decimal.Parse(_value))
                {
                    AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} is greater or Equal then Variable {1}", _sourceVar.GetName(),
                                                             _value));
                    ActionStatus = Enums.Status.Pass;
                }
                else
                {
                    AutoApp.Logger.WriteFailLog(
                        string.Format("Variable compare failed ,Variable {0} value Less than Variable {1}",
                                      _sourceVar.GetName(), _value));
                    ActionStatus = Enums.Status.Fail;
                }
            }
            else
            {
                if (decimal.Parse(_sourceVar.GetValue()) <= decimal.Parse(_value))
                {
                    AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} is Less or Equal then Variable {1}", _sourceVar.GetName(),
                                                             _value));
                    ActionStatus = Enums.Status.Pass;
                }
                else
                {
                    AutoApp.Logger.WriteFailLog(string.Format("Variable {0} is greater then Variable {1}", _sourceVar.GetName(),
                                                               _value));
                    ActionStatus = Enums.Status.Fail;
                }
            }
        }

        private void CompareGreaterOrLess(bool greater)
        {
            if (greater)
            {
                if (decimal.Parse(_sourceVar.GetValue()) > decimal.Parse(_value))
                {
                    AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} is greater then Variable {1}", _sourceVar.GetName(),
                                                            _value));
                    ActionStatus = Enums.Status.Pass;
                }
                else
                {
                    AutoApp.Logger.WriteFailLog(
                        string.Format("Variable compare failed ,Variable {0} value Less than Variable {1}",
                                      _sourceVar.GetName(), _value));
                    ActionStatus = Enums.Status.Fail;
                }
            }
            else
            {
                if (decimal.Parse(_sourceVar.GetValue()) < decimal.Parse(_value))
                {
                    AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} is Less then {1}", _sourceVar.GetName(),
                                                             _value));
                    ActionStatus = Enums.Status.Pass;
                }
                else
                {
                    AutoApp.Logger.WriteFailLog(string.Format("Variable {0} is greater then Variable {1}", _sourceVar.GetName(),
                                                              _value));
                    ActionStatus = Enums.Status.Fail;
                }
            }
        }

        private void CompareVariables(bool equal)
        {
            decimal var1, var2;
            bool res1 = decimal.TryParse(_sourceVar.GetValue(), out var1);
            bool res2 = decimal.TryParse(_value, out var2);

            if (equal)
            {
                if (res1 && res2)
                {
                    if (var1 == var2)
                    {
                        AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} & {1} are equals - value {2}", _sourceVar.GetName(), _value, _sourceVar.GetValue()));
                        ActionStatus = Enums.Status.Pass;
                    }
                    else
                    {
                        AutoApp.Logger.WriteFailLog(
                            string.Format("Variable compare failed ,Variable {0} is {1} & Variable {2} is {3}",
                                          _sourceVar.GetName(), _sourceVar.GetValue(), _value, _value));
                        ActionStatus = Enums.Status.Fail;
                    }
                    return;
                }

                if (_sourceVar.GetValue() == _value)
                {
                    AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} &  {1} are equals - value {2}", _sourceVar.GetName(), _value, _sourceVar.GetValue()));
                    ActionStatus = Enums.Status.Pass;
                }
                else
                {
                    AutoApp.Logger.WriteFailLog(
                        string.Format("Variable compare failed ,Variable {0} is {1} & Variable {2} is {3}",
                                      _sourceVar.GetName(), _sourceVar.GetValue(), _value, _value));
                    ActionStatus = Enums.Status.Fail;
                }
            }
            else
            {
                if (res1 && res2)
                {
                    if (var1 != var2)
                    {
                        AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} &  {1} are not equals", _sourceVar.GetName(), _value));
                        ActionStatus = Enums.Status.Pass;
                    }
                    else
                    {
                        AutoApp.Logger.WriteFailLog(
                            string.Format("Variable {0} &  {1} are equals - value {2}",
                                          _sourceVar.GetName(), _sourceVar.GetValue(), _value));
                        ActionStatus = Enums.Status.Fail;
                    }

                    return;
                }

                if (_sourceVar.GetValue() != _value)
                {
                    AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} &  {1} are not equals", _sourceVar.GetName(), _value));
                    ActionStatus = Enums.Status.Pass;
                }
                else
                {
                    AutoApp.Logger.WriteFailLog(
                        string.Format("Variable {0} &  {1} are equals - value {2}",
                                      _sourceVar.GetName(), _sourceVar.GetValue(), _value));
                    ActionStatus = Enums.Status.Fail;
                }
            }
        }

        private void IncrementVariable()
        {
            decimal newVal = decimal.Parse(Singleton.Instance<SavedData>().GetVariableData(_sourceVar.GetName())) + decimal.Parse(Singleton.Instance<SavedData>().GetVariableData(_operationData.Value));
            _sourceVar.SetValue(newVal.ToString());
            ActionStatus = Enums.Status.Pass;
        }

        private void DecrementVariable()
        {
            decimal newVal = decimal.Parse(Singleton.Instance<SavedData>().GetVariableData(_sourceVar.GetName())) - decimal.Parse(Singleton.Instance<SavedData>().GetVariableData(_operationData.Value));
            _sourceVar.SetValue(newVal.ToString());
            ActionStatus = Enums.Status.Pass;
        }

        private void SetVariable()
        {
            if (Singleton.Instance<SavedData>().Variables.ContainsKey(_sourceVar.GetName()))
            {
                Singleton.Instance<SavedData>().Variables[_sourceVar.GetName()].SetValue(_value);
                ActionStatus = Enums.Status.Pass;
            }
            else
            {
                AutoApp.Logger.WriteFailLog(string.Format("Variable {0} does not exist", _sourceVar.GetName()));
                ActionStatus = Enums.Status.Fail;
            }
        }

        private void DeleteVariable()
        {
            AutoApp.Logger.WriteInfoLog(string.Format("Deleting Variable {0}", _sourceVar.GetName()));
            if (Singleton.Instance<SavedData>().Variables.ContainsKey(_sourceVar.GetName()))
            {
                //deletes only unpermanent vas
                if (!Singleton.Instance<SavedData>().Variables[_sourceVar.GetName()].IsPermanent())
                {
                    Singleton.Instance<SavedData>().Variables.Remove(_sourceVar.GetName());
                    AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} was deleted", _sourceVar.GetName()));
                    ActionStatus = Enums.Status.Pass;
                }
                else
                {
                    AutoApp.Logger.WriteFailLog(string.Format("Variable {0} is permanent", _sourceVar.GetName()));
                    ActionStatus = Enums.Status.Fail;
                }
            }
            else
            {
                AutoApp.Logger.WriteFailLog(string.Format("Variable {0} does not exist", _sourceVar.GetName()));
                ActionStatus = Enums.Status.Fail;
            }
        }

        private void DeleteAll()
        {
            Singleton.Instance<SavedData>().Variables.Clear();
            AutoApp.Logger.WriteInfoLog("All Variables where deleted");
            ActionStatus = Enums.Status.Pass;
        }

        private void Create()
        {
            if (Singleton.Instance<SavedData>().Variables.ContainsKey(_sourceVar.GetName()))
            {
                AutoApp.Logger.WriteWarningLog(string.Format("Variable {0} already exist", _sourceVar.GetName()));
                Singleton.Instance<SavedData>().Variables[_sourceVar.GetName()].SetValue(Singleton.Instance<SavedData>().GetVariableData(_operationData.Value));
            }
            else
            {
                Singleton.Instance<SavedData>().Variables.Add(_sourceVar.GetName(), _sourceVar);
                Singleton.Instance<SavedData>().Variables[_sourceVar.GetName()].SetValue(Singleton.Instance<SavedData>().GetVariableData(_operationData.Value));
                AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} created with value {1}", _sourceVar.GetName(), _sourceVar.GetValue()));
            }

            ActionStatus = Enums.Status.Pass;
        }

        public override void Construct()
        {
            _type = (VariablesActionType)Enum.Parse(typeof(VariablesActionType), Details[0]);
            _operationData = new OperationData { TargetVar = Details[1], FileName = Details[2], Value = Details[3] };
            // _sourceVar = new SavedData.VariableData(Details[4], Details[5], Convert.ToBoolean(Details[6));
            _sourceVar = new SavedData.VariableData(Details[4], Details[3], Convert.ToBoolean(Details[5]));
        }
    }

    public struct OperationData
    {
        public string TargetVar { get; set; }

        public string FileName { get; set; }

        public string Value { get; set; }
    }
}