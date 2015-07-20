using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;

namespace AutomationClient.Views
{
    public partial class LableView : Window, IAutomationWindow
    {
        public LableView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (LabelAction.ActionType)Enum.Parse(typeof(LabelAction.ActionType), operationCmb.Text);
            var action = new LabelAction(type, new LabelAction.ActionData() { LableName = lableCmb.Text, Loops = loopCountCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("{0} Label - {1}", operationCmb.Text, lableCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                lableCmb.Text = selectedStepEntity.Action.Details[1];
                loopCountCmb.Text = selectedStepEntity.Action.Details.Count > 2 ? selectedStepEntity.Action.Details[2] : "1";
            }

            UpdateGui(false);
        }

        private void operationCmb_DropDownClosed(object sender, EventArgs e)
        {
            UpdateGui(true);
        }

        private void UpdateGui(bool clearLoops)
        {
            if (operationCmb.Text != "Create")
            {
                loopCountCmb.Text = "";
                loopCountCmb.IsEnabled = false;
            }
            else
            {
                loopCountCmb.IsEnabled = true;
                if (clearLoops)
                    loopCountCmb.Text = "1";
            }
        }
    }
}