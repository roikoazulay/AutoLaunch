using AutomationCommon;

namespace AutomationServer
{
    public class ConditionAction : ActionBase
    {
        public ConditionActionData _actionData;
        public bool GoToLableshouldActivated = false;
        public ScriptObj ActiveScript { get; set; }

        public ConditionAction()
            : base(Enums.ActionTypeId.Conditions)
        {
        }

        public ConditionAction(ConditionActionData actionData)
            : base(Enums.ActionTypeId.Conditions)
        {
            _actionData = actionData;
            Details.Add(_actionData.SourceVar);//1
            Details.Add(_actionData.Result); //2
            Details.Add(_actionData.Lable); //3
        }

        //public  void Execute1()
        //{
        //    if (Singleton.Instance<SavedData>().Variables.ContainsKey(_actionData.SourceVar))
        //    {
        //        if (Singleton.Instance<SavedData>().Variables[_actionData.SourceVar].GetValue() == _actionData.Result)
        //        {
        //            if (Singleton.Instance<ActionHandler>().Flowhandler.FindLableIndex(_actionData.Lable))
        //            {
        //                AutoApp.Logger.WritePassLog(string.Format("Condition Activated, GoTo lable on index {0}",
        //                                                          Singleton.Instance<ActionHandler>().Flowhandler.
        //                                                              LableIndex));

        //                ActionStatus = Enums.Status.Pass;
        //            }
        //            else
        //            {
        //                AutoApp.Logger.WriteFailLog(string.Format("Lable {0} was not found on script {1}",
        //                                                          _actionData.Lable,
        //                                                          Singleton.Instance<ActionHandler>().Flowhandler.
        //                                                              ActiveScriptEntity.
        //                                                              ShortName()));
        //                ActionStatus = Enums.Status.Fail;
        //            }
        //        }
        //        else
        //        {
        //            AutoApp.Logger.WriteWarningLog("Condition was not met,continuing next step");
        //            ActionStatus = Enums.Status.Pass;
        //        }
        //    }
        //    else
        //    {
        //        AutoApp.Logger.WritePassLog(string.Format("Variable {0} does not exist", _actionData.SourceVar));
        //        ActionStatus = Enums.Status.Fail;
        //    }
        //}

        public  void Execute1()
        {
            if (Singleton.Instance<SavedData>().Variables.ContainsKey(_actionData.SourceVar))
            {
                //should fix here if the condition was not met
                if (Singleton.Instance<SavedData>().Variables[_actionData.SourceVar].GetValue() == Singleton.Instance<SavedData>().GetVariableData(_actionData.Result))
                {
                    Singleton.Instance<ActionHandler>().Flowhandler.FindLableIndex(_actionData.Lable, ActiveScript);

                    if (ActiveScript.NextepIndex != -1)
                    {
                        AutoApp.Logger.WritePassLog(string.Format("Condition Activated, GoTo label on index {0}, label name: {1}", ActiveScript.NextepIndex, _actionData.Lable));
                        ActionStatus = Enums.Status.Pass;
                    }
                    else
                        ActionStatus = Enums.Status.Fail;
                }
                else
                {
                    ActiveScript.NextepIndex = -1;
                    AutoApp.Logger.WriteWarningLog("Condition was not met,continuing next step");
                    ActionStatus = Enums.Status.Pass;
                }
            }
            else
            {
                AutoApp.Logger.WritePassLog(string.Format("Variable {0} does not exist", _actionData.SourceVar));
                ActionStatus = Enums.Status.Fail;
            }
        }


        public override void Execute()
        {

            if (Singleton.Instance<SavedData>().Variables[_actionData.SourceVar].GetValue() == Singleton.Instance<SavedData>().GetVariableData(_actionData.Result))
            {
                GoToLableshouldActivated = true;
                AutoApp.Logger.WritePassLog(string.Format("Condition was met , GoTo label  {0}", _actionData.Lable));
                ActionStatus = Enums.Status.Pass;
            }
            else
            {
                GoToLableshouldActivated = false;
                AutoApp.Logger.WriteWarningLog("Condition was not met,continuing next step");
                ActionStatus = Enums.Status.Pass;
            }
        }

        public override void Construct()
        {
            _actionData = new ConditionActionData() { SourceVar = Details[0], Result = Details[1], Lable = Details[2] };
        }
    }

    public struct ConditionActionData
    {
        public string SourceVar { get; set; } //1

        public string Result { get; set; } //2

        public string Lable { get; set; }//3
    }
}