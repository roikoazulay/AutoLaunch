namespace AutomationCommon
{
    public class Enums
    {
        public enum ActionTypeId
        {
            Non = 0,
            Sleep = 1,
            RunProgram = 2,
            Clipboard = 3,
            VariablesOperations = 4,
            TextOperations = 5,
            Lable = 6,
            Conditions = 7,
            Rs232Operations = 8,
            Timers = 9,
            SSH = 10,
            Telnet = 11,
            Sftp = 12,
            Sqlite = 13,
            DateTime = 14,
            Table = 15,
            GuiAutomation = 16,
            MessageBox = 17,
            RelayControl = 18,
            WaynSim = 19,
            ScriptExecute = 20,
            Ping = 21,
            Horison80PowerSupplyAction = 22,
            MotorController = 23,
            FileInfo = 24,
            SystemAction = 25,
            EmailAction = 26,
            ListAction = 27,
            DictionaryAction = 28,
            SwitchAction = 29,
            ServerComAction = 30
        }

        public enum Status
        {
            Pass, Fail, Skipped, NoN, Info, Exception, Warning
        }

        public enum OnFailerAction
        {
            SkipTest = 1, Continue = 2, Stop = 3, non = 4
        }
    }
}