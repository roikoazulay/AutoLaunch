using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using AutomationCommon;
using AutomationServer;

namespace AutomationClient
{
    /// <summary>
    /// Interaction logic for Designer.xaml
    /// </summary>
    public partial class Designer : Window
    {
        private DesignerViewModel model;

        public Designer()
        {
            InitializeComponent();
            model = Singleton.Instance<DesignerViewModel>();// new DesignerViewModel();
            this.DataContext = model;
            this.Title = "Auto Launcher " + "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void exitMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void moveUp_Click(object sender, RoutedEventArgs e)
        {
            try { dgStepEntities.Items.Refresh(); }
            catch { }
        }

        private void moveDw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgStepEntities.Items.Refresh();
            }
            catch { }
        }

        private void dgStepEntities_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            model.ToolBarActions("EditStep");
        }

        private void runBtn_Click(object sender, RoutedEventArgs e)
        {
            //  MainTabControl.SelectedIndex = 1;
        }

        private void dgStepEntities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.SelectedGridForArrow = DesignerViewModel.SelectedGrid.Script;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.SelectedGridForArrow = DesignerViewModel.SelectedGrid.Test;
            testDataGrid.ToolTip = Singleton.Instance<DesignerViewModel>().SelctedScriptName;
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            model.SelectedGridForArrow = DesignerViewModel.SelectedGrid.Suite;
            var cell = TestListGrid.CurrentCell;
        }

        private static void ExpandSubContainers(ItemsControl parentContainer)
        {
            foreach (Object item in parentContainer.Items)
            {
                TreeViewItem currentContainer = parentContainer.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;

                if (currentContainer != null && currentContainer.Items.Count > 0)
                {
                    // Expand the current item.
                    currentContainer.IsExpanded = true;
                    if (currentContainer.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                    {
                        // If the sub containers of current item is not ready, we need to wait until
                        // they are generated.
                        currentContainer.ItemContainerGenerator.StatusChanged += delegate
                        {
                            ExpandSubContainers(currentContainer);
                        };
                    }
                    else
                    {
                        // If the sub containers of current item is ready, we can directly go to the next
                        // iteration to expand them.
                        ExpandSubContainers(currentContainer);
                    }
                }
            }
        }

        private void aboutBtn_Click(object sender, RoutedEventArgs e)
        {
            // ExpandSubContainers(ScriptsTreeView);
            // return;

            AboutWin about = new AboutWin(this);
            about.ShowDialog();
        }

        private string lastLayout = "";

        private void SaveLayout()
        {
            //StringBuilder sb = new StringBuilder();
            //using (StringWriter sw = new StringWriter(sb))
            //{
            //    var serializer = new XmlLayoutSerializer(myDock);
            //    serializer.Serialize(sw);
            //    sw.Flush();
            //    lastLayout = sb.ToString();
            //}

            //  var serializer = new XmlLayoutSerializer(myDock);
        }

        private void RestoreLayout()
        {
            //var serializer = new XmlLayoutSerializer(myDock);
            //using (var stream = new StreamReader("c:\\avdocLayout.xml"))
            //    serializer.Deserialize(stream);
            //myDock.Layout.ActiveContent = lastLayout;
        }

        private void reportBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveLayout();
            HelperClass.ExecutePocess(HelperClass.GetLastFile(StaticFields.LOG_PATH, "htm"));
        }

        #region Datagrid Single-Click Editing

        private void DataGridCell_MouseEnter(object sender, MouseEventArgs e)
        {
            // for future options - display the step defiles on hover
            //string header = (((System.Windows.Controls.DataGridCell)(sender)).Column).Header.ToString();
            //if (header == "Type Name")
            //{
            //    var action = ((System.Windows.FrameworkElement)(((System.Windows.Controls.ContentControl)(sender)).Content)).DataContext;
            //    var tid = (((AutomationServer.StepEntity)(action)).Action).TypeId;
            //    model.SelectedStepEntity = (((AutomationServer.StepEntity)(action)));
            //    AutomationClient.ModuleHelper.WindowFactory(AutomationCommon.Enums.ActionTypeId.Non).ShowDialog();
            //}
        }

        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.DataGridCell cell = sender as System.Windows.Controls.DataGridCell;
            if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
            {
                if (!cell.IsFocused)
                {
                    cell.Focus();
                }
                System.Windows.Controls.DataGrid dataGrid = FindVisualParent<System.Windows.Controls.DataGrid>(cell);
                if (dataGrid != null)
                {
                    if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                    {
                        if (!cell.IsSelected)
                            cell.IsSelected = true;
                    }
                    else
                    {
                        DataGridRow row = FindVisualParent<DataGridRow>(cell);
                        if (row != null && !row.IsSelected)
                        {
                            row.IsSelected = true;
                        }
                    }
                }
            }
        }

        private static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }

        #endregion Datagrid Single-Click Editing

        private void CopyStep_Click(object sender, RoutedEventArgs e)
        {
            if (model.SelectedGridForArrow == DesignerViewModel.SelectedGrid.Script)
            {
                CopySteps(false, true);
                //model.CopyStepEntities.Clear();
                //if (dgStepEntities.SelectedItems.Count > 0)
                //{
                //    for (int i = 0; i < dgStepEntities.SelectedItems.Count; i++)
                //    {
                //        var entity = dgStepEntities.SelectedItems[i] as StepEntity;
                //        model.CopyStepEntities.Add(entity);
                //    }
                //}
            }
            else if (model.SelectedGridForArrow == DesignerViewModel.SelectedGrid.Test)
            {
                //model.CopyTestEntities.Clear();
                //int count = testDataGrid.SelectedItems.Count;
                //if (testDataGrid.SelectedItems.Count > 0)
                //{
                //    for (int i = 0; i < count; i++)
                //    {
                //        var entity = testDataGrid.SelectedItems[i] as TestEntity;
                //        model.CopyTestEntities.Add(entity);
                //    }

                //    for (int i = 0; i < count; i++)
                //    {
                //        Singleton.Instance<SaveData>().Test.Entities.RemoveAt(model.SelectedTestEntitiesIndex);
                //    }
                //}
                CopySteps(false, false);
                model.SelectedTestEntitiesIndex = -1;//reset index when copy
            }
        }

        private void CutStep_Click(object sender, RoutedEventArgs e)
        {
            CopySteps(true, false);
            model.SelectedTestEntitiesIndex = -1;//reset index when copy
        }

        private void CopySteps(bool cut, bool script)
        {
            if (script)
            {
                model.CopyStepEntities.Clear();
                if (dgStepEntities.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dgStepEntities.SelectedItems.Count; i++)
                    {
                        var entity = dgStepEntities.SelectedItems[i] as StepEntity;
                        model.CopyStepEntities.Add(entity);
                    }
                }
            }
            else
            {
                model.CopyTestEntities.Clear();
                int count = testDataGrid.SelectedItems.Count;
                if (testDataGrid.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var entity = testDataGrid.SelectedItems[i] as TestEntity;
                        model.CopyTestEntities.Add(entity);
                    }
                }

                if (cut)

                    for (int i = 0; i < count; i++)
                        Singleton.Instance<SaveData>().Test.Entities.Remove(testDataGrid.SelectedItems[0] as TestEntity);
                //Singleton.Instance<SaveData>().Test.Entities.RemoveAt(model.SelectedTestEntitiesIndex);
            }
        }

        private void autoScrollChk_Click(object sender, RoutedEventArgs e)
        {
            model.AutoScroll = (bool)autoScrollChk.IsChecked;
        }

        private void OdcExpander_Expanded(object sender, RoutedEventArgs e)
        {
            // ToolboxExpander.IsExpanded = false;
            //  ExecutionExpander.IsExpanded = true;
        }

        private void variablesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var data = model.GetVaribaleData(model.SelectedVariable);
            varDataTxb.Text = data;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddScriptAsStepBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Singleton.Instance<SaveData>().SelectedStepEntity = null;
            Singleton.Instance<SaveData>().IsSelectedStepEntity = false;
            string path = ((System.Windows.Controls.HeaderedItemsControl)(sender)).Tag.ToString();
            AutomationClient.Views.ScriptView sv = new Views.ScriptView();
            sv.AddStep(path, string.Empty);
            sv.Close();
        }

        private void ScriptsTreeView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //bubbling the tree scroll
            PreviewMouseWheel(sender, e);
        }

        private static void PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        private void TestListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.SelectedGridForArrow = DesignerViewModel.SelectedGrid.Suite;
            TestListGrid.ToolTip = Singleton.Instance<DesignerViewModel>().SelctedTestName;
        }

        private void deleteStep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgStepEntities.Items.Refresh();
                testDataGrid.Items.Refresh();
                TestListGrid.Items.Refresh();
            }
            catch { }
        }

        #region Break Point Methods

        private void dgStepEntities_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;

            Label lb = new Label();
            lb.FontFamily = new System.Windows.Media.FontFamily("Ariel");
            lb.FontSize = 11;
            lb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            lb.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            lb.Height = 19;
            lb.Width = 25;
            lb.Content = (e.Row.GetIndex() + 1).ToString();
            sp.Children.Add(lb);

            Image Img = new Image();
            Img.Height = 1;
            Img.Width = 1;
            sp.Children.Add(Img);
            e.Row.Header = sp;

            LoadBreakPoint(e.Row);
        }

        private void testDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void TestListGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Set_Ldiary_RowHeaderStyle(object sender, RoutedEventArgs e)
        {
            DataGridRow selectedRow = sender as DataGridRow;
            int index = 0;
            var sp = (StackPanel)selectedRow.Header;
            Image Img = (Image)sp.Children[1];
            int.TryParse(((Label)sp.Children[0]).Content.ToString(), out index);
            if (Img.Source == null)
            {
                Img.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/AutomationClient;component/Images/Red-Ball-icon.png", UriKind.Relative));
                Img.Height = 15;
                Img.Width = 15;
                model.BreakPHandler.AddStepIndex(model.SelctedScriptName, index);
            }
            else
            {
                ((Image)sp.Children[1]).Source = null;
                model.BreakPHandler.RemoveStepIndex(model.SelctedScriptName, index);
            }
        }

        public void LoadBreakPoint(DataGridRow row)
        {
            string scriptName = model.SelctedScriptName;
            var bklist = model.BreakPHandler.BreakPointObjList;
            var bko = (from bk in bklist where bk.SriptName == scriptName select bk).FirstOrDefault();
            if (bko != null)
            {
                var sp = (StackPanel)row.Header;
                int index = int.Parse(((Label)sp.Children[0]).Content.ToString());
                if (bko.GetStepsIndexs().IndexOf(index) != -1)
                {
                    ((Image)sp.Children[1]).Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/AutomationClient;component/Images/Red-Ball-icon.png", UriKind.Relative));
                    ((Image)sp.Children[1]).Height = 15;
                    ((Image)sp.Children[1]).Width = 15;
                }
            }
        }

        #endregion Break Point Methods

        private void dgStepEntities_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void debugBtn_Click(object sender, RoutedEventArgs e)
        {
            model.BreakPHandler.BreakPointEnable = !model.BreakPHandler.BreakPointEnable;
            debugBtn.Label = model.BreakPHandler.BreakPointEnable ? "DebugOn" : "DebugOff";
        }

        private void searchControl_RunFilterClick(object sender, RoutedEventArgs e)
        {
            string[] filesArray = Directory.GetFiles(searchControl.Path, "*.*", SearchOption.AllDirectories);
            string fName = searchControl.SearchedFile;
            foreach (var item in filesArray)
            {
                FileInfo f = new FileInfo(item);

                if (f.Name.ToLower().Contains(fName.ToLower()))
                {
                    searchControl.FilteredList.Add(item);
                }
            }
        }

        private void searchControl_OpenSelectedFileClick(object sender, RoutedEventArgs e)
        {
            model.LoadNodeData(searchControl.SelectedFile);
        }

        //this disable auto scrolling for tree when the stack panel get focused
        private void stackTree_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        private void MenuItemTearDown_Click(object sender, RoutedEventArgs e)
        {
            Singleton.Instance<DesignerViewModel>().AddTearDown(((System.Windows.Controls.HeaderedItemsControl)(sender)).Tag.ToString());
        }

        private void ClearTearSuiteDownBtn_Click(object sender, RoutedEventArgs e)
        {
            Singleton.Instance<DesignerViewModel>().AddTearDown(string.Empty);
        }
    }
}