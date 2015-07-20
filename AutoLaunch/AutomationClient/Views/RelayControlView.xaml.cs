using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class RelayControlView : Window, IAutomationWindow
    {
        public RelayControlView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (RelayControlAction.ActionType)Enum.Parse(typeof(RelayControlAction.ActionType), operationCmb.Text);
            var action = new RelayControlAction(type, new RelayControlAction.ActionData() { ComPort = portCmb.Text, Vendor = vendorCmb.Text, RelayNumber = relayNumberCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("RelayControl {0}", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                portCmb.Text = selectedStepEntity.Action.Details[1];
                vendorCmb.Text = selectedStepEntity.Action.Details[2];
                relayNumberCmb.Text = selectedStepEntity.Action.Details[3];
            }
        }

        private void operationCmb_DropDownClosed(object sender, EventArgs e)
        {
            if ((RelayControlAction.ActionType)Enum.Parse(typeof(RelayControlAction.ActionType), operationCmb.Text) == RelayControlAction.ActionType.SetRelayStatus)
                HelperClass.ShowMessageBox("Info", "Set Relays Status in hexadecimal - FFFFFFFF will open all relays & 00000000 will close all relays");
        }
    }
}