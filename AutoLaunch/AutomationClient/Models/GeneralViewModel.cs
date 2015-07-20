using System;
using System.Collections.ObjectModel;
using AutomationCommon;
using AutomationServer;
using AutomationServer.Actions;
using AutomationServer.Actions.RS232;

namespace AutomationClient
{
    public sealed class GeneralViewModel : ViewModelBase
    {
        public ObservableCollection<string> VariableList { get; private set; }

        public ObservableCollection<string> VariableOperations { get; private set; }

        public ObservableCollection<string> TextOperations { get; private set; }

        public ObservableCollection<string> LableOperations { get; private set; }

        public ObservableCollection<string> Rs232Operations { get; private set; }

        public ObservableCollection<string> TimerOperations { get; private set; }

        public ObservableCollection<string> SshOperations { get; private set; }

        public ObservableCollection<string> TelnetOperations { get; private set; }

        public ObservableCollection<string> SftpOperations { get; private set; }

        public ObservableCollection<string> DateTimeOperations { get; private set; }

        public ObservableCollection<string> TableOperations { get; private set; }

        public ObservableCollection<string> AutoItGuiOperations { get; private set; }

        public ObservableCollection<string> WayneOperations { get; private set; }

        public ObservableCollection<string> HR80Operations { get; private set; }

        public ObservableCollection<string> MotorOperations { get; private set; }

        public ObservableCollection<string> FileOperations { get; private set; }

        public ObservableCollection<string> RelayControlOperations { get; private set; }

        public ObservableCollection<string> SystemOperations { get; set; }

        public ObservableCollection<string> ListOperations { get; set; }

        public ObservableCollection<string> DictionaryOperations { get; set; }

        public ObservableCollection<string> ServerComOperations { get; set; }

        public GeneralViewModel()
        {
            FileOperations = new ObservableCollection<string>();
            MotorOperations = new ObservableCollection<string>();
            VariableList = new ObservableCollection<string>();
            VariableOperations = new ObservableCollection<string>();
            TextOperations = new ObservableCollection<string>();
            LableOperations = new ObservableCollection<string>();
            Rs232Operations = new ObservableCollection<string>();
            TimerOperations = new ObservableCollection<string>();
            SshOperations = new ObservableCollection<string>();
            TelnetOperations = new ObservableCollection<string>();
            SftpOperations = new ObservableCollection<string>();
            DateTimeOperations = new ObservableCollection<string>();
            TableOperations = new ObservableCollection<string>();
            AutoItGuiOperations = new ObservableCollection<string>();
            WayneOperations = new ObservableCollection<string>();
            HR80Operations = new ObservableCollection<string>();
            RelayControlOperations = new ObservableCollection<string>();
            SystemOperations = new ObservableCollection<string>();
            ListOperations = new ObservableCollection<string>();
            DictionaryOperations = new ObservableCollection<string>();
            ServerComOperations = new ObservableCollection<string>();

            foreach (string variable in Singleton.Instance<SavedData>().Variables.Keys)
                VariableList.Add(variable);

            var listOfValues = Enum.GetValues(typeof(VariablesAction.VariablesActionType));
            foreach (var value in listOfValues)
                VariableOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(TextManipulationAction.TextActionType));
            foreach (var value in listOfValues)
                TextOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(LabelAction.ActionType));
            foreach (var value in listOfValues)
                LableOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(Rs232Action.Rs232ActionType));
            foreach (var value in listOfValues)
                Rs232Operations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(TimersAction.TimersActionType));
            foreach (var value in listOfValues)
                TimerOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(SshAction.SshActionType));
            foreach (var value in listOfValues)
                SshOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(TelentAction.TelentActionType));
            foreach (var value in listOfValues)
                TelnetOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(SftpAction.SftpActionType));
            foreach (var value in listOfValues)
                SftpOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(DateTimeAction.ActionType));
            foreach (var value in listOfValues)
                DateTimeOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(TableAction.ActionType));
            foreach (var value in listOfValues)
                TableOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(AutoItUiAction.ActionType));
            foreach (var value in listOfValues)
                AutoItGuiOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(WaynPumpAction.ActionType));
            foreach (var value in listOfValues)
                WayneOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(Horison80PowerSupplyAction.ActionType));
            foreach (var value in listOfValues)
                HR80Operations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(MotorControllerAction.ActionType));
            foreach (var value in listOfValues)
                MotorOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(FileAction.ActionType));
            foreach (var value in listOfValues)
                FileOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(RelayControlAction.ActionType));
            foreach (var value in listOfValues)
                RelayControlOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(SystemAction.ActionType));
            foreach (var value in listOfValues)
                SystemOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(ListAction.ActionType));
            foreach (var value in listOfValues)
                ListOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(DictionaryAction.ActionType));
            foreach (var value in listOfValues)
                DictionaryOperations.Add(value.ToString());

            listOfValues = Enum.GetValues(typeof(RemoteServerAction.ActionType));
            foreach (var value in listOfValues)
                ServerComOperations.Add(value.ToString());
        }
    }
}