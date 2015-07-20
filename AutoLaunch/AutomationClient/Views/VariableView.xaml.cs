using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AutomationCommon;
using AutomationServer;

namespace AutomationClient.Views
{
    public partial class VariableView : Window, IAutomationWindow
    {
        public string description { get; set; }

        public VariableView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var id = (VariablesAction.VariablesActionType)Enum.Parse(typeof(VariablesAction.VariablesActionType), operationCmb.Text);
            IVariableData iv = new SavedData.VariableData(varCmb.Text, valueTxb.Text);
            var op = new OperationData { FileName = fileNameCmb.Text, TargetVar = targetVarCmb.Text, Value = valueTxb.Text };//we use the same valueTxb.Text field for both setting value to variable and performing operations (depands on the action)

            var variable = new VariablesAction(id, iv, op);
            var entity = new StepEntity(variable);
            entity.Comment = string.Format("Variables {0} - {1}", operationCmb.Text, varCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);

            if ((id == VariablesAction.VariablesActionType.Create) || (id == VariablesAction.VariablesActionType.LoadVariableFile))
                variable.Execute();
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                targetVarCmb.Text = selectedStepEntity.Action.Details[1];
                fileNameCmb.Text = selectedStepEntity.Action.Details[2];
                valueTxb.Text = selectedStepEntity.Action.Details[3];
                varCmb.Text = selectedStepEntity.Action.Details[4];
            }
        }

        private void varCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string var = ((object[])(e.AddedItems))[0].ToString();
                valueTxb.Text = Singleton.Instance<SavedData>().Variables.ContainsKey(var)
                               ? Singleton.Instance<SavedData>().Variables[var].GetValue()
                               : string.Empty;
            }
            catch
            {
            }
        }

        private void varCmb_TouchUp(object sender, TouchEventArgs e)
        {
            valueTxb.Text = Singleton.Instance<SavedData>().Variables.ContainsKey(varCmb.Text)
                             ? Singleton.Instance<SavedData>().Variables[varCmb.Text].GetValue()
                             : string.Empty;
        }

        private void operationCmb_DropDownClosed(object sender, EventArgs e)
        {
            targetVarCmb.Text = string.Empty;
            targetVarCmb.IsEnabled = false;
            switch (operationCmb.Text)
            {
                case "RandomNumber":
                    valueTxb.Text = "MinValue,MaxValue";
                    break;

                case "RandomDouble":
                    valueTxb.Text = "MinValue,MaxValue,precision (numbers after the decimal point)";
                    break;

                case "LoadVariableFile":
                    MessageBox.Show("The file should be CSV starting from row 2 containing variable name & value, for example:" + System.Environment.NewLine + "IP_Address,172.16.6.6", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                case "Equals":
                case "NotEqual":
                case "LessThan":
                case "GreaterThan":
                case "LessThanOrEqual":
                case "GreaterThanOrEqual":
                case "IsEmpty":
                    targetVarCmb.IsEnabled = true;
                    break;

                case "RoundDown":
                case "RoundUp":
                    valueTxb.Text = "set the number of fractional digits to round";
                    break;
            }

            UpdateToolTip();
        }

        private void UpdateToolTip()
        {
            var id = (VariablesAction.VariablesActionType)Enum.Parse(typeof(VariablesAction.VariablesActionType), operationCmb.Text);
            switch (id)
            {
                case AutomationServer.VariablesAction.VariablesActionType.Copy:
                    tipTool.Text = "If target exist then Variable Name is copied to the target variable otherwise Variable Name it copied to the variable in the value field.";
                    break;
            }
        }
    }
}