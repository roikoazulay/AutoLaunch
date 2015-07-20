using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class PingView : Window, IAutomationWindow
    {
        public PingView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(targetVarCmb.Text))
            {
                HelperClass.ShowErrorMessage("Target variable is empty");
                return;
            }
            var type = (PingAction.ActionType)Enum.Parse(typeof(PingAction.ActionType), operationCmb.Text);
            var action = new PingAction(type, new PingAction.ActionData() { Host = hostCmb.Text, Loops = loopCountCmb.Text, TargetVar = targetVarCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Ping Action to host {0}", hostCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                hostCmb.Text = selectedStepEntity.Action.Details[1];
                loopCountCmb.Text = selectedStepEntity.Action.Details[2];
                targetVarCmb.Text = selectedStepEntity.Action.Details[3];
            }
        }
    }
}