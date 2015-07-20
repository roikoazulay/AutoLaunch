using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class DictionaryActionView : Window, IAutomationWindow
    {
        public DictionaryActionView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var id = (DictionaryAction.ActionType)Enum.Parse(typeof(DictionaryAction.ActionType), operationCmb.Text);
            var action = new DictionaryAction(id, new DictionaryAction.ActionData() { Name = dictionaryName.Text, Key = keyCmb.Text, Value = valueCmb.Text, Target = targetVarCmb.Text, FileName = fileNameCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Dictionary Action - {0}", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                dictionaryName.Text = selectedStepEntity.Action.Details[1];
                keyCmb.Text = selectedStepEntity.Action.Details[2];
                valueCmb.Text = selectedStepEntity.Action.Details[3];
                targetVarCmb.Text = selectedStepEntity.Action.Details[4];
                fileNameCmb.Text = selectedStepEntity.Action.Details[5];
            }
        }

        private void operationCmb_DropDownClosed(object sender, EventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}