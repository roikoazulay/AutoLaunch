using System;
using System.Collections.Generic;
using System.Configuration;

namespace AutomationCommon
{
    public class AppSettings
    {
        public string ApplicationDirectory { get; set; }

        public string Utils { get; set; }

        public string StatupFolder { get; set; }

        public string StartupScript { get; set; }

        public int ClientServerPort { get; set; }

        public bool ScriptAutoSave { get; set; }

        public int FontSize { get; set; }

        public string FontFamily { get; set; }

        public bool ClearAllVars { get; set; }

        public string TearDownScript { get; set; }

        public bool ShowStepDetails { get; set; }

        public bool ShowStepInfo { get; set; }

        public string ServerIp { get; set; }

        public int RemoteServerPort { get; set; }

        public AppSettings()
        {
            Refresh();
            RemoteServerPort = 2222;
        }

        public void SetConfigSetting(string setting, string newValue, string configFileName)
        {
            var exeConfigurationFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFileName };
            var config = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);

            if (config.AppSettings.Settings[setting] == null)
                config.AppSettings.Settings.Add(setting, newValue);
            else
                config.AppSettings.Settings[setting].Value = newValue;
            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection(configFileName);
        }

        public void Refresh()
        {
            ApplicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var exeConfigurationFileMap = new ExeConfigurationFileMap { ExeConfigFilename = "AutoLaunch.config" };
            var config = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);

            try
            {
                StatupFolder = config.AppSettings.Settings["StartupFolder"].Value;
                ScriptAutoSave = bool.Parse(config.AppSettings.Settings["ScriptAutoSave"].Value);
                FontSize = int.Parse(config.AppSettings.Settings["FontSize"].Value);
                FontFamily = config.AppSettings.Settings["FontFamily"].Value;
                ClientServerPort = int.Parse(config.AppSettings.Settings["ClientServerPort"].Value);
                // StartupScript = config.AppSettings.Settings["StartupScript"].Value;
                ClearAllVars = bool.Parse(config.AppSettings.Settings["ClearAllVars"].Value);
                //override , this cause problems
                ClearAllVars = false;
                Utils = config.AppSettings.Settings["Utils"].Value;
                TearDownScript = config.AppSettings.Settings["TearDownScript"].Value;

                ShowStepDetails = bool.Parse(config.AppSettings.Settings["ShowStepDetails"].Value);
                ShowStepInfo = bool.Parse(config.AppSettings.Settings["ShowStepInfo"].Value);

                ServerIp = config.AppSettings.Settings["ServerIp"].Value;

                //AppDomain.CurrentDomain.BaseDirectory+
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
        }

        public Dictionary<string, string> GetConfigFileKeyValues(string filePath)
        {
            var exeConfigurationFileMap = new ExeConfigurationFileMap { ExeConfigFilename = filePath };
            var config = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
            var keysValues = new Dictionary<string, string>();
            int count = config.AppSettings.Settings.Count;
            string key = string.Empty;
            for (int i = 0; i < count; i++)
            {
                key = config.AppSettings.Settings.AllKeys.GetValue(i).ToString();
                keysValues.Add(key, config.AppSettings.Settings[key].Value.ToString());
            }

            return keysValues;
        }
    }
}