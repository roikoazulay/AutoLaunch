using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class TelnetView : Window, IAutomationWindow
    {
        public TelnetView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (TelentAction.TelentActionType)Enum.Parse(typeof(TelentAction.TelentActionType), operationCmb.Text);

            var action = new TelentAction(type, new TelentAction.TelentActionData() { Host = hostTxb.Text, Port = portTxb.Text, Command = commandTxb.Text, TargetVar = varCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Telnet {0} Action", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                hostTxb.Text = selectedStepEntity.Action.Details[1];
                portTxb.Text = selectedStepEntity.Action.Details[2];
                commandTxb.Text = selectedStepEntity.Action.Details[3];
                varCmb.Text = selectedStepEntity.Action.Details[4];
            }
        }
    }
}