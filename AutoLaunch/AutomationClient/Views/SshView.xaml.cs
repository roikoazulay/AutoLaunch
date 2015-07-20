using System;
using System.Windows;
using System.Windows.Controls;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class SshView : Window, IAutomationWindow
    {
        public SshView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (SshAction.SshActionType)Enum.Parse(typeof(SshAction.SshActionType), operationCmb.Text);

            var sshAction = new SshAction(type, new SshAction.SshActionData() { Host = varCmb.Text, UserName = userNameTxb.Text, Password = passwordTxb.Text, Command = commandCmb.Text, LogFileName = logCmb.Text });
            var entity = new StepEntity(sshAction);
            entity.Comment = string.Format("SSH {0} Action", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                varCmb.Text = selectedStepEntity.Action.Details[1];
                userNameTxb.Text = selectedStepEntity.Action.Details[2];
                passwordTxb.Text = selectedStepEntity.Action.Details[3];
                commandCmb.Text = selectedStepEntity.Action.Details[4];
                logCmb.Text = selectedStepEntity.Action.Details[5];
            }
        }

        private void varCmb_TextChanged(object sender, TextChangedEventArgs e)
        {
            logCmb.Text = "SSH_LOG" + varCmb.Text + ".log";
        }

        private void varCmb_DropDownClosed(object sender, EventArgs e)
        {
            logCmb.Text = "SSH_LOG" + varCmb.Text + ".log";
        }
    }
}