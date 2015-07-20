using System;
using System.Diagnostics;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class RunProgram : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            Eexcute,
            Kill
        }

        public RunProgram()
            : base(Enums.ActionTypeId.RunProgram)
        {
        }

        public RunProgram(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.RunProgram)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.Program); //1
            Details.Add(_actionData.Parms); //2
            Details.Add(_actionData.WaitForClose); //3
        }

        public void ExecuteCommandSync()
        {
            string programName = Singleton.Instance<SavedData>().GetVariableData(_actionData.Program);
            string param = Singleton.Instance<SavedData>().GetVariableData(_actionData.Parms);

            ActionStatus = Enums.Status.Fail;
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                var procStartInfo = new ProcessStartInfo(programName, param);

                //procStartInfo.RedirectStandardOutput = true;
                //procStartInfo.UseShellExecute = false;
                // procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;

                proc.Start();
                // Get the output into a string
                //  string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                // Console.WriteLine(result);
                //Wait for the process to exit or time out.
                if (bool.Parse(_actionData.WaitForClose))
                    proc.WaitForExit();
                //{
                //    int timeout = 60;

                //    while (timeout > 0)
                //    {
                //        if (proc.HasExited)
                //            break;

                //        timeout--;
                //        new System.Threading.ManualResetEvent(false).WaitOne(1000);
                //    }

                //}

                //Check to see if the proces
                HasFinished = true;
                ActionStatus = Enums.Status.Pass;
            }
            catch (Exception objException)
            {
                AutoApp.Logger.WriteFatalLog(objException.ToString());
            }
        }

        public void KillProcess()
        {
            try
            {
                Process[] processlist = Process.GetProcesses();

                foreach (Process proc in processlist)
                {
                    if (proc.ProcessName == _actionData.Program)
                        proc.Kill();
                }

                ActionStatus = Enums.Status.Pass;
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
            }
        }

        public override void Construct()
        {
            if (Details.Count == 2)
            {
                _type = (ActionType)Enum.Parse(typeof(ActionType), ActionType.Eexcute.ToString());
                _actionData = new ActionData() { Program = Details[0], Parms = Details[1] };
            }
            else
            {
                _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
                _actionData = new ActionData() { Program = Details[1], Parms = Details[2], WaitForClose = Details[3] };
            }
        }

        public struct ActionData
        {
            public string Program { get; set; } //1

            public string Parms { get; set; } //2

            public string WaitForClose { get; set; } //3
        }

        public override void Execute()
        {
            switch (_type)
            {
                case ActionType.Eexcute:
                    AutoApp.Logger.WriteInfoLog(string.Format("Starting Run Program action {0} with parameters {1} ,wait for exit {2}", _actionData.Program, _actionData.Parms, _actionData.WaitForClose));
                    ExecuteCommandSync();
                    AutoApp.Logger.WriteInfoLog("Finished Run Program action");
                    break;

                case ActionType.Kill:
                    AutoApp.Logger.WriteInfoLog(string.Format("Killing program name {0} ", _actionData.Program));
                    KillProcess();
                    AutoApp.Logger.WriteInfoLog("Kill Program finished");
                    break;
            }
        }
    }
}