using AutomationCommon;

namespace AutomationClient
{
    public class ModuleHelper
    {
        public static System.Windows.Window WindowFactory(Enums.ActionTypeId id)
        {
            if (id == Enums.ActionTypeId.Non)
            {
                id = Singleton.Instance<SaveData>().SelectedStepEntity.Action.TypeId;
            }

            System.Windows.Window win = null;

            switch (id)
            {
                case Enums.ActionTypeId.Sleep:
                    win = new AutomationClient.Views.SleepView();
                    break;

                case Enums.ActionTypeId.RunProgram:
                    win = new AutomationClient.Views.RunProgramView();
                    break;

                case Enums.ActionTypeId.VariablesOperations:
                    win = new AutomationClient.Views.VariableView();
                    break;

                case Enums.ActionTypeId.TextOperations:
                    win = new AutomationClient.Views.TextManipulationView();
                    break;

                case Enums.ActionTypeId.Lable:
                    win = new AutomationClient.Views.LableView();
                    break;

                case Enums.ActionTypeId.Conditions:
                    win = new AutomationClient.Views.ConditionView();
                    break;

                case Enums.ActionTypeId.Rs232Operations:
                    win = new AutomationClient.Views.Rs232View();
                    break;

                case Enums.ActionTypeId.Timers:
                    win = new AutomationClient.Views.TimersView();
                    break;

                case Enums.ActionTypeId.SSH:
                    win = new AutomationClient.Views.SshView();
                    break;

                case Enums.ActionTypeId.Telnet:
                    win = new AutomationClient.Views.TelnetView();
                    break;

                case Enums.ActionTypeId.Sftp:
                    win = new AutomationClient.Views.SftpView();
                    break;

                case Enums.ActionTypeId.Sqlite:
                    win = new AutomationClient.Views.SqlitView();
                    break;

                case Enums.ActionTypeId.DateTime:
                    win = new AutomationClient.Views.DateTimeView();
                    break;

                case Enums.ActionTypeId.Table:
                    win = new AutomationClient.Views.TableView();
                    break;

                case Enums.ActionTypeId.GuiAutomation:
                    win = new AutomationClient.Views.AutoItUI();
                    break;

                case Enums.ActionTypeId.MessageBox:
                    win = new AutomationClient.Views.MessageView();
                    break;

                case Enums.ActionTypeId.RelayControl:
                    win = new AutomationClient.Views.RelayControlView();
                    break;

                case Enums.ActionTypeId.WaynSim:
                    win = new AutomationClient.Views.WayneSimView();
                    break;

                case Enums.ActionTypeId.ScriptExecute:
                    win = new AutomationClient.Views.ScriptView();
                    break;

                case Enums.ActionTypeId.Ping:
                    win = new AutomationClient.Views.PingView();
                    break;

                case Enums.ActionTypeId.Horison80PowerSupplyAction:
                    win = new AutomationClient.Views.Hr80PSView();
                    break;

                case Enums.ActionTypeId.MotorController:
                    win = new AutomationClient.Views.MotorControlerView();
                    break;

                case Enums.ActionTypeId.FileInfo:
                    win = new AutomationClient.Views.FileView();
                    break;

                case Enums.ActionTypeId.EmailAction:
                    win = new AutomationClient.Views.EmailView();
                    break;

                case Enums.ActionTypeId.SystemAction:
                    win = new AutomationClient.Views.SystemView();
                    break;

                case Enums.ActionTypeId.ListAction:
                    win = new AutomationClient.Views.ListActionView();
                    break;

                case Enums.ActionTypeId.DictionaryAction:
                    win = new AutomationClient.Views.DictionaryActionView();
                    break;

                case Enums.ActionTypeId.SwitchAction:
                    win = new AutomationClient.Views.SwitchView();
                    break;

                case Enums.ActionTypeId.ServerComAction:
                    win = new AutomationClient.Views.ServerComView();
                    break;
            }

            return win;
        }
    }
}