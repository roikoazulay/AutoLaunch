namespace AutomationCommon
{
    public class StaticFields
    {
        public static string SCRIPT_EXTENTION = ".spt";
        public static string TEST_EXTENTION = ".tst";
        public static string SUITE_EXTENTION = ".tsu"; //to fix extention for suite
        public static string INITIAL_PATH;
        public static string LOG_PATH;
        public static string SCRIPT_PATH;
        public static string TEST_PATH;
        public static string SUITE_PATH;
        public static string BACKUP_FOLDER;
        public static string UTILS;
        public static string SUITE_LOOKUP_TABLE;

        //public static string LOG_PATH = INITIAL_PATH + "\\Logs";
        //public static string SCRIPT_PATH = INITIAL_PATH + "\\Scripts";
        //public static string TEST_PATH = INITIAL_PATH + "\\Tests";
        //public static string SUITE_PATH = INITIAL_PATH + "\\Suites";

        static StaticFields()
        {
            INITIAL_PATH = AutoApp.Settings.StatupFolder;
            LOG_PATH = INITIAL_PATH + "\\Bin\\Logs";
            SCRIPT_PATH = INITIAL_PATH + "\\Scripts";
            TEST_PATH = INITIAL_PATH + "\\Tests";
            SUITE_PATH = INITIAL_PATH + "\\Suites";
            BACKUP_FOLDER = INITIAL_PATH + "\\Backup";
            UTILS = AutoApp.Settings.Utils;
            SUITE_LOOKUP_TABLE = StaticFields.INITIAL_PATH + "\\Bin\\SuiteTable.csv";
        }
    }
}