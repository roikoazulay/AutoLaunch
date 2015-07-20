using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class EmailView : Window, IAutomationWindow
    {
        public EmailView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (EmailAction.ActionType)Enum.Parse(typeof(EmailAction.ActionType), operationCmb.Text);
            var action = new EmailAction(type, new EmailAction.ActionData() { Recipient = recipientTxb.Text, From = fromTxb.Text, Subject = subjectTxb.Text, Body = bodyTxb.Text, MailServer = mailSrvTxb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Email {0}", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                recipientTxb.Text = selectedStepEntity.Action.Details[1];
                fromTxb.Text = selectedStepEntity.Action.Details[2];
                subjectTxb.Text = selectedStepEntity.Action.Details[3];
                bodyTxb.Text = selectedStepEntity.Action.Details[4];
                mailSrvTxb.Text = selectedStepEntity.Action.Details[5];
            }
        }
    }
}