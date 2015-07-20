using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class DateTimeView : Window, IAutomationWindow
    {
        public DateTimeView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (DateTimeAction.ActionType)Enum.Parse(typeof(DateTimeAction.ActionType), operationCmb.Text);
            var action = new DateTimeAction(type, new DateTimeAction.ActionData() { SourceVar = srcVarCmb.Text, TimeFormat = timeFormatCmb.Text, TargetVar = trgVarCmb.Text, Value = valueCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("DateTime {0} Action", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                srcVarCmb.Text = selectedStepEntity.Action.Details[1];
                timeFormatCmb.Text = selectedStepEntity.Action.Details[2];
                trgVarCmb.Text = selectedStepEntity.Action.Details[3];
                if (selectedStepEntity.Action.Details.Count > 4)
                    valueCmb.Text = selectedStepEntity.Action.Details[4];
            }
        }
    }
}