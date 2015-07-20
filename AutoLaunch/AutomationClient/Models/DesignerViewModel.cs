using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using AutomationClient.Models.Memento;
using AutomationCommon;
using AutomationServer;

namespace AutomationClient
{
    public class DesignerViewModel : ViewModelBase
    {
        public BreakPointHandler BreakPHandler { get; set; }

        public string AlternatingRowBackground { get; set; }
     
        public bool AutoScroll { get; set; }

        public string SelectedScriptNameGrp { get; set; }//name which appears on the group box

        public string SelectedTestNameGrp { get; set; }//name which appears on the group box

        public string SelectedSuiteNameGrp { get; set; }//name which appears on the group box

        public string SelectedVariable { get; set; }

        public string ConnectionIcon { get; set; }

        public string MessageInfo { get; set; }

        public bool? ServerConnect { get; private set; }//enable run button

        private ICommunication comProxy;//client server proxy
        private DispatcherTimer _mailBoxTimer = new DispatcherTimer();//timer for retrieving messages from the server

        public string SelectedSuiteName { get; set; }//this will be used for passing as ref name to the server on execution

        public string Cycles { get; set; }

        public string ActivatedCycles { get; private set; }//holds the activated cycles from the server

        public string ActiveStepInfo { get; private set; }//holds the info of each active step (currently only sleep action is supported)

        public string Percentage { get; private set; }//holds the tests which already passed on the server

        public string ActiveSuiteName { get; private set; }//holds the activated cycles from the server

        public string ActiveTestName { get; private set; }//holds the tests which already passed on the server

        public string SelctedScriptName { get; set; }//hold only the name of the selected script

        public string SelctedTestName { get; set; }//hold only the name of the selected Test

        public SelectedGrid SelectedGridForArrow { get; set; }

        private Enums.OnFailerAction _onFailerAction;

        public Enums.OnFailerAction OnFailerAction
        {
            get { return _onFailerAction; }
            set
            {
                if (value != Enums.OnFailerAction.non)
                    _onFailerAction = value;
            }
        }

        public DesignerViewModel()
        {
            AlternatingRowBackground = "#FFEAF0F8";
            BreakPHandler = new BreakPointHandler();
            AutoScroll = true;
            SelectedScriptNameGrp = "Script Name: ";
            SelectedTestNameGrp = "Test Name: ";
            OnFailerAction = Enums.OnFailerAction.Stop;
            SelectedSuiteName = null;

            //enable editing xaml when view model gets null referance
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                LoadSettings();

            Cycles = "1";
            Singleton.Instance<SaveData>().FileScanner = new FileScanner();
            ServerConnect = null;//init value for disable run button at startup before connect to server
            _mailBoxTimer.Tick += MailBoxTimer_Tick;
            _mailBoxTimer.Interval = new TimeSpan(0, 0, 1);
            _mailBoxTimer.Start();
        }

        #region FontSettings

        public int FontSize
        {
            get { return AutoApp.Settings.FontSize; }
            set
            {
                AutoApp.Settings.FontSize = value;
                AutoApp.Settings.SetConfigSetting("FontSize", value.ToString(), "AutoLaunch.config");
                OnPropertyChanged("FontSize");
            }
        }

        public string FontFamily
        {
            get { return AutoApp.Settings.FontFamily; }
            set
            {
                AutoApp.Settings.FontFamily = value;
                AutoApp.Settings.SetConfigSetting("FontFamily", value.ToString(), "AutoLaunch.config");
                OnPropertyChanged("FontFamily");
            }
        }

        #endregion FontSettings

        #region Controls DependencyProperty

        private void ScrollToLastItem()
        {
            try
            {
                //dataGrid.Items.Refresh();
                CollectionViewSource.GetDefaultView(ClientMessages).Refresh();

                if (dataGrid.Items.Count > 0)
                {
                    var border = VisualTreeHelper.GetChild(dataGrid, 0) as Decorator;
                    if (border != null)
                    {
                        var scroll = border.Child as ScrollViewer;
                        if (scroll != null) scroll.ScrollToEnd();
                    }
                }
            }
            catch { }
        }

