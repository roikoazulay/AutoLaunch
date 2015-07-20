using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class ServerComView : Window, IAutomationWindow
    {
        public ServerComView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        //private void saveBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    var type = (ServerComAction.ActionType)Enum.Parse(typeof(ServerComAction.ActionType), operationCmb.Text);
        //    var action = new ServerComAction(type, new ServerComAction.ActionData() { Host = hostCmb.Text, Port = portTxb.Text,  Value = valueTxb.Text, TargetVar = targetVarCmb.Text });
        //    var entity = new StepEntity(action);
        //    entity.Comment = string.Format("Remote Server Action {0}", operationCmb.Text);
        //    Singleton.Instance<SaveData>().AddStepEntity(entity);
        //}
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (RemoteServerAction.ActionType)Enum.Parse(typeof(RemoteServerAction.ActionType), operationCmb.Text);
            var action = new RemoteServerAction(type, new RemoteServerAction.ActionData() { Host = hostCmb.Text, Port = portTxb.Text, Value = valueTxb.Text, TargetVar = targetVarCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Remote Server Action {0}", operationCmb.Text);
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
                valueTxb.Text = selectedStepEntity.Action.Details[3];
                targetVarCmb.Text = selectedStepEntity.Action.Details[4];
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}