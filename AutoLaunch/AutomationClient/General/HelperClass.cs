using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AutomationCommon;
using AutomationServer;

namespace AutomationClient
{
    public class HelperClass
    {
        public static bool ShowQuestionMessageBox(string title, string msg)
        {
            if (MessageBox.Show(msg, title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                return true;

            return false;
        }

        public static MessageBoxResult ShowQuestionMessageBoxResult(string title, string msg)
        {
            return MessageBox.Show(msg, title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        }

        public static void ShowMessageBox(string title, string msg)
        {
            MessageBox.Show(msg, title);
        }

        public static void ShowErrorMessage(string info)
        {
            MessageBox.Show(info, "Error", MessageBoxButton.OK,
                                   MessageBoxImage.Error);
        }

        public static void SwopObservableCollectionElements<T>(ObservableCollection<T> collection, ref int selectedIndex,
                                                               bool moveUp)
        {
            if (selectedIndex == -1)//validation when no item is seleted
                return;
            int count = collection.Count;

            if (moveUp && selectedIndex == 0)
                return;

            if (!moveUp && selectedIndex == count - 1)
                return;

            if (moveUp)
            {
                var values = new List<T> { collection[selectedIndex], collection[selectedIndex - 1] };
                collection[selectedIndex] = values[1];
                collection[selectedIndex - 1] = values[0];
                selectedIndex--;
            }
            else
            {
                var values = new List<T> { collection[selectedIndex], collection[selectedIndex + 1] };
                collection[selectedIndex] = values[1];
                collection[selectedIndex + 1] = values[0];
                selectedIndex++;
            }
        }

        public static string SaveFileDialog(string filter, string fileExtantion, string initialDir)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.InitialDirectory = initialDir;
            dlg.DefaultExt = fileExtantion; // ".txt";
            dlg.Filter = filter; //"Text documents (.txt)|*.txt";
            dlg.ShowDialog();
            return dlg.FileName;
        }

        public static string SaveScript(ScriptObj obj, string initialDir)
        {
            string fileName = SaveFileDialog("Script File (.spt)|*.spt", ".spt", initialDir);
            if (!string.IsNullOrEmpty(fileName))
                FileHandler.GenerateFile(obj, fileName);
            else
                ShowErrorMessage("Invalid file name");
            return fileName;
        }

        public static string SaveTest(TestObj obj, string initialDir)
        {
            string fileName = SaveFileDialog("Test File (.tst)|*.tst", ".tst", initialDir);
            if (!string.IsNullOrEmpty(fileName))
                FileHandler.GenerateFile(obj, fileName);
            else
                ShowErrorMessage("Invalid file name");
            return fileName;
        }

        public static string SaveToFile(EntityBase obj, string fileName = null)
        {
            //  string fileName = string.Empty;
            Type t = obj.GetType();
            if (fileName == null)
            {
                if (t == typeof(TestObj))
                    fileName = SaveFileDialog("Test File (.tst)|*.tst", ".tst", StaticFields.TEST_PATH);
                else if (t == typeof(TestSuite))
                    fileName = SaveFileDialog("Suite File (.tsu)|*.tsu", ".tsu", StaticFields.SUITE_PATH);
                else if (t == typeof(ScriptObj))
                    fileName = SaveFileDialog("Script File (.spt)|*.spt", ".spt", StaticFields.SCRIPT_PATH);
            }

            if (!string.IsNullOrEmpty(fileName))
                FileHandler.GenerateFile(obj, fileName);
            else
                ShowErrorMessage("Invalid file name");
            return fileName;
        }

        public static void ExportSuite(List<string> fileList)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string destFolder = dialog.SelectedPath;
                string cPath = string.Empty;
                foreach (var file in fileList)
                {
                    FileInfo f = new FileInfo(file);

                    if (f.Extension == ".tsu")
                        cPath = file.Substring(file.IndexOf("\\Suites"));
                    else if (f.Extension == ".tst")
                        cPath = file.Substring(file.IndexOf("\\Tests"));
                    else if (f.Extension == ".spt")
                        cPath = file.Substring(file.IndexOf("\\Scripts"));

                    f = new FileInfo(destFolder + cPath);
                    System.IO.Directory.CreateDirectory(f.Directory.ToString());
                    File.Copy(file, destFolder + cPath, true);
                }

                ShowMessageBox("Export Suite", "Suite Exported Successfully");
            }
        }

        public static string OpenFileDialog()
        {
            var dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.ShowDialog();
            return dlg.FileName;
        }

        public static string OpenFolderDialog()
        {
            var dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.ShowDialog();
            return dlg.SelectedPath;
        }

        public static void CreateFolders()
        {
            CreateFolders(StaticFields.INITIAL_PATH);
            CreateFolders(StaticFields.SCRIPT_PATH);
            CreateFolders(StaticFields.TEST_PATH);
            CreateFolders(StaticFields.SUITE_PATH);
            CreateFolders(StaticFields.LOG_PATH);
            CreateFolders(StaticFields.BACKUP_FOLDER);
        }

        public static void CreateFolders(string folderName)
        {
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
        }

        public static string GetLastFile(string path, string extention)
        {
            var directory = new DirectoryInfo(path);
            var myFile = (from f in directory.GetFiles()
                          where f.Extension == "." + extention
                          orderby f.LastWriteTime descending
                          select f).FirstOrDefault();

            if (myFile == null)
                return string.Empty;

            return myFile.FullName;
        }

        public static void ExecutePocess(string command)
        {
            try
            { System.Diagnostics.Process.Start(command); }
            catch { }
        }

        public static void ChangeControlState(Visual visual, bool state)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(visual, i);
                if (VisualTreeHelper.GetChildrenCount(childVisual) > 0)
                    ChangeControlState(childVisual, state);

                if (childVisual is Control)
                {
                    if (childVisual.GetType() == typeof(TextBox))
                        ((Control)childVisual).IsEnabled = state;
                    else if (childVisual.GetType() == typeof(ComboBox))
                        ((Control)childVisual).IsEnabled = state;
                    else if (childVisual.GetType() == typeof(CheckBox))
                        ((Control)childVisual).IsEnabled = state;
                }
            }
        }
    }
}