        // DataGrid Instance in the viewModel
        private static DataGrid dataGrid = null;

        // DataGrid Property changed event
        public static void OnDataGridChanged
        (DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            dataGrid = obj as DataGrid;
        }

        public static readonly DependencyProperty DataGridProperty = DependencyProperty.RegisterAttached("DataGrid", typeof(DataGrid),
        typeof(DesignerViewModel), new FrameworkPropertyMetadata(OnDataGridChanged));

        public static void SetDataGrid(DependencyObject element, DataGrid value)
        {
            element.SetValue(DataGridProperty, value);
        }

        public static DataGrid GetDataGrid(DependencyObject element)
        {
            return (DataGrid)element.GetValue(DataGridProperty);
        }

        #endregion Controls DependencyProperty

        private void MailBoxTimer_Tick(object sender, EventArgs e)
        {
            _mailBoxTimer.Stop();
            try
            {
                var msgs = comProxy.GetMailBox();
                AutoApp.Logger.WriteFatalLog("ClientMessage bufferSize = " + msgs.Count.ToString());
                if (msgs.Count > 0)
                {
                    foreach (ClientMessage c in msgs)
                    {
                        Singleton.Instance<SaveData>().ClientMessages.Add(c);
                    }
                    OnPropertyChanged("ClientMessages");
                    var suiteProgressInfo = comProxy.GetSuiteProgressInfo();

                    ActivatedCycles = suiteProgressInfo.PassedCycles;
                    Percentage = string.Format("{0}%", suiteProgressInfo.SuiteProgressPersantage);
                    ActiveTestName = suiteProgressInfo.ActiveTestName;
                    ActiveSuiteName = suiteProgressInfo.SuiteName;

                    OnPropertyChanged("Percentage");
                    OnPropertyChanged("ActivatedCycles");
                    OnPropertyChanged("ActiveTestName");
                    OnPropertyChanged("ActiveSuiteName");
                    ConnectionIcon = "/Images/passPic.png";
                    OnPropertyChanged("ConnectionIcon");

                    if (AutoScroll)
                        ScrollToLastItem();
                }

                if (comProxy.IsActive())
                {
                    ActiveStepInfo = comProxy.GetActiveStepInfo();
                    OnPropertyChanged("ActiveStepInfo");
                }
            }
            catch
            {
                if (comProxy != null)
                {
                    MessageInfo = "Automation server disconnected at " + DateTime.Now;
                    ConnectionIcon = "/Images/failPic.png";
                    OnPropertyChanged("MessageInfo");
                    OnPropertyChanged("ConnectionIcon");
                    comProxy = null;
                }
            }
            _mailBoxTimer.Start();
        }

        public List<string> VariableList
        {
            get
            {
                if (comProxy != null)
                    return comProxy.GetVariablesNames();
                else
                    return null;
            }
        }

        public string GetVaribaleData(string varName)
        {
            return comProxy.GetVariableData(varName);
        }

        public ObservableCollection<TestSuiteEntity> SuiteEntitiesList
        {
            get { return Singleton.Instance<SaveData>().TestSuite.Entities; }
        }

        public ObservableCollection<TestEntity> TestEntitiesList
        {
            get { return Singleton.Instance<SaveData>().Test.Entities; }
        }

        public ObservableCollection<StepEntity> StepEntitiesList
        {
            get { return Singleton.Instance<SaveData>().Script.Entities; }
            set { Singleton.Instance<SaveData>().Script.Entities = value; }
        }

        public ObservableCollection<StepEntity> CopyStepEntities
        {
            get { return Singleton.Instance<SaveData>().CopyStepEntities.Entities; }
        }

        public ObservableCollection<TestEntity> CopyTestEntities
        {
            get
            {
                return Singleton.Instance<SaveData>().CopyTestEntities.Entities;
            }
        }

        public string SuiteDescription
        {
            get { return Singleton.Instance<SaveData>().TestSuite.Description; }
            set { Singleton.Instance<SaveData>().TestSuite.Description = value; }
        }

