using System.Collections.Generic;
using System.IO;
using AutomationCommon;

namespace AutomationClient
{
    public class Folder
    {
        public string FullPath { get; set; }

        public string Label { get; set; }

        public List<Folder> Files { get; set; }

        public string Image { get; set; }

        public string ActionImage { get; set; }

        public Folder(string fullpath)
        {
            FullPath = fullpath;
            Label = fullpath.Substring(fullpath.LastIndexOf('\\') + 1, fullpath.Length - fullpath.LastIndexOf('\\') - 1).Replace(StaticFields.SCRIPT_EXTENTION, "");

            if (fullpath.Contains(StaticFields.SCRIPT_EXTENTION))
            {
                Image = "/Images/ComplexObject.png";
                ActionImage = "/Images/Plus-icon.png";
            }
            else if (fullpath.Contains(StaticFields.TEST_EXTENTION))
            {
                Image = "/Images/document.png";
                ActionImage = "/Images/Plus-icon.png";
            }
            else if (fullpath.Contains(StaticFields.SUITE_EXTENTION))
            {
                Image = "/Images/Actions-view-calendar-list-icon.png";
            }
            else
            {
                ActionImage = "/Images/folder-open-icon.png";
                Image = "";
            }

            Files = new List<Folder>();
        }
    }

    public class FileScanner
    {
        public static List<Folder> TestListFolder { get; set; }

        public static List<Folder> ScriptListFolder { get; set; }

        public static List<Folder> SuiteListFolder { get; set; }

        public List<Folder> TestList;
        public List<Folder> ScriptList;
        public List<Folder> SuiteList;

        public List<Folder> Files;

        public FileScanner()
        {
            ScriptListFolder = new List<Folder>();
            TestListFolder = new List<Folder>();
            SuiteListFolder = new List<Folder>();

            TestList = new List<Folder>();
            ScriptList = new List<Folder>();
            SuiteList = new List<Folder>();

            FillFiles(StaticFields.SCRIPT_PATH, ScriptList);
            FillFiles(StaticFields.TEST_PATH, TestList);
            FillFiles(StaticFields.SUITE_PATH, SuiteList);
        }

        private void FillFiles(string path, List<Folder> rootTarget)
        {
            Folder rootFolder = new Folder(path);
            PopulateTreeViewFiles(path, rootFolder);
            rootTarget.Add(PopulateTreeViewDirectories(path, rootFolder));
        }

        public Folder PopulateTreeViewDirectories(string directoryValue, Folder parentNode)
        {
            List<string> directorys = new List<string>();
            var folders = new DirectoryInfo(directoryValue).GetDirectories();

            foreach (var directoryInfo in folders)
            {
                if (!directoryInfo.Attributes.ToString().ToLower().Contains("hidden"))//filter SVN folders (hidden ones)
                    directorys.Add(directoryInfo.FullName);
            }

            try
            {
                if (directorys.Count != 0)
                {
                    foreach (string directory in directorys)
                    {
                        Folder f = new Folder(directory);
                        parentNode.Files.Add(f);
                        PopulateTreeViewFiles(directory, f);
                        PopulateTreeViewDirectories(directory, f);
                    }
                }
            }
            catch
            {
            }
            return parentNode;
        }

        private void PopulateTreeViewFiles(string directoryValue, Folder parentNode)
        {
            string[] filesArray = Directory.GetFiles(directoryValue);
            foreach (string file in filesArray)
            {
                if (file.Contains(StaticFields.TEST_EXTENTION))//adds test name to the test list
                    TestListFolder.Add(new Folder(file));
                else if (file.Contains(StaticFields.SCRIPT_EXTENTION))
                    ScriptListFolder.Add(new Folder(file));
                else if (file.Contains(StaticFields.SUITE_EXTENTION))
                    ScriptListFolder.Add(new Folder(file));

                var f = new Folder(file);
                parentNode.Files.Add(f);
            }
        }

        //static List<string> searchfiles = new List<string>();
        //public void FindFiles(string name,string rootFolder)
        //{
        //    searchfiles = new List<string>();

        //}
    }
}