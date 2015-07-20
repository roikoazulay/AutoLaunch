using AutomationCommon;
using AutomationServer.Actions;
using AutomationServer.Actions.RS232;

namespace AutomationServer
{
    public class ActionFactory
    {
        public static ActionBase GetAction(Enums.ActionTypeId id)
        {
            ActionBase action = null;
            switch (id)
            {
                case Enums.ActionTypeId.RunProgram:
                    action = new RunProgram();
                    break;

                case Enums.ActionTypeId.Sleep:
                    action = new SleepAction();
                    break;

                case Enums.ActionTypeId.VariablesOperations:
                    action = new VariablesAction();
                    break;

                case Enums.ActionTypeId.TextOperations:
                    action = new TextManipulationAction();
                    break;

                case Enums.ActionTypeId.Lable:
                    action = new LabelAction();
                    break;

                case Enums.ActionTypeId.Conditions:
                    action = new ConditionAction();
                    break;

                case Enums.ActionTypeId.Rs232Operations:
                    action = new Rs232Action();
                    break;

                case Enums.ActionTypeId.Timers:
                    action = new TimersAction();
                    break;

                case Enums.ActionTypeId.SSH:
                    action = new SshAction();
                    break;

                case Enums.ActionTypeId.Telnet:
                    action = new TelentAction();
                    break;

                case Enums.ActionTypeId.Sftp:
                    action = new SftpAction();
                    break;

                case Enums.ActionTypeId.Sqlite:
                    action = new SqliteAction();
                    break;

                case Enums.ActionTypeId.Table:
                    action = new TableAction();
                    break;

                case Enums.ActionTypeId.GuiAutomation:
                    action = new AutoItUiAction();
                    break;

                case Enums.ActionTypeId.MessageBox:
                    action = new MessageAction();
                    break;

                case Enums.ActionTypeId.RelayControl:
                    action = new RelayControlAction();
                    break;

                case Enums.ActionTypeId.WaynSim:
                    action = new WaynPumpAction();
                    break;

                case Enums.ActionTypeId.ScriptExecute:
                    action = new ScriptAction();
                    break;

                case Enums.ActionTypeId.Ping:
                    action = new PingAction();
                    break;

                case Enums.ActionTypeId.Horison80PowerSupplyAction:
                    action = new Horison80PowerSupplyAction();
                    break;

                case Enums.ActionTypeId.MotorController:
                    action = new MotorControllerAction();
                    break;

                case Enums.ActionTypeId.FileInfo:
                    action = new FileAction();
                    break;

                case Enums.ActionTypeId.SystemAction:
                    action = new SystemAction();
                    break;

                case Enums.ActionTypeId.EmailAction:
                    action = new EmailAction();
                    break;

                case Enums.ActionTypeId.DateTime:
                    action = new DateTimeAction();
                    break;

                case Enums.ActionTypeId.ListAction:
                    action = new ListAction();
                    break;

                case Enums.ActionTypeId.DictionaryAction:
                    action = new DictionaryAction();
                    break;

                case Enums.ActionTypeId.SwitchAction:
                    action = new SwitchAction();
                    break;

                case Enums.ActionTypeId.ServerComAction:
                    action = new RemoteServerAction();
                    break;
            }

            action.Details.Clear();
            return action;
        }
    }
}