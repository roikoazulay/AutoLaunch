using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class SqlitView : Window, IAutomationWindow
    {
        public SqlitView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (SqliteAction.ActionType)Enum.Parse(typeof(SqliteAction.ActionType), operationCmb.Text);
            var action = new SqliteAction(type, new SqliteAction.ActionData() { DbName = dbNameTxb.Text, Options = optionsTxb.Text, Query = queryTxb.Text, TargetVar = varCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Sqlite {0}", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                dbNameTxb.Text = selectedStepEntity.Action.Details[1];
                optionsTxb.Text = selectedStepEntity.Action.Details[2];
                queryTxb.Text = selectedStepEntity.Action.Details[3];
                varCmb.Text = selectedStepEntity.Action.Details[4];
            }
        }
    }
}