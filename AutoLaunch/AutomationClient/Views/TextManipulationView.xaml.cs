using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;

namespace AutomationClient.Views
{
    public partial class TextManipulationView : Window, IAutomationWindow
    {
        public TextManipulationView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(targetCmb.Text))
                HelperClass.ShowMessageBox("Info", "Target Variable is empty");

            var type = (TextManipulationAction.TextActionType)Enum.Parse(typeof(TextManipulationAction.TextActionType), operationCmb.Text);
            var data = new TextActionData() { SourceVar = srcCmb.Text, TargetVar = targetCmb.Text, Value = valueTxb.Text, Length = lengthTxb.Text };
            var textMaipulationAction = new TextManipulationAction(type, data);
            var entity = new StepEntity(textMaipulationAction);
            entity.Comment = string.Format("Text Operations {0}", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                srcCmb.Text = selectedStepEntity.Action.Details[1];
                targetCmb.Text = selectedStepEntity.Action.Details[2];
                valueTxb.Text = selectedStepEntity.Action.Details[3];
                if (selectedStepEntity.Action.Details.Count > 4)
                    lengthTxb.Text = selectedStepEntity.Action.Details[4];
            }
            ReleaseControls();
        }

        private void operationCmb_DropDownClosed(object sender, EventArgs e)
        {
            //lengthTxb.IsEnabled = operationCmb.Text == "Substring" ? true : false;
            //if (operationCmb.Text == "Replace")
            //    valueTxb.Text = "{Old String}~{New String}";
            var type = (TextManipulationAction.TextActionType)Enum.Parse(typeof(TextManipulationAction.TextActionType), operationCmb.Text);
            switch (type)
            {
                case TextManipulationAction.TextActionType.Replace:
                    valueTxb.Text = "{Old String}~{New String}";
                    break;
            }

            ReleaseControls();
        }

        private void ReleaseControls()
        {
            targetCmb.IsEnabled = true;
            lengthTxb.IsEnabled = false;
            var actionType = (TextManipulationAction.TextActionType)Enum.Parse(typeof(TextManipulationAction.TextActionType), operationCmb.Text);
            switch (actionType)
            {
                case TextManipulationAction.TextActionType.SubstringByIndex:
                    lengthTxb.IsEnabled = true;
                    break;

                case TextManipulationAction.TextActionType.Split:
                    MessageBox.Show("Target Variable must contain a List Variable Name");
                    break;
            }
        }
    }
}