        public string SuiteTearDownScript
        {
            get { return Singleton.Instance<SaveData>().TestSuite.TearDownScript; }
            set { Singleton.Instance<SaveData>().TestSuite.TearDownScript = value; }
        }

        public string ScriptDescription
        {
            get { return Singleton.Instance<SaveData>().Script.Description; }
            set { Singleton.Instance<SaveData>().Script.Description = value; }
        }

        public string TestDescription
        {
            get { return Singleton.Instance<SaveData>().Test.Description; }
            set { Singleton.Instance<SaveData>().Test.Description = value; }
        }

        public StepEntity SelectedStepEntity
        {
            get { return Singleton.Instance<SaveData>().SelectedStepEntity; }
            set
            {
                Singleton.Instance<SaveData>().SelectedStepEntityIndex =
                    Singleton.Instance<SaveData>().Script.Entities.IndexOf(value);
                Singleton.Instance<SaveData>().IsSelectedStepEntity = true;
                Singleton.Instance<SaveData>().SelectedStepEntity = value;
                OnPropertyChanged("SelectedStepEntity");
            }
        }

        public int SelectedTestEntitiesIndex { get; set; }

        //display the steps of the selected script
        public TestEntity SelectedTestEntity
        {
            get { return Singleton.Instance<SaveData>().SelectedTestEntity; }
            set
            {
                if (value != null)
                {
                    Singleton.Instance<SaveData>().SelectedTestEntity = value;
                    Singleton.Instance<SaveData>().Script.Entities.Clear();
                    LoadNodeData(value.FullName);
                }
            }
        }

        //display the scripts of the selected testGroup
        public TestSuiteEntity SelectedSuiteEntity
        {
            get { return Singleton.Instance<SaveData>().SelectedTestSuiteEntity; }
            set
            {
                if (value != null)
                {
                    Singleton.Instance<SaveData>().Script.Entities.Clear();
                    Singleton.Instance<SaveData>().Test.Entities.Clear();
                    Singleton.Instance<SaveData>().SelectedTestSuiteEntity = value;
                    LoadNodeData(value.FullName);
                }
            }
        }

        public ObservableCollection<AutomationCommon.ClientMessage> ClientMessages
        {
            get { return Singleton.Instance<SaveData>().ClientMessages; }
        }

        private void LoadSettings()
        {
            HelperClass.CreateFolders();
        }

        public void AddTearDown(string scriptName)
        {
            Singleton.Instance<DesignerViewModel>().SuiteTearDownScript = scriptName;
            OnPropertyChanged("SuiteTearDownScript");
        }

        #region Commands for opening ToolBox Object

        private ICommand _selectObject;

        public ICommand SelectToolBoxItem
        {
            get
            {
                if (_selectObject == null)
                    _selectObject = new RelayCommand<string>(FactoryFormObject);
                return _selectObject;
            }
        }

        private void FactoryFormObject(string type)
        {
            Singleton.Instance<SaveData>().SelectedStepEntity = null;
            Singleton.Instance<SaveData>().IsSelectedStepEntity = false;
            Enums.ActionTypeId id = (Enums.ActionTypeId)Enum.Parse(typeof(Enums.ActionTypeId), type);
            AutomationClient.ModuleHelper.WindowFactory(id).ShowDialog();
        }

        #endregion Commands for opening ToolBox Object

        #region tool bar buttons & Menus

        private ICommand _toolBarAction;

        public ICommand SetToolBarAction
        {
            get
            {
                if (_toolBarAction == null)
                    _toolBarAction = new RelayCommand<string>(ToolBarActions);
                return _toolBarAction;
            }
        }

