using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class SftpView : Window, IAutomationWindow
    {
        public SftpView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (SftpAction.SftpActionType)Enum.Parse(typeof(SftpAction.SftpActionType), operationCmb.Text);
            var action = new SftpAction(type, new SftpAction.SftpActionData() { Host = varCmb.Text, UserName = userNameTxb.Text, Password = passwordTxb.Text, Command1 = command1Txb.Text, Command2 = command2Txb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Sftp {0} Action", operationCmb.Text);
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
                command1Txb.Text = selectedStepEntity.Action.Details[4];
                command2Txb.Text = selectedStepEntity.Action.Details[5];
            }
        }
    }
}