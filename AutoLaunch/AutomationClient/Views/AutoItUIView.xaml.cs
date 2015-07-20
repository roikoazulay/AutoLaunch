using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class AutoItUI : Window, IAutomationWindow
    {
        public AutoItUI()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (AutoItUiAction.ActionType)Enum.Parse(typeof(AutoItUiAction.ActionType), operationCmb.Text);
            var data = new AutoItUiAction.ActionData { WindowTitle = winTitleCmb.Text, ControlID = controlIdTxb.Text, Text = textCmb.Text, TargetVariable = targetCmb.Text };
            var action = new AutoItUiAction(type, data);
            var entity = new StepEntity(action);
            entity.Comment = string.Format("GUI Automation {0}", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                winTitleCmb.Text = selectedStepEntity.Action.Details[1];
                controlIdTxb.Text = selectedStepEntity.Action.Details[2];
                textCmb.Text = selectedStepEntity.Action.Details[3];
                targetCmb.Text = selectedStepEntity.Action.Details[4];
            }
        }
    }
}