using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class MotorControlerView : Window, IAutomationWindow
    {
        public MotorControlerView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (MotorControllerAction.ActionType)Enum.Parse(typeof(MotorControllerAction.ActionType), operationCmb.Text);
            var action = new MotorControllerAction(type, new MotorControllerAction.ActionData() { ComPort = portCmb.Text, Value = valueCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Motor Controller {0}", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            infoImage.Visibility = System.Windows.Visibility.Hidden;
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                portCmb.Text = selectedStepEntity.Action.Details[1];
                valueCmb.Text = selectedStepEntity.Action.Details[2];
            }
        }

        private void operationCmb_DropDownClosed(object sender, EventArgs e)
        {
            if (operationCmb.Text.Contains("Move"))
                infoImage.Visibility = System.Windows.Visibility.Visible;
            else
                infoImage.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}