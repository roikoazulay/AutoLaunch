using System;
using AutomationCommon;

namespace AutomationServer.Actions
{
    //currently not implemented
    public class EnvironmentVariable : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        public enum ActionType
        {
            GetEnvironmentVariable,
            SetEnvironmentVariable
        }

        public EnvironmentVariable()
            : base(Enums.ActionTypeId.DateTime)
        {
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog("Starting Environment " + _type.ToString());
            string value = string.Empty;
            switch (_type)
            {
                case ActionType.GetEnvironmentVariable:
                    value = Environment.GetEnvironmentVariable(_actionData.Value, EnvironmentVariableTarget.Process);
                    Singleton.Instance<SavedData>().Variables[_actionData.SourceVar].SetValue(value);
                    ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.SetEnvironmentVariable:
                    value = Singleton.Instance<SavedData>().GetVariableData(_actionData.SourceVar);
                    Environment.SetEnvironmentVariable(Singleton.Instance<SavedData>().GetVariableData(_actionData.Value), value);
                    ActionStatus = Enums.Status.Pass;
                    break;
            }

            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("Environment Action " + _type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("Environment Action " + _type.ToString() + " Failed");
        }

        public EnvironmentVariable(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.DateTime)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.SourceVar); //1
            Details.Add(_actionData.Value); //2
            Details.Add(_actionData.Type); //3
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { SourceVar = Details[1], Value = Details[2], Type = Details[3] };
        }

        public struct ActionData
        {
            public string SourceVar { get; set; } //1

            public string Value { get; set; } //2

            public string Type { get; set; } //3  -Process,User,Machine
        }
    }
}