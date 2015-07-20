using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class SystemView : Window, IAutomationWindow
    {
        public SystemView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (SystemAction.ActionType)Enum.Parse(typeof(SystemAction.ActionType), operationCmb.Text);
            var action = new SystemAction(type, new SystemAction.ActionData() { TargetVar = targetCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("{0} System Action", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                targetCmb.Text = selectedStepEntity.Action.Details[1];
            }
        }
    }
}