        public void ToolBarActions(string command)
        {
            int selectedIndex = 0;//index for move up/down arrows
            switch (command)
            {
                case "ClearAllEntities":
                    if (HelperClass.ShowQuestionMessageBox("Confirmation", "Are you sure you want to clear all elements ?"))
                    {
                        StepEntitiesList.Clear();
                        TestEntitiesList.Clear();
                        SuiteEntitiesList.Clear();
                        Percentage = string.Empty;
                        ClientMessages.Clear();
                        SelectedSuiteName = null;
                        ScriptDescription = string.Empty;
                        TestDescription = string.Empty;
                        SuiteDescription = string.Empty;

                        SelctedTestName = string.Empty;
                        SelectedTestNameGrp = "Test Name: ";
                        SelectedSuiteNameGrp = "Suite Name: ";

                        SelctedScriptName = string.Empty;
                        SelectedScriptNameGrp = "Script Name: ";
                        OnPropertyChanged("SelectedScriptNameGrp");

                        OnPropertyChanged("ScriptDescription");
                        OnPropertyChanged("TestDescription");
                        OnPropertyChanged("SelectedSuiteName");

                        OnPropertyChanged("SelectedTestNameGrp");
                        OnPropertyChanged("SelectedSuiteNameGrp");

                        OnPropertyChanged("SuiteDescription");

                        BreakPHandler.BreakPointObjList = new List<BreakPointObj>();

                        SuiteTearDownScript = string.Empty;
                        OnPropertyChanged("SuiteTearDownScript");
                    }
                    break;

                case "delStep":
                    if (SelectedGridForArrow == DesignerViewModel.SelectedGrid.Script)
                    {
                        StepEntitiesList.Remove(SelectedStepEntity);
                        Singleton.Instance<SaveData>().UpdateAutoSave();
                    }
                    else if (SelectedGridForArrow == DesignerViewModel.SelectedGrid.Test)
                        TestEntitiesList.Remove(SelectedTestEntity);
                    break;

                case "OpenScript":
                    Singleton.Instance<SaveData>().Script = FileHandler.ExtructScriptFromFile(HelperClass.OpenFileDialog());
                    OnPropertyChanged("StepEntitiesList");
                    break;

                case "SaveScript":
                    HelperClass.SaveToFile(Singleton.Instance<SaveData>().Script);
                    Singleton.Instance<SaveData>().FileScanner = new FileScanner();
                    OnPropertyChanged("ScannerScriptFiles");
                    break;

                case "SaveTest":
                    HelperClass.SaveToFile(Singleton.Instance<SaveData>().Test);
                    Singleton.Instance<SaveData>().FileScanner = new FileScanner();
                    OnPropertyChanged("ScannerTestFiles");
                    break;

                case "SaveSuite":
                    HelperClass.SaveToFile(Singleton.Instance<SaveData>().TestSuite);
                    Singleton.Instance<SaveData>().FileScanner = new FileScanner();
                    OnPropertyChanged("ScannerSuitesFiles");
                    break;

                case "MoveElementUp":
                case "MoveElementDown":
                    bool moveDirection = command == "MoveElementUp" ? true : false;
                    if (SelectedGridForArrow == SelectedGrid.Script)
                    {
                        selectedIndex = StepEntitiesList.IndexOf(SelectedStepEntity);
                        HelperClass.SwopObservableCollectionElements(StepEntitiesList, ref selectedIndex, moveDirection);
                        SelectedStepEntity = selectedIndex != -1 ? StepEntitiesList[selectedIndex] : null;
                        Singleton.Instance<SaveData>().UpdateAutoSave();
                    }
                    else if (SelectedGridForArrow == SelectedGrid.Test)
                    {
                        selectedIndex = TestEntitiesList.IndexOf(SelectedTestEntity);
                        HelperClass.SwopObservableCollectionElements(TestEntitiesList, ref selectedIndex, moveDirection);
                        SelectedTestEntity = selectedIndex != -1 ? TestEntitiesList[selectedIndex] : null;
                    }
                    else if (SelectedGridForArrow == SelectedGrid.Suite)
                    {
                        selectedIndex = SuiteEntitiesList.IndexOf(SelectedSuiteEntity);
                        HelperClass.SwopObservableCollectionElements(SuiteEntitiesList, ref selectedIndex, moveDirection);
                        SelectedSuiteEntity = selectedIndex != -1 ? SuiteEntitiesList[selectedIndex] : null;
                    }
                    break;

                case "DuplicateStepEntity":
                    StepEntitiesList.Add(new StepEntity(SelectedStepEntity));
                    Singleton.Instance<SaveData>().UpdateAutoSave();
                    OnPropertyChanged("StepEntitiesList");
                    break;

                case "pasteStep":
                    if (SelectedGridForArrow == DesignerViewModel.SelectedGrid.Script)
                    {
                        for (int i = 0; i < CopyStepEntities.Count; i++)
                            StepEntitiesList.Add(new StepEntity(CopyStepEntities[i]));
                        OnPropertyChanged("StepEntitiesList");
                    }
                    if (SelectedGridForArrow == DesignerViewModel.SelectedGrid.Test)
                    {
                        for (int i = 0; i < CopyTestEntities.Count; i++)
                        {
                            if (SelectedTestEntitiesIndex < 0)
                                TestEntitiesList.Add(new TestEntity(CopyTestEntities[i]));
                            else
                            {
                                TestEntitiesList.Insert((i + 1) + SelectedTestEntitiesIndex, new TestEntity(CopyTestEntities[i]));
                            }
                        }

                        OnPropertyChanged("TestEntitiesList");
                    }
                    break;

                case "EditStep":
                    if (SelectedStepEntity != null)
                    {
                        AutomationClient.ModuleHelper.WindowFactory(AutomationCommon.Enums.ActionTypeId.Non).ShowDialog();
                        OnPropertyChanged("StepEntitiesList");
                        Singleton.Instance<SaveData>().UpdateAutoSave();
                    }
                    break;

                case "ServerConnect":
                    try
                    {
                        //172.16.7.184
                        string uri = @"http://localhost:" + AutoApp.Settings.ClientServerPort.ToString() + @"/ServerCommunication";//"http://localhost:2090/ServerCommunication"
                        var wSHttpBinding = new WSHttpBinding();
                        wSHttpBinding.MaxReceivedMessageSize = 2147483647;
                        wSHttpBinding.MaxBufferPoolSize = 2147483647;

                        wSHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
                        wSHttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
                        wSHttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
                        wSHttpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

                        comProxy = ChannelFactory<ICommunication>.CreateChannel(wSHttpBinding, new EndpointAddress(uri));
                        if (Assembly.GetExecutingAssembly().GetName().Version.ToString() != comProxy.GetVersion())
                        {
                            HelperClass.ShowErrorMessage("Client/Server version mismatch");
                            return;
                        }

                        ServerConnect = comProxy.IsConnected();
                        OnPropertyChanged("ServerConnect");
                        ConnectionIcon = "/Images/passPic.png";
                        MessageInfo = "";
                        OnPropertyChanged("MessageInfo");
                        OnPropertyChanged("ConnectionIcon");
                    }
                    catch (Exception ex)
                    {
                        HelperClass.ShowErrorMessage("Connection to server failed , please verify that the server is active" + System.Environment.NewLine + System.Environment.NewLine + ex.Message);
                        ConnectionIcon = "/Images/failPic.png";
                    }
                    break;

                case "ServerLaunch":
                    Process[] processlist = Process.GetProcesses();

                    foreach (Process proc in processlist)
                        if (proc.ProcessName == "AutomationServer")
                        {
                            HelperClass.ShowMessageBox("Info", "Automation Server already active");
                            return;
                        }
                    ProcessStartInfo p = new ProcessStartInfo();
                    p.FileName = "AutomationServer.exe";
                    p.WorkingDirectory = AutoApp.Settings.ApplicationDirectory;
                    Process.Start(p);

                    //Process.Start(new ProcessStartInfo(AutoApp.Settings.ApplicationDirectory+"\\AutomationServer.exe"));
                    break;

                case "ExecuteTest":
                    try
                    {
                        if (string.IsNullOrEmpty(SelectedSuiteName))
                        {
                            HelperClass.ShowErrorMessage("Suite must be saved before execution");
                            return;
                        }

                        if (comProxy.IsActive())
                            HelperClass.ShowErrorMessage("Server is currently running another suite");
                        else
                        {
                            ClientMessages.Clear();
                            comProxy.SetBreakPointList(BreakPHandler.BreakPointObjList, BreakPHandler.BreakPointEnable);
                            comProxy.ExecuteSuite(SelectedSuiteName, int.Parse(Cycles), OnFailerAction);
                        }
                    }
                    catch (Exception ex)
                    {
                        HelperClass.ShowErrorMessage(ex.Message);
                    }
                    break;

                case "OpenAppConfig":
                    var sw = new SettingsWindow();
                    sw.ShowDialog();
                    break;

                case "PauseExecution":

                    if (comProxy != null)
                        comProxy.PauseExecution();
                    break;

                case "StopExecution":
                    if (comProxy != null)
                        comProxy.StopExecution();
                    break;

                case "GetVarList":
                    OnPropertyChanged("VariableList");
                    break;

                case "UpdateScript":
                    if (!string.IsNullOrEmpty(SelctedScriptName))
                    {
                        HelperClass.SaveToFile(Singleton.Instance<SaveData>().Script, SelctedScriptName);
                        Singleton.Instance<SaveData>().FileScanner = new FileScanner();
                        OnPropertyChanged("ScannerScriptFiles");
                        AlternatingRowBackground = "#FFF0F0F0";
                        break;
                    }
                    else
                    {
                        HelperClass.ShowErrorMessage("No script selected, Saving as new one");
                        var name= HelperClass.SaveToFile(Singleton.Instance<SaveData>().Script);
                        Singleton.Instance<SaveData>().FileScanner = new FileScanner();
                        OnPropertyChanged("ScannerScriptFiles");
                        loadnew = true;
                        LoadNodeData(name);
                    }
                        
                    break;

                case "NewScript":
                    if (HelperClass.ShowQuestionMessageBox("Confirmation", "Are you sure you want to clear the script"))
                    {
                        Singleton.Instance<SaveData>().SelectedTestEntity = null;
                        Singleton.Instance<SaveData>().Script.Entities.Clear();
                        ScriptDescription = string.Empty;
                        SelctedScriptName = string.Empty;
                        SelectedScriptNameGrp = "Script Name: ";
                        OnPropertyChanged("SelectedScriptNameGrp");
                        OnPropertyChanged("ScriptDescription");
                    }
                    break;

                case "AddToTest":
                    AddNodeToList(SelctedScriptName);
                    break;

                case "AddToSuite":
                    AddNodeToList(SelctedTestName);
                    break;

                case "UpdateTest":
                    if (!string.IsNullOrEmpty(SelctedTestName))
                    {
                        HelperClass.SaveToFile(Singleton.Instance<SaveData>().Test, SelctedTestName);
                        Singleton.Instance<SaveData>().FileScanner = new FileScanner();
                        OnPropertyChanged("ScannerTestFiles");
                    }
                    else
                    {
                        HelperClass.ShowErrorMessage("No Test selected, Saving as new one");
                        var name = HelperClass.SaveToFile(Singleton.Instance<SaveData>().Test);
                        Singleton.Instance<SaveData>().FileScanner = new FileScanner();
                        OnPropertyChanged("ScannerTestFiles");
                        LoadNodeData(name);
                    }
                     
                    break;

                case "UpdateSuite":
                    if (!string.IsNullOrEmpty(SelectedSuiteName))
                    {
                       // var tt = Singleton.Instance<SaveData>().TestSuite;
                        HelperClass.SaveToFile(Singleton.Instance<SaveData>().TestSuite, SelectedSuiteName);
                        Singleton.Instance<SaveData>().FileScanner = new FileScanner();
                        OnPropertyChanged("ScannerTestFiles");
                    }
                    else
                    {
                        HelperClass.ShowErrorMessage("No Suite selected, Saving as new one");
                       // var tt = Singleton.Instance<SaveData>().TestSuite;
                        var name = HelperClass.SaveToFile(Singleton.Instance<SaveData>().TestSuite, SelectedSuiteName);
                        Singleton.Instance<SaveData>().FileScanner = new FileScanner();
                        OnPropertyChanged("ScannerTestFiles");
                        LoadNodeData(name);
                    }
                        
                    break;

                case "RefreshFiles":
                    Singleton.Instance<SaveData>().FileScanner = new FileScanner();
                    OnPropertyChanged("ScannerScriptFiles");
                    OnPropertyChanged("ScannerTestFiles");
                    OnPropertyChanged("ScannerSuitesFiles");
                    break;

                case "InvertselectSteps":
                case "SelectAllSteps":
                    List<StepEntity> ents = new List<StepEntity>();
                    foreach (var ent in SaveData.Script.Entities)
                    {
                        if (command == "SelectAllSteps")
                            ent.Enable = true;
                        else
                            ent.Enable = !ent.Enable;
                        ents.Add(ent);
                    }

                    SaveData.Script.Entities.Clear();
                    foreach (var ent in ents)
                        SaveData.Script.Entities.Add(ent);

                    break;

                case "InvertselectScripts":
                case "SelectAllScripts":
                    List<TestEntity> scriptsEnt = new List<TestEntity>();
                    foreach (var ent in SaveData.Test.Entities)
                    {
                        if (command == "SelectAllScripts")
                            ent.Enable = true;
                        else
                            ent.Enable = !ent.Enable;
                        scriptsEnt.Add(ent);
                    }

                    SaveData.Test.Entities.Clear();
                    foreach (var ent in scriptsEnt)
                        SaveData.Test.Entities.Add(ent);

                    break;

                case "UndoScript":
                    Singleton.Instance<SaveData>().Script = Singleton.Instance<Originator>().Restore();
                    OnPropertyChanged("StepEntitiesList");

                    //StepEntitiesList = new ObservableCollection<StepEntity>();
                    //StepEntitiesList = Singleton.Instance<SaveData>().Script.Entities;
                    break;

                case "ExportSuite":
                    ExportSuite();
                    break;
            }

            OnPropertyChanged("AlternatingRowBackground");
        }

