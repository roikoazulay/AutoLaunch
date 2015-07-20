using System.Windows;
using AutomationCommon;
using AutomationServer;

namespace AutomationClient.Views
{
    public partial class ConditionView : Window, IAutomationWindow
    {
        public ConditionView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var conditionAction = new ConditionAction(new ConditionActionData { SourceVar = varCmb.Text, Lable = lableCmb.Text, Result = conditionCmb.Text });
            var entity = new StepEntity(conditionAction);
            entity.Comment = string.Format("If variable {0} is {1} then GoTo lable {2}", varCmb.Text, conditionCmb.Text, lableCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                varCmb.Text = selectedStepEntity.Action.Details[0];
                conditionCmb.Text = selectedStepEntity.Action.Details[1];
                lableCmb.Text = selectedStepEntity.Action.Details[2];
            }
        }
    }
}