using System;
using System.IO;
using AutomationCommon;

namespace AutomationServer.Actions
{
    public class FileAction : ActionBase
    {
        private ActionData _actionData;
        private ActionType _type;

        public FileAction()
            : base(Enums.ActionTypeId.FileInfo)
        {
        }

        public FileAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.FileInfo)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.FileName); //1
            Details.Add(_actionData.TargetVar); //2
        }

        public enum ActionType
        {
            GetFileLength,
            Exist,
            NotExist,
            CreationTime,
            IsReadOnly,
            LastAccessTime,
            LastWriteTime,
            Delete,
            Rename,
            Copy,
            Move
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { FileName = Details[1], TargetVar = Details[2] };
        }

        public override void Execute()
        {
            string file = Singleton.Instance<SavedData>().GetVariableData(_actionData.FileName);
            FileInfo fileinfo = new FileInfo(file);

            bool fileExist = fileinfo.Exists;

            ////protection before testing
            //if (_type != ActionType.Exist)
            //{
            //    if (!fileinfo.Exists)
            //        AutoApp.Logger.WriteWarningLog(string.Format("File {0} does not exist ", file));
            //    else
            //        fileExist = true;
            //}

            AutoApp.Logger.WriteInfoLog("Starting File Action " + _type.ToString());
            switch (_type)
            {
                case ActionType.GetFileLength:
                    if (!fileExist)
                        return;
                    AutoApp.Logger.WriteInfoLog(string.Format("Got File length {0} ", fileinfo.Length));
                    Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(fileinfo.Length.ToString());
                    ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.Exist:
                    if (fileExist)
                    {
                        AutoApp.Logger.WritePassLog(string.Format(string.Format("File {0} exist is {1} ", file, fileExist)));
                        ActionStatus = Enums.Status.Pass;
                    }
                    else
                        AutoApp.Logger.WriteFailLog(string.Format(string.Format("File {0} does not exist", file)));


                    if (!string.IsNullOrEmpty(_actionData.TargetVar))
                    { 
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(fileExist.ToString());
                        ActionStatus = Enums.Status.Pass;
                    }
                       

                    break;

                case ActionType.NotExist:

                    if (!fileExist)
                    {
                        AutoApp.Logger.WritePassLog(string.Format(string.Format("File {0} does not exist", file)));
                        ActionStatus = Enums.Status.Pass;
                        if (!string.IsNullOrEmpty(_actionData.TargetVar))
                            Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue("False");
                    }
                    else
                        AutoApp.Logger.WriteFailLog(string.Format(string.Format("File {0} exist", file)));

                    if (!string.IsNullOrEmpty(_actionData.TargetVar))
                    {
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(fileExist.ToString());
                        ActionStatus = Enums.Status.Pass;
                    }


                    break;

                case ActionType.Delete:
                    if (fileExist)
                        fileinfo.Delete();
                    ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.Copy:
                    if (fileExist)
                    {
                        fileinfo.CopyTo(Singleton.Instance<SavedData>().GetVariableData(_actionData.TargetVar));
                        ActionStatus = Enums.Status.Pass;
                    }
                    break;

                case ActionType.Move:
                case ActionType.Rename:
                    if (fileExist)
                    {
                        fileinfo.MoveTo(Singleton.Instance<SavedData>().GetVariableData(_actionData.TargetVar));
                        ActionStatus = Enums.Status.Pass;
                    }
                    break;

                case ActionType.CreationTime:
                    if (fileExist)
                    {
                        AutoApp.Logger.WriteInfoLog(string.Format(string.Format("File {0} Creation Time is {1} ", file, fileinfo.CreationTime)));
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(fileinfo.CreationTime.ToString());
                        ActionStatus = Enums.Status.Pass;
                    }
                    break;

                case ActionType.LastAccessTime:
                    if (fileExist)
                    {
                        AutoApp.Logger.WriteInfoLog(string.Format(string.Format("File {0} Last Access Time is {1} ", file, fileinfo.LastAccessTime)));
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(fileinfo.LastAccessTime.ToString());
                        ActionStatus = Enums.Status.Pass;
                    }
                    break;

                case ActionType.LastWriteTime:
                    if (fileExist)
                    {
                        AutoApp.Logger.WriteInfoLog(string.Format(string.Format("File {0} Last Write Time is {1} ", file, fileinfo.LastWriteTime)));
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(fileinfo.LastWriteTime.ToString());
                        ActionStatus = Enums.Status.Pass;
                    }
                    break;

                case ActionType.IsReadOnly:
                    if (fileExist)
                    {
                        AutoApp.Logger.WriteInfoLog(string.Format(string.Format("File {0} Read Only state is {1} ", file, fileinfo.IsReadOnly)));
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(fileinfo.IsReadOnly.ToString());
                        ActionStatus = Enums.Status.Pass;
                    }
                    break;
            }
        }

        public struct ActionData
        {
            public string FileName { get; set; } //1

            public string TargetVar { get; set; } //2
        }
    }
}