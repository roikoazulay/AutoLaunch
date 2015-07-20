using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AutomationCommon;
using System.Text;

namespace AutomationServer
{
    public class TextManipulationAction : ActionBase
    {
        private TextActionType _type;
        private TextActionData _textActionData;

        private string _soureString;
        private string _searchString;

        private bool _supressFailure = false;

        public enum TextActionType
        {
            Contains,
            NotContains,
            Substring,
            SubstringBefore,
            SubstringAfter,
            SubstringByIndex,
            ExtructRow,
            ExtructRows,
            ExtructLastRow,
            CountOccurrences,
            Equal,
            NotEqual,
            Replace,
            join,
            Trim,
            TrimStart,
            TrimEnd,
            RegexContains,
            GetLength,
            Split,
            RegexExtructRows,
            ConvertHexToAscii
            //GetLastRow
        }

        private enum SubstringType
        {
            Before, After, Regular, ByIndex, ByRow, ByRows
        }

        public TextManipulationAction()
            : base(Enums.ActionTypeId.TextOperations)
        {
        }

        public TextManipulationAction(TextActionType type, TextActionData actionData)
            : base(Enums.ActionTypeId.TextOperations)
        {
            _textActionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_textActionData.SourceVar); //1
            Details.Add(_textActionData.TargetVar); //2
            Details.Add(_textActionData.Value); //3
            Details.Add(_textActionData.Length); //4
        }

        public void Init()
        {
            _soureString = Singleton.Instance<SavedData>().GetVariableData(_textActionData.SourceVar);
            _searchString = Singleton.Instance<SavedData>().GetVariableData(_textActionData.Value);
        }

        private enum TrimType
        {
            Trim, TrimStart, TrimEnd
        }

        public override void Execute()
        {
            Init();
            _supressFailure = Singleton.Instance<SavedData>().Variables.ContainsKey(_textActionData.TargetVar);
            switch (_type)
            {
                case TextActionType.GetLength:
                    GetLength();
                    break;

                case TextActionType.join:
                    JoinString();
                    break;

                case TextActionType.Contains:
                    FindString(true);
                    break;

                case TextActionType.NotContains:
                    FindString(false);
                    break;

                case TextActionType.SubstringByIndex:
                    SubString(SubstringType.ByIndex);
                    break;

                case TextActionType.Substring:
                    SubString(SubstringType.Regular);
                    break;

                case TextActionType.SubstringAfter:
                    SubString(SubstringType.After);
                    break;

                case TextActionType.SubstringBefore:
                    SubString(SubstringType.Before);
                    break;

                case TextActionType.ExtructRow:
                    SubString(SubstringType.ByRow);
                    break;

                case TextActionType.ExtructRows:
                    SubString(SubstringType.ByRows);
                    break;

                case TextActionType.CountOccurrences:
                    CountStringOccurrences();
                    break;

                case TextActionType.Equal:
                    CompareStrings(true);
                    break;

                case TextActionType.NotEqual:
                    CompareStrings(false);
                    break;

                case TextActionType.Replace:
                    ReplaceStr();
                    break;

                case TextActionType.Trim:
                    Trim(TrimType.Trim);
                    break;

                case TextActionType.TrimEnd:
                    Trim(TrimType.TrimEnd);
                    break;

                case TextActionType.TrimStart:
                    Trim(TrimType.TrimStart);
                    break;

                case TextActionType.RegexContains:
                    RegxContains();
                    break;

                case TextActionType.RegexExtructRows:
                    RegexExtructRows();
                    break;

                case TextActionType.Split:
                    SplitMethod();
                    break;

                case TextActionType.ExtructLastRow:
                    GetLastRow();
                    break;

                case TextActionType.ConvertHexToAscii:
                    ConvertHexToAscii();
                    break;
            }

            switch (_type)
            {
                case TextActionType.NotEqual:
                case TextActionType.Equal:
                case TextActionType.NotContains:
                case TextActionType.Contains:
                case TextActionType.RegexContains:

                    //if target var is configured copy source var to target
                    if (Singleton.Instance<SavedData>().Variables.ContainsKey(_textActionData.TargetVar))
                    {
                        bool res = ActionStatus == Enums.Status.Pass ? true : false;
                        Singleton.Instance<SavedData>().Variables[_textActionData.TargetVar].SetValue(res.ToString());
                        AutoApp.Logger.WriteInfoLog(string.Format("Target variable {0} value was set to {1}",
                                                                  _textActionData.TargetVar,
                                                                  res));

                        if (ActionStatus == Enums.Status.Fail)
                        {
                            ActionStatus = Enums.Status.Pass;
                            AutoApp.Logger.WriteWarningLog("Action failed ...Change status to Passed - target variable exist");
                        }
                    }
                    break;
            }
        }

