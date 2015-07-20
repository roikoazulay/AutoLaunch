using AutomationCommon;

namespace AutomationServer
{
    internal class StartUp
    {
        private static void Main(string[] args)
        {
            AutomationManager a = new AutomationManager(args);

            AutoApp.Logger.WriteInfoLog("AutoLunch exiting");
            System.Environment.Exit((int)a.handler.ActiveTestSuite.Status);
        }
    }
}