        #endregion tool bar buttons & Menus

        #region Test & Script add action

        private ICommand _nodeAddToList;

        public ICommand NodeAddToList
        {
            get
            {
                if (_nodeAddToList == null)
                    _nodeAddToList = new RelayCommand<string>(AddNodeToList);
                return _nodeAddToList;
            }
        }

        private void AddNodeToList(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;
            //add scripts to test list
            if (name.Contains(StaticFields.SCRIPT_EXTENTION)) //filter other buttons
            {
                var s = new TestEntity(FileHandler.ExtructScriptFromFile(name), name);
                s.Enable = true;
                Singleton.Instance<SaveData>().Test.Entities.Add(s);
                OnPropertyChanged("TestEntitiesList");
            }

            //add test to suite
            if (name.Contains(StaticFields.TEST_EXTENTION)) //filter other buttons
            {
                var t = new TestSuiteEntity(FileHandler.ExtructTestFromFile(name), name);
                t.Enable = true;
                Singleton.Instance<SaveData>().TestSuite.Entities.Add(t);
                //  SelectedSuiteName = null;//reseting the suite name (the user can't execute un saved suite)
                OnPropertyChanged("TestGroupList");
            }
        }

        #endregion Test & Script add action

        #region Load selected node type to Grid (script/test)

