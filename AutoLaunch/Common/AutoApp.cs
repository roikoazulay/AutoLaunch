namespace AutomationCommon
{
    public static class AutoApp
    {
        public static AutoLogger Logger
        {
            get { return Singleton.Instance<AutoLogger>(); }
        }

        public static AppSettings Settings
        {
            get { return Singleton.Instance<AppSettings>(); }
        }
    }
}