using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class Hr80PSView : Window, IAutomationWindow
    {
        public Hr80PSView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (Horison80PowerSupplyAction.ActionType)Enum.Parse(typeof(Horison80PowerSupplyAction.ActionType), operationCmb.Text);
            var action = new Horison80PowerSupplyAction(type, new Horison80PowerSupplyAction.ActionData() { Host = hostCmb.Text, Port = portTxb.Text, Parm1 = param1Txb.Text, TargetVar = targetVarCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("HR-80 Power Supply Action {0}", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                hostCmb.Text = selectedStepEntity.Action.Details[1];
                portTxb.Text = selectedStepEntity.Action.Details[2];
                targetVarCmb.Text = selectedStepEntity.Action.Details[3];
                param1Txb.Text = selectedStepEntity.Action.Details[4];
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}