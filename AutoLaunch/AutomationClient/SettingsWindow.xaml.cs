using System.Windows;
using AutomationCommon;

namespace AutomationClient
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            Singleton.Instance<AppSettings>().Refresh();
            startUpFolderTxb.Text = Singleton.Instance<AppSettings>().StatupFolder;
            autoSaveChk.IsChecked = Singleton.Instance<AppSettings>().ScriptAutoSave;
            portTxb.Text = Singleton.Instance<AppSettings>().ClientServerPort.ToString();
            clearAllVarsChk.IsChecked = Singleton.Instance<AppSettings>().ClearAllVars;
            TearDownTxb.Text = Singleton.Instance<AppSettings>().TearDownScript;
            scriptShowDetailsChk.IsChecked = Singleton.Instance<AppSettings>().ShowStepDetails;
            ServerIpTxb.Text = Singleton.Instance<AppSettings>().ServerIp;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            AutoApp.Settings.SetConfigSetting("StartupFolder", startUpFolderTxb.Text, "AutoLaunch.config");
            AutoApp.Settings.SetConfigSetting("ScriptAutoSave", autoSaveChk.IsChecked.ToString(), "AutoLaunch.config");
            AutoApp.Settings.SetConfigSetting("ClientServerPort", portTxb.Text, "AutoLaunch.config");
            AutoApp.Settings.SetConfigSetting("ClearAllVars", clearAllVarsChk.IsChecked.ToString(), "AutoLaunch.config");
            AutoApp.Settings.SetConfigSetting("TearDownScript", TearDownTxb.Text, "AutoLaunch.config");
            AutoApp.Settings.SetConfigSetting("ShowStepDetails", scriptShowDetailsChk.IsChecked.ToString(), "AutoLaunch.config");
            AutoApp.Settings.SetConfigSetting("ServerIp", ServerIpTxb.Text, "AutoLaunch.config");
            HelperClass.ShowMessageBox("Settings", "Setting saved successfully,Server restart required!");
        }

        private void scriptSelectBtn_Click(object sender, RoutedEventArgs e)
        {
            TearDownTxb.Text = HelperClass.OpenFileDialog();
        }

        private void statrupFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            startUpFolderTxb.Text = HelperClass.OpenFolderDialog();
        }
    }
}