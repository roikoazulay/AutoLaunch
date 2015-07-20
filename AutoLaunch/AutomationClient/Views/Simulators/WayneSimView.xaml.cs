using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class WayneSimView : Window, IAutomationWindow
    {
        public WayneSimView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (WaynPumpAction.ActionType)Enum.Parse(typeof(WaynPumpAction.ActionType), operationCmb.Text);
            var action = new WaynPumpAction(type, new WaynPumpAction.ActionData() { TargetVariable = varCmb.Text, PumpId = pumpCmb.Text, NzlId = nzlCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Wayne Sim {0} Action", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                varCmb.Text = selectedStepEntity.Action.Details[1];
                pumpCmb.Text = selectedStepEntity.Action.Details[2];
                nzlCmb.Text = selectedStepEntity.Action.Details[3];
            }
        }
    }
}