        private void RegexExtructRows()
        {
            var sub = CommonHelper.ExtructLineByRegexString(_searchString, _soureString, true);
            Singleton.Instance<SavedData>().Variables[_textActionData.TargetVar].SetValue(sub);
            ActionStatus = Enums.Status.Pass;
            AutoApp.Logger.WritePassLog("Regex Extracted row completed");

        }

        private void ConvertHexToAscii()
        {
            var ret = CommonHelper.ConvertHexToAscii(_soureString);
            Singleton.Instance<SavedData>().Variables[_textActionData.TargetVar].SetValue(ret);
            ActionStatus = Enums.Status.Pass;
            AutoApp.Logger.WritePassLog("Convert To Ascii row completed");
        }
       
        private void GetLastRow()
        {
            AutoApp.Logger.WriteInfoLog("Get Last Row action");
            StringReader sr = new StringReader(_soureString);
            string lastRow = string.Empty;
            string line = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains(_searchString))
                    lastRow = line;
            }

            Singleton.Instance<SavedData>().Variables[_textActionData.TargetVar].SetValue(lastRow);
            AutoApp.Logger.WritePassLog("Get Last Row Completed");
            ActionStatus = Enums.Status.Pass;
        }

        private void SplitMethod()
        {
            string sourceData = Singleton.Instance<SavedData>().Variables[_textActionData.SourceVar].GetValue();
            var dataArray = sourceData.Split(new string[] { Singleton.Instance<SavedData>().GetVariableData(_searchString) }, StringSplitOptions.None);
            Singleton.Instance<SavedData>().ListObj[_textActionData.TargetVar] = dataArray.ToList();
            ActionStatus = Enums.Status.Pass;
            AutoApp.Logger.WritePassLog(string.Format("Split String Completed ,List Variable {0} value was changed", _textActionData.TargetVar));
        }

        private void GetLength()
        {
            if (_textActionData.TargetVar == null)
            {
                AutoApp.Logger.WriteFailLog("Action failed ...target variable does not exist");
            }
            else
            {
                Singleton.Instance<SavedData>().Variables[_textActionData.TargetVar].SetValue(_soureString.Length.ToString());
                AutoApp.Logger.WritePassLog(string.Format("GetLength Completed - Length is {0}", _soureString.Length));
                ActionStatus = Enums.Status.Pass;
            }
        }

        private void RegxContains()
        {
            if (Regex.Match(_soureString, _searchString).Success)
            {
                ActionStatus = Enums.Status.Pass;
                AutoApp.Logger.WritePassLog("Regex String Found");
            }
            else
                AutoApp.Logger.WriteFailLog("Regex String was not found");
        }

        private void Trim(TrimType type)
        {
            string data = Singleton.Instance<SavedData>().Variables[_textActionData.SourceVar].GetValue();
            char ch;
            if (_searchString == "\\r")
                ch = '\r';
            else if (_searchString == "\\n")
                ch = '\n';
            else
                ch = Convert.ToChar(_searchString);

            if (type == TrimType.Trim)
                data = data.Trim(ch);
            else if (type == TrimType.TrimEnd)
                data = data.TrimEnd(ch);
            else if (type == TrimType.TrimStart)
                data = data.TrimStart(ch);

            Singleton.Instance<SavedData>().Variables[_textActionData.TargetVar].SetValue(data);
            ActionStatus = Enums.Status.Pass;
            AutoApp.Logger.WritePassLog(string.Format("{0} String Completed", type));
        }

        private void JoinString()
        {
            string data = Singleton.Instance<SavedData>().Variables[_textActionData.SourceVar].GetValue() + Singleton.Instance<SavedData>().GetVariableData(_searchString);
            Singleton.Instance<SavedData>().Variables[_textActionData.TargetVar].SetValue(data);
            ActionStatus = Enums.Status.Pass;
            AutoApp.Logger.WritePassLog(string.Format("Join String Completed ,variable {0} value was changed to {1}", _textActionData.TargetVar, data));
        }

        private void CompareStrings(bool equal)
        {
            if (equal)
            {
                if (_soureString.Equals(_searchString))
                {
                    ActionStatus = Enums.Status.Pass;
                    AutoApp.Logger.WritePassLog("Source String equals to target string");
                }
                else
                {
                    ActionStatus = Enums.Status.Fail;
                    AutoApp.Logger.WriteFailLog("Source String not equals to target string");
                }
            }
            else
            {
                if (_soureString.Equals(_searchString))
                {
                    ActionStatus = Enums.Status.Fail;
                    AutoApp.Logger.WriteFailLog("Source String equals to target string");
                }
                else
                {
                    ActionStatus = Enums.Status.Pass;
                    AutoApp.Logger.WritePassLog("Source String not equals to target string");
                }
            }
        }

        private void CountStringOccurrences()
        {
            AutoApp.Logger.WriteInfoLog(string.Format("Count String Occurrences {0} on variable {1}", _searchString, _textActionData.SourceVar));
            // int count = new Regex(_searchString).Matches(_soureString).Count;
            int count = _soureString.Split(new string[] { _searchString }, StringSplitOptions.None).Length - 1;

            Singleton.Instance<SavedData>().Variables[_textActionData.TargetVar].SetValue(count.ToString());
            AutoApp.Logger.WriteInfoLog(string.Format("Counted {0} String Occurrences on variable {1}", count, _textActionData.SourceVar));
            AutoApp.Logger.WritePassLog("Substring Completed");
            ActionStatus = Enums.Status.Pass;
        }

        private void FindString(bool contain)
        {
            if (contain)
            {
                AutoApp.Logger.WriteInfoLog(string.Format("Verify variable {0} contains variable {1} content ({2})",
                                                         _textActionData.SourceVar, _textActionData.Value, _searchString));
                if (string.IsNullOrEmpty(_searchString))
                {
                    AutoApp.Logger.WriteFailLog("Variable " + _textActionData.Value + " is empty");
                    return;
                }

                if (_soureString.Contains(_searchString))
                {
                    AutoApp.Logger.WritePassLog("String found");
                    ActionStatus = Enums.Status.Pass;
                }
                else
                {
                    AutoApp.Logger.WriteFailLog("String not found");
                    ActionStatus = Enums.Status.Fail;
                }
            }
            else
            {
                AutoApp.Logger.WriteInfoLog(string.Format("Verify variable {0} not contains variable {1} content ({2})",
                                                         _textActionData.SourceVar, _textActionData.Value, _searchString));

                if (string.IsNullOrEmpty(_searchString))
                {
                    AutoApp.Logger.WriteFailLog("Variable " + _textActionData.Value + " is empty");
                    return;
                }

                if (!_soureString.Contains(_searchString))
                {
                    AutoApp.Logger.WritePassLog("String not found");
                    ActionStatus = Enums.Status.Pass;
                }
                else
                {
                    AutoApp.Logger.WriteFailLog("String found");
                    ActionStatus = Enums.Status.Fail;
                }
            }
        }

        private void ReplaceStr()
        {
            string sub = string.Empty;

            if (!string.IsNullOrEmpty(_soureString))
            {
                string[] strs = new string[2];
                int startIndex = _textActionData.Value.IndexOf("}");
                int endIndex = _textActionData.Value.LastIndexOf("}");
                strs[0] = _textActionData.Value.Substring(0, startIndex + 1);
                startIndex = _textActionData.Value.LastIndexOf("{");
                strs[1] = _textActionData.Value.Substring(startIndex, endIndex - (startIndex - 1));
                // string [] strs = _textActionData.Value.Split('~');
                if (strs.Length == 2)
                {
                    string oldString = CommonHelper.ExtructValueBetween("{", "}", strs[0]);
                    string newString = CommonHelper.ExtructValueBetween("{", "}", strs[1]);
                    if (oldString == "\\r")
                        oldString = Convert.ToString('\r');

                    if (oldString == "\\n")
                        oldString = Convert.ToString('\n');

                    if (newString == "\\r")
                        newString = Convert.ToString('\r');

                    if (newString == "\\n")
                        newString = Convert.ToString('\n');

                    // sub = _soureString.Replace("\r", newString);
                    sub = _soureString.Replace(Singleton.Instance<SavedData>().GetVariableData(oldString), Singleton.Instance<SavedData>().GetVariableData(newString));
                    Singleton.Instance<SavedData>().Variables[_textActionData.TargetVar].SetValue(sub);
                    AutoApp.Logger.WritePassLog("String replace Completed");
                }
                else
                {
                    AutoApp.Logger.WriteFailLog("replace string not in correct format");
                    return;
                }
            }
            else
            {
                AutoApp.Logger.WriteWarningLog(string.Format("Variable {0} string is empty", _textActionData.SourceVar));
            }

            ActionStatus = Enums.Status.Pass;
        }

        //private string ExtructValueFromRow(string title, string segment)
        //{
        //    string tmpString = "";
        //    int startPos = segment.IndexOf(title);
        //    if (startPos != -1)
        //    {
        //        int endPos = segment.IndexOf(System.Environment.NewLine, startPos);
        //        if (endPos != -1)
        //            tmpString = segment.Substring(startPos + title.Length, endPos - startPos - title.Length);
        //    }

        //    return tmpString;
        //}
        ///// <summary>Finds the value from the end of the startString to the start of the endString </summary>
        //private string ExtructValueBetween(string startString, string endString, string segment)
        //{
        //    string tmpString = "";
        //    try
        //    {
        //        int startPos = segment.IndexOf(startString);
        //        if (startPos == -1)
        //            return "";
        //        int endPos = segment.IndexOf(endString, startPos + 1);

        //        if (endPos == -1)
        //            return "";

        //        tmpString = segment.Substring(startPos + startString.Length, endPos - startPos - startString.Length);
        //    }
        //    catch
        //    {
        //    }
        //    return tmpString;
        //}

        private void SubString(SubstringType type)
        {
            string sub = string.Empty;

            if ((type == SubstringType.ByRow) || (type == SubstringType.ByRows))
            {
                sub = CommonHelper.ExtructLineByString(_searchString, _soureString, type == SubstringType.ByRows);
                if (!string.IsNullOrEmpty(sub))
                {
                    Singleton.Instance<SavedData>().Variables[_textActionData.TargetVar].SetValue(sub);
                    ActionStatus = Enums.Status.Pass;
                    AutoApp.Logger.WritePassLog("Extracted row completed");
                }
                else
                {
                    AutoApp.Logger.WriteWarningLog("Extracted row completed, string was not found");
                    Singleton.Instance<SavedData>().Variables[_textActionData.TargetVar].SetValue(string.Empty);
                }

                return;
            }

            int length = 0;
            if (string.IsNullOrEmpty(_textActionData.Length))
                length = -1;
            else
            {
                length = int.Parse(Singleton.Instance<SavedData>().GetVariableData(_textActionData.Length));
            }

            //---new
            _soureString = Singleton.Instance<SavedData>().GetVariableData(_soureString);
            _searchString = Singleton.Instance<SavedData>().GetVariableData(_searchString);

            if (!string.IsNullOrEmpty(_soureString))
            {
                int sIndex = _soureString.IndexOf(_searchString);

                if (type == SubstringType.ByIndex)
                    sIndex = int.Parse(Singleton.Instance<SavedData>().GetVariableData(_textActionData.Value));

                if (sIndex < 0)
                    AutoApp.Logger.WriteInfoLog("Data to substring was not found or wrong index");
                else
                {
                    switch (type)
                    {
                        case SubstringType.ByIndex:
                        case SubstringType.Regular:
                            if (_soureString.Length > sIndex)
                            {
                                if (length == -1)
                                    sub = _soureString.Substring(sIndex);
                                else
                                    sub = _soureString.Substring(sIndex, length);
                            }
                            else
                                AutoApp.Logger.WriteWarningLog("Data to substring has wrong index");
                            break;

                        case SubstringType.Before:
                            sub = _soureString.Substring(0, sIndex);
                            break;

                        case SubstringType.After:
                            sIndex += _searchString.Length;
                            sub = _soureString.Substring(sIndex);
                            break;

                        case SubstringType.ByRow:
                            sub = CommonHelper.ExtructLineByString(_searchString, _soureString);
                            break;
                    }
                    Singleton.Instance<SavedData>().Variables[_textActionData.TargetVar].SetValue(sub);
                }
                AutoApp.Logger.WritePassLog("Substring Completed");
            }
            else
            {
                AutoApp.Logger.WriteWarningLog(string.Format("Variable {0} string is empty", _textActionData.SourceVar));
            }

            ActionStatus = Enums.Status.Pass;
        }

        public override void Construct()
        {
            _type = (TextActionType)Enum.Parse(typeof(TextActionType), Details[0]);
            _textActionData = new TextActionData() { SourceVar = Details[1], TargetVar = Details[2], Value = Details[3], Length = Details.Count > 4 ? Details[4] : null };
        }
    }

    public struct TextActionData
    {
        public string SourceVar { get; set; } //1

        public string TargetVar { get; set; } //2

        public string Value { get; set; } //3

        public string Length { get; set; } //4
    }
}