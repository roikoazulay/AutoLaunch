using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class MessageView : Window, IAutomationWindow
    {
        public MessageView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (MessageAction.ActionType)Enum.Parse(typeof(MessageAction.ActionType), "ShowMessage");
            var data = new MessageAction.ActionData { Message = messageCmb.Text, TimeOut = timeOutCmb.Text };
            var action = new MessageAction(type, data);
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Message action {0}", "ShowMessage");
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                // operationCmb.Text = selectedStepEntity.Action.Details[0];
                messageCmb.Text = selectedStepEntity.Action.Details[1];
                timeOutCmb.Text = selectedStepEntity.Action.Details[2];
            }
        }
    }
}