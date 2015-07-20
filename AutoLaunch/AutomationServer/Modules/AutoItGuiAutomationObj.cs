using System.Threading;
using AutoItX3Lib;

namespace AutomationServer.Modules
{
    public class AutoItGuiAutomationObj
    {
        private static AutoItX3 autoit = new AutoItX3();
        private const int RETRAY = 3;

        public static bool ControlClick1(string windowTitle, string controlID, bool CheckIsGray = false)
        {
            int result = autoit.ControlClick(windowTitle, "", controlID);
            if (result == 1)
            {
                string isEnabled = string.Empty;
                if (CheckIsGray)
                {
                    Thread.Sleep(200);
                    isEnabled = autoit.ControlCommand(windowTitle, "", controlID, "IsEnabled", "");
                    return isEnabled == "0";//Returns 1 if Control is enabled, 0 otherwise
                }
            }
            else
                return false;

            return true;
        }

        public static bool ControlClick(string windowTitle, string controlID, bool CheckIsGray = false)
        {
            int count = 0;
            while (count < RETRAY)
            {
                count++;
                int result = autoit.ControlClick(windowTitle, "", controlID);
                if (result == 1)
                {
                    string isEnabled = string.Empty;
                    if (CheckIsGray)
                    {
                        new System.Threading.ManualResetEvent(false).WaitOne(200);
                        isEnabled = autoit.ControlCommand(windowTitle, "", controlID, "IsEnabled", "");
                        return isEnabled == "0";//Returns 1 if Control is enabled, 0 otherwise
                    }
                    return true;
                }

                new System.Threading.ManualResetEvent(false).WaitOne(500);
            }
            return false;
        }

        public static bool ControlSendText1(string windowTitle, string controlID, string textToSend)
        {
            int result = autoit.ControlSetText(windowTitle, "", controlID, textToSend);

            //int result = autoit.ControlSend(windowTitle, "", controlID, textToSend);
            if (result == 1)
            {
                Thread.Sleep(200);
                string text = autoit.ControlGetText(windowTitle, "", controlID);
                return text == textToSend;
            }
            else
                return false;
        }

        public static bool ControlSendText(string windowTitle, string controlID, string textToSend)
        {
            int count = 0;
            while (count < RETRAY)
            {
                count++;
                int result = autoit.ControlSetText(windowTitle, "", controlID, textToSend);
                //int result = autoit.ControlSend(windowTitle, "", controlID, textToSend);
                if (result == 1)
                {
                    new System.Threading.ManualResetEvent(false).WaitOne(200);
                    string text = autoit.ControlGetText(windowTitle, "", controlID);
                    return text == textToSend;
                }
                new System.Threading.ManualResetEvent(false).WaitOne(500);
            }

            return false;
        }

        public static bool ControlGetText(string windowTitle, string controlID, ref string retVal)
        {
            retVal = autoit.ControlGetText(windowTitle, "", controlID);
            return autoit.error == 0;
        }

        public static bool ControlCheck(string windowTitle, string controlID)
        {
            autoit.ControlCommand(windowTitle, "", controlID, "Check", "");
            Thread.Sleep(200);
            string IsChecked = autoit.ControlCommand(windowTitle, "", controlID, "IsChecked", "");
            return IsChecked == "1";
        }

        public static bool ControlUnCheck(string windowTitle, string controlID)
        {
            autoit.ControlCommand(windowTitle, "", controlID, "UnCheck", "");
            Thread.Sleep(200);
            string IsChecked = autoit.ControlCommand(windowTitle, "", controlID, "IsChecked", "");
            return IsChecked == "0";
        }
    }
}