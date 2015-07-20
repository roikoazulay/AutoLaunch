using System;
using System.Diagnostics;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class SqliteAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            EexcuteQuery
        }

        public SqliteAction()
            : base(Enums.ActionTypeId.Sqlite)
        {
        }

        public override void Execute()
        {
            //sqlite3.exe -header data.db "SELECT * FROM transactions"; >roi.csv
            string query = Singleton.Instance<SavedData>().GetVariableData(_actionData.Query);
            string command = _actionData.Options + " " + '"' + _actionData.DbName + '"' + " " + '"' + query + '"' + ";";
            AutoApp.Logger.WriteInfoLog("Starting Sqlite " + _type.ToString());
            switch (_type)
            {
                case ActionType.EexcuteQuery:

                    if (ExecuteCommand(command))
                    {
                        ActionStatus = Enums.Status.Pass;
                        AutoApp.Logger.WritePassLog("Sqlite " + _type.ToString() + " Passed");
                    }
                    break;
            }
        }

        private bool ExecuteCommand(string command)
        {
            var procStartInfo = new ProcessStartInfo(AutomationCommon.StaticFields.UTILS + "\\sqlite3.exe", command);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;

            // Now we create a process, assign its ProcessStartInfo and start it
            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();

            //Wait for the process to exit or time out.
            proc.WaitForExit(5000);
            //Check to see if the proces
            HasFinished = true;
            string output = proc.StandardOutput.ReadToEnd();

            //if no error return true
            if (output.ToLower().Contains("error"))
            {
                AutoApp.Logger.WriteFailLog(string.Format("Sqlite failure {0} , error detected {1}", _type, output));
                return false;
            }

            if (Singleton.Instance<SavedData>().Variables.ContainsKey(_actionData.TargetVar))
                Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(output);
            else
            {
                AutoApp.Logger.WriteFailLog(string.Format("Sqlite failure, Variable {0} does not exist", _actionData.TargetVar));
                return false;
            }

            return true;
        }

        public SqliteAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.Sqlite)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.DbName); //1
            Details.Add(_actionData.Options); //2
            Details.Add(_actionData.Query); //2
            Details.Add(_actionData.TargetVar); //3
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { DbName = Details[1], Options = Details[2], Query = Details[3], TargetVar = Details[4] };
        }

        public struct ActionData
        {
            public string DbName { get; set; } //1

            public string Options { get; set; } //2

            public string Query { get; set; } //3

            public string TargetVar { get; set; } //4
        }
    }
}