using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;

namespace AutomationClient.Views
{
    public partial class FileView : Window, IAutomationWindow
    {
        public FileView()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var type = (FileAction.ActionType)Enum.Parse(typeof(FileAction.ActionType), operationCmb.Text);
            var action = new FileAction(type, new FileAction.ActionData() { FileName = fileNameCmb.Text, TargetVar = valueCmb.Text });
            var entity = new StepEntity(action);
            entity.Comment = string.Format("File Action {0}", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                fileNameCmb.Text = selectedStepEntity.Action.Details[1];
                valueCmb.Text = selectedStepEntity.Action.Details[2];
            }
        }

        private void operationCmb_DropDownClosed(object sender, EventArgs e)
        {
           // valueCmb.IsEnabled = true;
            switch (operationCmb.Text)
            {
                case "GetFileLength":
                case "Exist":
                case "IsReadOnly":
                case "CreationTime":
                case "LastWriteTime":
                case "LastAccessTime":
                    valueLbl.Content = "Target Variable:";
                    break;

                case "Rename":
                case "Copy":
                case "Move":
                    valueLbl.Content = "New Name:";
                    break;

                default:
                    valueLbl.Content = "Value:";
                    valueCmb.IsEnabled = false;
                    break;
            }
        }
    }
}