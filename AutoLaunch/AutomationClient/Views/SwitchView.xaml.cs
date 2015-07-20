using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class SwitchView : Window, IAutomationWindow
    {
        public SwitchView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = SwitchAction.ActionType.SwitchAction;
            var action = new SwitchAction(type, new SwitchAction.ActionData() { Switch = switchCmb.Text, CaseList = GenerateSwitchList(), DefaultScript = defaultScriptTxb.Text + SwitchAction.SwitchDelimiter + paramsDefaultTxb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Switch Action");
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                //selectedStepEntity.Action.Details[0];
                switchCmb.Text = selectedStepEntity.Action.Details[1];
                InitDataList(selectedStepEntity.Action.Details[2]);
                var d = selectedStepEntity.Action.Details[3].Split(new string[] { SwitchAction.SwitchDelimiter }, StringSplitOptions.None);
                defaultScriptTxb.Text = d[0];
                paramsDefaultTxb.Text = d[1];
            }
        }

        private void addCaseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.listView.Items.Add(new SwitchItem { Case = caseCmb.Text, ScriptName = scriptTxb.Text, Params = paramsTxb.Text });
        }

        private void selectScriptBtn_Click(object sender, RoutedEventArgs e)
        {
            scriptTxb.Text = HelperClass.OpenFileDialog();
        }

        public class SwitchItem
        {
            public string Case { get; set; }

            public string ScriptName { get; set; }

            public string Params { get; set; }
        }

        private string GenerateSwitchList()
        {
            string data = string.Empty;
            foreach (var item in listView.Items)
                data += ((SwitchItem)item).Case + SwitchAction.SwitchDelimiter + ((SwitchItem)item).ScriptName + SwitchAction.SwitchDelimiter + ((SwitchItem)item).Params + System.Environment.NewLine;

            return data;
        }

        private void InitDataList(string date)
        {
            var caseList = date.Split('\n');
            foreach (var item in caseList)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var details = item.Split(new string[] { SwitchAction.SwitchDelimiter }, StringSplitOptions.None);
                    this.listView.Items.Add(new SwitchItem { Case = details[0], ScriptName = details[1], Params = details[2].TrimEnd('\r') });
                }
            }
        }

        private void selectDefaultScriptBtn_Click(object sender, RoutedEventArgs e)
        {
            defaultScriptTxb.Text = HelperClass.OpenFileDialog();
        }
    }
}