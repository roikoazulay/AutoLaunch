using System;
using System.Diagnostics;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    /// <summary>
    /// Interaction logic for RunProgramView.xaml
    /// </summary>
    public partial class RunProgramView : Window, IAutomationWindow
    {
        public RunProgramView()
        {
            InitializeComponent();
            Construct();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var data = new RunProgram.ActionData() { Program = programeTxb.Text, Parms = paramTxb.Text, WaitForClose = waitForExit.IsChecked.ToString() };
            var type = (RunProgram.ActionType)Enum.Parse(typeof(RunProgram.ActionType), operationCmb.Text);
            var action = new RunProgram(type, data);
            var entity = new StepEntity(action);
            entity.Comment = string.Format("Process {0} with parameters {1} action {2}", programeTxb.Text, paramTxb.Text, operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            this.Height = 165;
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                if (selectedStepEntity.Action.Details.Count == 2)
                {
                    programeTxb.Text = selectedStepEntity.Action.Details[0];
                    paramTxb.Text = selectedStepEntity.Action.Details[1];
                }
                else
                {
                    programeTxb.Text = selectedStepEntity.Action.Details[1];
                    paramTxb.Text = selectedStepEntity.Action.Details[2];
                    waitForExit.IsChecked = bool.Parse(selectedStepEntity.Action.Details[3]);
                    operationCmb.Text = selectedStepEntity.Action.Details.Count > 4 ? selectedStepEntity.Action.Details[4] : selectedStepEntity.Action.Details[0];
                }
            }
        }

        private void selectedProgramBtn_Click(object sender, RoutedEventArgs e)
        {
            programeTxb.Text = HelperClass.OpenFileDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 250;
            Process[] processlist = Process.GetProcesses();
            string procTxt = "";
            foreach (Process proc in processlist)
                procTxt += proc.ProcessName + System.Environment.NewLine;

            processTxb.Text = procTxt;
        }
    }
}