        private ICommand _nodeSelection;

        public ICommand NodeSelection
        {
            get
            {
                if (_nodeSelection == null)
                    _nodeSelection = new RelayCommand<string>(LoadNodeData);
                return _nodeSelection;
            }
        }

        static bool loadnew = false;//this will go True only when trying to update new script (which was never saved) 
        //display the file content on the data grid when selecting a node (script/test/suite)
        public void LoadNodeData(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            AlternatingRowBackground = "#FFF0F0F0";
            OnPropertyChanged("AlternatingRowBackground");
            var f = new FileInfo(name);
            if (name.Contains(StaticFields.SCRIPT_EXTENTION)) //filter other buttons
            {
                if (loadnew)
                {
                    SaveData.Script.Entities.Clear();
                    loadnew = false;
                }
                else if (SaveData.Script.Entities.Count != 0)
                {
                    var res = HelperClass.ShowQuestionMessageBoxResult("Script Step Add", "Clear current script");
                    if (res == MessageBoxResult.Yes)
                        SaveData.Script.Entities.Clear();
                    else if (res == MessageBoxResult.No)
                    {
                        SelectedScriptNameGrp = "Script Name: ";
                        OnPropertyChanged("SelectedScriptNameGrp");
                        SelctedScriptName = string.Empty;
                    }
                    else
                        return;
                }

                if (SaveData.Script.Entities.Count == 0)
                    SaveData.Script = FileHandler.ExtructScriptFromFile(name);
                else
                {
                    var c = FileHandler.ExtructScriptFromFile(name);

                    foreach (StepEntity entity in c.Entities)
                    {
                        SaveData.Script.Entities.Add(entity);
                    }
                }

                SelctedScriptName = f.FullName;
                SelectedScriptNameGrp = "Script Name: " + f.Name;
                OnPropertyChanged("SelectedScriptNameGrp");

                //automatic generate variables when click the script
                foreach (StepEntity s in SaveData.Script.Entities)
                {
                    if (s.Action.TypeId == Enums.ActionTypeId.VariablesOperations)
                    {
                        if ((s.Action.Details[0] == "Create") || (s.Action.Details[0] == "LoadVariableFile"))
                            s.Execute();
                    }
                }

                OnPropertyChanged("StepEntitiesList");
                OnPropertyChanged("ScriptDescription");
            }
            else if (name.Contains(StaticFields.TEST_EXTENTION))
            {
                SaveData.Test = FileHandler.ExtructTestFromFile(name);
                OnPropertyChanged("TestEntitiesList");
                OnPropertyChanged("TestDescription");
                SelctedTestName = f.FullName;
                SelectedTestNameGrp = "Test Name: " + new FileInfo(name).Name;
                OnPropertyChanged("SelectedTestNameGrp");
            }
            else if (name.Contains(StaticFields.SUITE_EXTENTION))
            {
                SaveData.TestSuite = FileHandler.ExtructTestSuiteFromFile(name);
                SelectedSuiteName = name;//updating the suite name
                ActiveSuiteName = new FileInfo(SelectedSuiteName).Name;

                SelectedSuiteNameGrp = "Suite Name: " + new FileInfo(name).Name;
                OnPropertyChanged("ActiveSuiteName");
                OnPropertyChanged("SuiteEntitiesList");
                OnPropertyChanged("SelectedSuiteName");
                OnPropertyChanged("SelectedSuiteNameGrp");
                OnPropertyChanged("SuiteDescription");
                OnPropertyChanged("SuiteTearDownScript");
            }
        }

