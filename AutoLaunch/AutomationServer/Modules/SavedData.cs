using System.Collections.Generic;
using System.Diagnostics;
using AutomationCommon;

namespace AutomationServer
{
    public class SavedData
    {
        private SavedData()
        {
            ReleaseAll();
        }

        #region Tables Save data

        public Dictionary<string, AutomationServer.Actions.TableAction.TableObj> Tables { get; set; }

        #endregion Tables Save data

        #region Variables saved data

        // public Dictionary<string, IVariableData> InternalVariables { get; set; }

        public Dictionary<string, IVariableData> Variables { get; set; }

        public struct VariableData : IVariableData
        {
            private string _value;
            private readonly string _name;
            private readonly bool _isPermanent;//cant be deleted

            public VariableData(string name, string value, bool isPermanent = false)
            {
                _name = name;
                _value = value;
                _isPermanent = isPermanent;
            }

            public string GetValue()
            {
                return _value;
            }

            public void SetValue(string newValue)
            {
                _value = newValue;
                if (newValue.Length < 1000)//display the value if it is short
                    AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} value was set to: {1}", _name, System.Environment.NewLine + newValue));
                else
                    AutoApp.Logger.WriteInfoLog(string.Format("Variable {0} value was changed (value is larger than 1000 characters)", _name));
            }

            public bool IsPermanent()
            {
                return _isPermanent;
            }

            public string GetName()
            {
                return _name;
            }
        }

        #endregion Variables saved data

        #region Serial Communication saved data

        public Dictionary<string, AbstractSerialCommunication> SerialCommunications { get; set; }

        #endregion Serial Communication saved data

        #region SSH Communication saved data

        public Dictionary<string, SshGranados> SshCommunications { get; set; }

        #endregion SSH Communication saved data

        #region Telent Communication saved data

        public Dictionary<string, TelnetClass> TelnetCommunications { get; set; }

        #endregion Telent Communication saved data

        # region Timers

        public Dictionary<string, Stopwatch> TimerList { get; set; }

        # endregion

        # region Labels
        //  public Dictionary<string, LableObj> LableList { get; set; }
        # endregion

        #region RemoteServer

        public Dictionary<string, ICommunication> RemoteServerCommunications { get; set; }

        #endregion RemoteServer

        #region ListObject saved data

        public Dictionary<string, List<string>> ListObj { get; set; }

        #endregion ListObject saved data

        #region DictionaryObject saved data

        public Dictionary<string, Dictionary<string, string>> DictionaryListObj { get; set; }

        #endregion DictionaryObject saved data

        #region Methods

        //returns variable data
        public string GetVariableData(string variable)
        {
            //if ((variable.Contains("{")) && (variable.Contains("}")))
            //    variable = CommonHelper.ExtructValueBetween("{", "}", variable);

            var v = Singleton.Instance<SavedData>().Variables;
            if (Singleton.Instance<SavedData>().Variables.ContainsKey(variable))
                variable = Singleton.Instance<SavedData>().Variables[variable].GetValue();

            return variable;
        }

        public void ReleaseAll()
        {
            try
            {
                RemoteServerCommunications = new Dictionary<string, ICommunication>();
                Variables = new Dictionary<string, IVariableData>();
                for (int i = 1; i < 10; i++)
                    Variables.Add("Params" + i.ToString(), new VariableData("Params" + i.ToString(), ""));

                if (SerialCommunications != null)
                    foreach (string ab in SerialCommunications.Keys)
                        SerialCommunications[ab].DisConnect();

                SerialCommunications = new Dictionary<string, AbstractSerialCommunication>();

                TimerList = new Dictionary<string, Stopwatch>();

                if (SshCommunications != null)
                    foreach (string ab in SshCommunications.Keys)
                        SshCommunications[ab].DisConnect();

                SshCommunications = new Dictionary<string, SshGranados>();

                // ToDo  - Disconnect telnet
                if (TelnetCommunications == null)
                    TelnetCommunications = new Dictionary<string, TelnetClass>();
                Tables = new Dictionary<string, Actions.TableAction.TableObj>();

                ListObj = new Dictionary<string, List<string>>();
                DictionaryListObj = new Dictionary<string, Dictionary<string, string>>();
            }
            catch
            {

            }
            
        }

        public void ExportVariableToFile(string fileName)
        {
            foreach (var varName in Variables.Keys)
            {
                CommonHelper.AppandStringToFile(string.Format("======= Variable: {0} ========", varName), fileName);
                CommonHelper.AppandStringToFile(Variables[varName].GetValue(), fileName);
            }

            //foreach (var varName in InternalVariables.Keys)
            //{
            //    CommonHelper.AppandStringToFile(string.Format("======= Internal Variable: {0} ========", varName), fileName);
            //    CommonHelper.AppandStringToFile(InternalVariables[varName].GetValue(), fileName);
            //}
        }

        /// <summary>
        /// this method gets list of parameters (comma separated) and set them on Params1 to Params9
        /// </summary>
        /// <param name="parms"></param>
        public void UpdateParams(string parms)
        {
            if (!string.IsNullOrEmpty(parms))
            {
                var variables = parms.Split(',');
                string varableName = "Params";
                List<string> preValues = new List<string>();
                for (int i = 0; i < variables.Length; i++)
                {
                    var data = Singleton.Instance<SavedData>().GetVariableData(variables[i]);
                    preValues.Add(data);
                }

                for (int i = 0; i < variables.Length; i++)
                    Singleton.Instance<SavedData>().Variables[varableName + (i + 1).ToString()].SetValue(preValues[i]);
            }
        }

        #endregion Methods
    }

    public interface IVariableData //DI
    {
        string GetName();

        string GetValue();

        void SetValue(string newValue);

        bool IsPermanent();//can't be deleted
    }
}