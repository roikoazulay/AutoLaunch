using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    /// <summary>
    /// Interaction logic for RunProgramView.xaml
    /// </summary>
    public partial class ScriptView : Window, IAutomationWindow
    {
        public ScriptView()
        {
            InitializeComponent();
            Construct();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            AddStep(programeTxb.Text, paramsTxb.Text);
        }

        public void AddStep(string scriptName, string param)
        {
            var data = new ScriptAction.ActionData() { Name = scriptName, Params = param };
            var type = (ScriptAction.ActionType)Enum.Parse(typeof(ScriptAction.ActionType), "Execute");
            var action = new ScriptAction(type, data);
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Script Execute {0}", scriptName);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                programeTxb.Text = selectedStepEntity.Action.Details[1];
                if (selectedStepEntity.Action.Details.Count > 2)
                    paramsTxb.Text = selectedStepEntity.Action.Details[2];
            }
        }

        private void selectedProgramBtn_Click(object sender, RoutedEventArgs e)
        {
            programeTxb.Text = HelperClass.OpenFileDialog();
        }

        private void viewBtn_Click(object sender, RoutedEventArgs e)
        {
            Singleton.Instance<DesignerViewModel>().LoadNodeData(programeTxb.Text);
        }
    }
}