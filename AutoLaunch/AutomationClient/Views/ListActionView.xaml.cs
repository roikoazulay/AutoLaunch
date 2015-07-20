using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class ListActionView : Window, IAutomationWindow
    {
        public ListActionView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var id = (ListAction.ActionType)Enum.Parse(typeof(ListAction.ActionType), operationCmb.Text);
            var action = new ListAction(id, new ListAction.ActionData() { ListName = listNameCmb.Text, Value = valueCmb.Text, Index = indexCmb.Text, Target = targetVarCmb.Text, FileName = fileNameCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("List Action - {0}", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                listNameCmb.Text = selectedStepEntity.Action.Details[1];
                valueCmb.Text = selectedStepEntity.Action.Details[2];
                indexCmb.Text = selectedStepEntity.Action.Details[3];
                targetVarCmb.Text = selectedStepEntity.Action.Details[4];
                fileNameCmb.Text = selectedStepEntity.Action.Details[5];
            }
        }

        private void operationCmb_DropDownClosed(object sender, EventArgs e)
        {
            if (operationCmb.Text == "Exists")
                MessageBox.Show("verify the all strings in list exist in some data string (opposite of contains)");
        }
    }
}