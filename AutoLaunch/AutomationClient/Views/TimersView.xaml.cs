using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;

namespace AutomationClient.Views
{
    public partial class TimersView : Window, IAutomationWindow
    {
        public TimersView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (TimersAction.TimersActionType)Enum.Parse(typeof(TimersAction.TimersActionType), operationCmb.Text);
            var timerAction = new TimersAction(type, new TimersAction.TimersActionData() { TimerName = timerNameTxb.Text, TargetVar = targetVarCmb.Text });
            var entity = new StepEntity(timerAction);
            entity.Comment = string.Format("Timer {0} - {1}", operationCmb.Text, timerNameTxb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                timerNameTxb.Text = selectedStepEntity.Action.Details[1];
                targetVarCmb.Text = selectedStepEntity.Action.Details[2];
            }
        }
    }
}