        private void ExportSuite()
        {
            if (string.IsNullOrEmpty(SelectedSuiteName))
            {
                HelperClass.ShowMessageBox("Export Suite", "Please select suite to export");
                return;
            }
            List<string> fileList = new List<string>();
            fileList.Add(SelectedSuiteName);
            var tsu = FileHandler.ExtructTestSuiteFromFile(SelectedSuiteName);
            foreach (var entity in tsu.Entities)
            {
                fileList.Add(entity.FullName);
                var tests = FileHandler.ExtructTestFromFile(entity.FullName);
                foreach (var script in tests.Entities)
                {
                    fileList.Add(script.FullName);
                    foreach (var step in script.Script.Entities)
                    {
                        if (step.Action.TypeId == Enums.ActionTypeId.ScriptExecute)//adding scripts inside Scripts
                        {
                            if (new FileInfo(step.Action.Details[1]).Exists)
                                fileList.Add(step.Action.Details[1]);
                        }
                    }
                }
            }

            HelperClass.ExportSuite(fileList);
        }

        #endregion Load selected node type to Grid (script/test)

        #region tree view properties (file lists)

        public List<Folder> ScannerScriptFiles
        {
            get { return SaveData.FileScanner.ScriptList; }
        }

        public List<Folder> ScannerTestFiles
        {
            get { return SaveData.FileScanner.TestList; }
        }

        public List<Folder> ScannerSuitesFiles
        {
            get { return SaveData.FileScanner.SuiteList; }
        }

        #endregion tree view properties (file lists)

        public enum SelectedGrid
        {
            Script, Test, Suite
        }
    }
}