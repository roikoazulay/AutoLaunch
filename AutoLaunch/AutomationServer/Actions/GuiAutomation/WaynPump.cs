using System;
using AutomationCommon;
using AutomationServer.Modules;

namespace AutomationServer.Actions
{
    public class WaynPumpAction : ActionBase
    {
        private ActionType _type;
        private ActionData _actionData;

        private const string TITLE = "Wayne pump simulator ver 2.0.2.0";
        private const string HUP_ID = "[CLASS:TButton; INSTANCE:12]";
        private const string HDW_ID = "[CLASS:TButton; INSTANCE:11]";
        private const string LITERS_ID = "[CLASS:TEdit; INSTANCE:11]";
        private const string MONEY_ID = "[CLASS:TEdit; INSTANCE:9]";
        private const string PRICE_ID = "[CLASS:TEdit; INSTANCE:7]";
        private const string STATUS_ID = "[CLASS:TEdit; INSTANCE:12]";

        private const string START_SIM_ID = "[CLASS:TButton; INSTANCE:19]";
        private const string STOP_SIM_ID = "[CLASS:TButton; INSTANCE:18]";
        private const string NZL_ID = "[CLASS:TGroupButton; INSTANCE:";

        public enum ActionType
        {
            HandleUp,
            HandleDown,
            NzlSelect,
            GetLiters,
            GetMoney,
            GetPrice,
            StartSimulator,
            StoptSimulator
        }

        public WaynPumpAction()
            : base(Enums.ActionTypeId.WaynSim)
        {
        }

        public override void Execute()
        {
            AutoApp.Logger.WriteInfoLog(string.Format("Executing {0} command", _type));
            string retVal = string.Empty;
            switch (_type)
            {
                case ActionType.HandleUp:
                    if (AutoItGuiAutomationObj.ControlClick(TITLE, HUP_ID, true))
                        ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.HandleDown:
                    if (AutoItGuiAutomationObj.ControlClick(TITLE, HDW_ID, true))
                        ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.StartSimulator:
                    int count = 0;
                    if (AutoItGuiAutomationObj.ControlClick(TITLE, START_SIM_ID, false))
                    {
                        string ststusText = "00-notprog";

                        while (ststusText == "00-notprog")
                        {
                            AutoItGuiAutomationObj.ControlGetText(TITLE, STATUS_ID, ref ststusText);
                            new System.Threading.ManualResetEvent(false).WaitOne(1000);
                            count++;
                            if (count > 20)
                            {
                                AutoApp.Logger.WriteFailLog(string.Format("WaynPump communication timeout after {0} sec", count));
                                return;
                            }
                        }

                        ActionStatus = Enums.Status.Pass;
                    }

                    break;

                case ActionType.StoptSimulator:
                    if (AutoItGuiAutomationObj.ControlClick(TITLE, STOP_SIM_ID, false))
                        ActionStatus = Enums.Status.Pass;
                    break;

                case ActionType.GetLiters:
                    if (AutoItGuiAutomationObj.ControlGetText(TITLE, LITERS_ID, ref retVal))
                    {
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVariable].SetValue(retVal);
                        ActionStatus = Enums.Status.Pass;
                    }
                    break;

                case ActionType.GetMoney:
                    if (AutoItGuiAutomationObj.ControlGetText(TITLE, MONEY_ID, ref retVal))
                    {
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVariable].SetValue(retVal);
                        ActionStatus = Enums.Status.Pass;
                    }
                    break;

                case ActionType.GetPrice:
                    if (AutoItGuiAutomationObj.ControlGetText(TITLE, PRICE_ID, ref retVal))
                    {
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVariable].SetValue(retVal);
                        ActionStatus = Enums.Status.Pass;
                    }
                    break;

                case ActionType.NzlSelect:
                    if (AutoItGuiAutomationObj.ControlCheck(TITLE, string.Format("{0} {1}]", NZL_ID, 129 - int.Parse(_actionData.NzlId))))
                        ActionStatus = Enums.Status.Pass;
                    break;
            }
            if (ActionStatus == Enums.Status.Pass)
                AutoApp.Logger.WritePassLog("WaynPump Action " + _type.ToString() + " Passed");
            else
                AutoApp.Logger.WriteFailLog("WaynPump Action " + _type.ToString() + " Failed");
        }

        public WaynPumpAction(ActionType type, ActionData actionData)
            : base(Enums.ActionTypeId.WaynSim)
        {
            _actionData = actionData;
            _type = type;

            Details.Add(type.ToString());
            Details.Add(_actionData.TargetVariable); //1
            Details.Add(_actionData.PumpId); //2
            Details.Add(_actionData.NzlId); //3
            Details.Add(_actionData.Data1); //4
            Details.Add(_actionData.Data2); //5
            Details.Add(_actionData.Data3); //6
        }

        public override void Construct()
        {
            _type = (ActionType)Enum.Parse(typeof(ActionType), Details[0]);
            _actionData = new ActionData() { TargetVariable = Details[1], NzlId = Details[2], PumpId = Details[1], Data1 = Details[2], Data2 = Details[3], Data3 = Details[4] };
        }

        public struct ActionData
        {
            public string TargetVariable { get; set; } //1

            public string PumpId { get; set; } //2

            public string NzlId { get; set; } //3

            public string Data1 { get; set; } //4

            public string Data2 { get; set; } //5

            public string Data3 { get; set; } //6
        }
    }
}