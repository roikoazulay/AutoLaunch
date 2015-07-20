using System;
using System.Diagnostics;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class SftpAction : ActionBase
    {
        private SftpActionType _type;
        private SftpActionData _sftpActionData;

        public enum SftpActionType
        {
            DeleteFile,
            DeleteFolder,
            GetFile,
            PutFile,
            CreateFolder
        }

        public SftpAction()
            : base(Enums.ActionTypeId.Sftp)
        {
        }

        public override void Execute()
        {
            //============================================================================================================
            //make sure to have the security key in the cache (can execute the command in batch file and confirm the key)
            //============================================================================================================

            string hostAddress = Singleton.Instance<SavedData>().GetVariableData(_sftpActionData.Host);

            string command = _sftpActionData.UserName + ":" + _sftpActionData.Password + "@" + hostAddress + " ";//root:ortech@192.168.1.3
            string Command1 = Singleton.Instance<SavedData>().GetVariableData(_sftpActionData.Command1);
            string Command2 = Singleton.Instance<SavedData>().GetVariableData(_sftpActionData.Command2);

            AutoApp.Logger.WriteInfoLog("Starting Sftp " + _type.ToString());
            switch (_type)
            {
                case SftpActionType.DeleteFolder:
                    // winscp.com root:ortech@192.168.1.3  /command "option batch Abort" "rmdir /home2" -hostkey="ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx" "exit"
                    command += "/command " + '"' + "option confirm off" + '"' + " " + '"' + "option batch Abort" + '"' + " " + '"' + "rmdir /" + Command1 + '"';
                    command += " " + " -hostkey=" + '"' + "ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx" + '"' + " " + '"' + "exit" + '"';
                    if (ExecuteSftpCommand(command))
                    {
                        ActionStatus = Enums.Status.Pass;
                        AutoApp.Logger.WritePassLog("Sftp " + _type.ToString() + " Passed");
                    }
                    break;

                case SftpActionType.DeleteFile:
                    // winscp.com root:ortech@192.168.1.3  /command "option confirm off" "option transfer binary" "rm /WebInstall.log" -hostkey="ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx" "exit"
                    command += "/command " + '"' + "option confirm off" + '"' + " " + '"' + "option transfer binary" + '"' + " " + '"' + "rm /" + Command2 + "/" + Command1 + '"';
                    command += " " + '"' + "exit" + '"' + " -hostkey=" + '"' + "ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx" + '"' + " " + '"' + "exit" + '"';
                    if (ExecuteSftpCommand(command))
                    {
                        ActionStatus = Enums.Status.Pass;
                        AutoApp.Logger.WritePassLog("Sftp " + _type.ToString() + " Passed");
                    }
                    break;

                case SftpActionType.CreateFolder:
                    //winscp.com root:ortech@192.168.1.3  /command "option batch Abort" "mkdir /home2" -hostkey="ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx" "exit"
                    command += "/command " + '"' + "option batch Abort" + '"' + " " + '"' + "mkdir /" + Command1 + '"';
                    command += " " + '"' + "exit" + '"' + " -hostkey=" + '"' + "ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx" + '"' + " " + '"' + "exit" + '"';
                    if (ExecuteSftpCommand(command))
                    {
                        ActionStatus = Enums.Status.Pass;
                        AutoApp.Logger.WritePassLog("Sftp " + _type.ToString() + " Passed");
                    }
                    break;

                case SftpActionType.GetFile:
                    //winscp.com root:ortech@192.168.1.3  /command "option confirm off" "option transfer binary" "get /home2/WebInstall.log c:\dell\" -hostkey="ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx" "exit"
                    command += "/command " + '"' + "option confirm off" + '"' + " " + '"' + "option batch Abort" + '"' + " " + '"' + "option transfer binary" + '"' + " " + '"' + "get " + Command1 + " " + Command2 + '"';
                    command += " " + '"' + '"' + " -hostkey=" + '"' + "ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx" + '"' + " " + '"' + "exit" + '"';
                    if (ExecuteSftpCommand(command))
                    {
                        ActionStatus = Enums.Status.Pass;
                        AutoApp.Logger.WritePassLog("Sftp " + _type.ToString() + " Passed");
                    }
                    break;

                case SftpActionType.PutFile:
                    //winscp.com root:ortech@192.168.1.3  /command "option confirm off" "option transfer binary" "cd /home2" "put c:\WebInstall.log" -hostkey="ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx" "exit"
                    command += "/command " + '"' + "option confirm off" + '"' + " " + '"' + "option batch Abort" + '"' + " " + '"' + "option transfer binary" + '"' + " " + '"' + "put " + Command1 + " " + "/" + Command2 + '"';
                    command += " " + '"' + "exit" + '"' + " -hostkey=" + '"' + "ssh-rsa 1024 xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx:xx" + '"';
                    if (ExecuteSftpCommand(command))
                    {
                        ActionStatus = Enums.Status.Pass;
                        AutoApp.Logger.WritePassLog("Sftp " + _type.ToString() + " Passed");
                    }
                    break;
            }
        }

        private bool ExecuteSftpCommand(string command)
        {
            var procStartInfo = new ProcessStartInfo(AutomationCommon.StaticFields.UTILS + "\\winscp.com", command);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;

            // Now we create a process, assign its ProcessStartInfo and start it
            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;

            AutoApp.Logger.WriteInfoLog("Executing SFTP Command " + AutomationCommon.StaticFields.UTILS + "\\winscp.com " + command);
            proc.Start();

            //Wait for the process to exit or time out.
            proc.WaitForExit();
            //Check to see if the proces
            HasFinished = true;
            string output = proc.StandardOutput.ReadToEnd();

            //if no error return true
            if (output.ToLower().Contains("error"))
            {
                if (output.Contains("Error deleting file '/usr/local/orpak/lost+found'"))//this will always return true since this folder cant be deleted
                    return true;
                AutoApp.Logger.WriteFailLog(string.Format("Sftp failure {0} , error detected {1}", _type, output));
                return false;
            }

            return true;
        }

        public SftpAction(SftpActionType type, SftpActionData actionData)
            : base(Enums.ActionTypeId.Sftp)
        {
            _sftpActionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_sftpActionData.Host); //1
            Details.Add(_sftpActionData.UserName); //2
            Details.Add(_sftpActionData.Password); //3
            Details.Add(_sftpActionData.Command1); //4
            Details.Add(_sftpActionData.Command2); //4
        }

        public override void Construct()
        {
            _type = (SftpActionType)Enum.Parse(typeof(SftpActionType), Details[0]);
            _sftpActionData = new SftpActionData() { Host = Details[1], UserName = Details[2], Password = Details[3], Command1 = Details[4], Command2 = Details[5] };
        }

        public struct SftpActionData
        {
            public string Host { get; set; } //1

            public string UserName { get; set; } //2

            public string Password { get; set; } //3

            public string Command1 { get; set; }//4

            public string Command2 { get; set; }//5
        }
    }
}