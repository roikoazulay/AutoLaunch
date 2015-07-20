using System.Windows;
using AutomationCommon;
using AutomationServer;

namespace AutomationClient.Views
{
    /// <summary>
    /// Interaction logic for SleepView.xaml
    /// </summary>
    public partial class SleepView : Window, IAutomationWindow
    {
        public SleepView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var entity = new StepEntity(new SleepAction(delayCmb.Text));
            entity.Comment = "Sleep for " + "{" + delayCmb.Text + "} Sec";
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                delayCmb.Text = selectedStepEntity.Action.Details[0];
            }
        }
    }
}