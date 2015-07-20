using System;
using System.Windows;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions.RS232;

namespace AutomationClient.Views
{
    public partial class Rs232View : Window, IAutomationWindow
    {
        public Rs232View()
        {
            InitializeComponent();
            Construct();
            DataContext = new GeneralViewModel();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            var id = (Rs232Action.Rs232ActionType)Enum.Parse(typeof(Rs232Action.Rs232ActionType), operationCmb.Text);
            var data = new Rs232Action.Rs232ActionData()
                                                   {
                                                       Port = portCmb.Text,
                                                       BaudRate = baudRateCmb.Text,
                                                       Command = commandTxb.Text,
                                                       DataBits = dataBitsCmb.Text,
                                                       Dtr = dtrCmb.Text,
                                                       Handshake = HandshakeCmb.Text,
                                                       Parity = parityCmb.Text,
                                                       Rts = rtsCmb.Text,
                                                       StopBits = stopBitsCmb.Text,
                                                       TargetVar = targetVarCmb.Text,
                                                       LogFileName = logCmb.Text,
                                                       AsBytes = asBytesCmb.Text,
                                                       SendLineFeed = lineFeedCmb.Text,
                                                       SendSingleChar = singleCharCmb.Text
                                                   };

            var rsObj = new Rs232Action(id, data);
            var entity = new StepEntity(rsObj);
            entity.Comment = string.Format("Rs232 Action - {0}", operationCmb.Text);
            Singleton.Instance<SaveData>().AddStepEntity(entity);
        }

        public void Construct()
        {
            var selectedStepEntity = Singleton.Instance<SaveData>().SelectedStepEntity;
            if (selectedStepEntity != null)
            {
                operationCmb.Text = selectedStepEntity.Action.Details[0];
                portCmb.Text = selectedStepEntity.Action.Details[1];
                HandshakeCmb.Text = selectedStepEntity.Action.Details[2];
                rtsCmb.Text = selectedStepEntity.Action.Details[3];
                dtrCmb.Text = selectedStepEntity.Action.Details[4];
                parityCmb.Text = selectedStepEntity.Action.Details[5];
                stopBitsCmb.Text = selectedStepEntity.Action.Details[6];
                dataBitsCmb.Text = selectedStepEntity.Action.Details[7];
                baudRateCmb.Text = selectedStepEntity.Action.Details[8];
                targetVarCmb.Text = selectedStepEntity.Action.Details[9];
                commandTxb.Text = selectedStepEntity.Action.Details[10];
                logCmb.Text = selectedStepEntity.Action.Details.Count > 11 ? selectedStepEntity.Action.Details[11] : string.Empty;
                asBytesCmb.Text = selectedStepEntity.Action.Details.Count > 12 ? selectedStepEntity.Action.Details[12] : false.ToString();
                lineFeedCmb.Text = selectedStepEntity.Action.Details.Count > 13 ? selectedStepEntity.Action.Details[13] : false.ToString();
                singleCharCmb.Text = selectedStepEntity.Action.Details.Count > 14 ? selectedStepEntity.Action.Details[14] : false.ToString();
            }
        }

        private void portCmb_DropDownClosed(object sender, EventArgs e)
        {
            logCmb.Text = @"C:\AutoLaunch\Bin\Logs\LOG_" + portCmb.Text + ".log";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void asBytesCmb_DropDownClosed(object sender, EventArgs e)
        {
            if (asBytesCmb.Text == "True")
                commandTxb.Text = "FF,12,33";
            else
                commandTxb.Text = "";
        }

        private void operationCmb_DropDownClosed(object sender, EventArgs e)
        {
            UpdateGui();
        }

        private void UpdateGui()
        {
            HelperClass.ChangeControlState(this, false);

            operationCmb.IsEnabled = true;
            portCmb.IsEnabled = true;
            switch (operationCmb.Text)
            {
                case "Connect":
                    HandshakeCmb.IsEnabled = true;
                    rtsCmb.IsEnabled = true;
                    dtrCmb.IsEnabled = true;
                    logCmb.IsEnabled = true;
                    logCmb.IsReadOnly = false;
                    parityCmb.IsEnabled = true;
                    stopBitsCmb.IsEnabled = true;
                    baudRateCmb.IsEnabled = true;
                    dataBitsCmb.IsEnabled = true;
                    break;

                case "Disconnect":
                    portCmb.IsEnabled = true;
                    break;

                case "SendCommand":
                    commandTxb.IsEnabled = true;
                    asBytesCmb.IsEnabled = true;
                    lineFeedCmb.IsEnabled = true;
                    singleCharCmb.IsEnabled = true;
                    break;
            }
        }
    }
}