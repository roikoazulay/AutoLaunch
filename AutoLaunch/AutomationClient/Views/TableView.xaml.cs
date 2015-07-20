using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class TableView : Window, IAutomationWindow
    {
        public TableView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (TableAction.ActionType)Enum.Parse(typeof(TableAction.ActionType), operationCmb.Text);

            var action = new TableAction(type, new TableAction.ActionData() { TableName = tableNameCmb.Text, Value = valueCmb.Text, Row = rowCmb.Text, Column = colCmb.Text, FileName = fileCmb.Text, TargetVar = targetCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Table {0} Action", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                tableNameCmb.Text = selectedStepEntity.Action.Details[1];
                valueCmb.Text = selectedStepEntity.Action.Details[2];
                rowCmb.Text = selectedStepEntity.Action.Details[3];
                colCmb.Text = selectedStepEntity.Action.Details[4];
                fileCmb.Text = selectedStepEntity.Action.Details[5];
                targetCmb.Text = selectedStepEntity.Action.Details[6];
            }
        }
    }
}