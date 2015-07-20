using System;
using System.IO;
using System.Xml;
using AutomationCommon;

namespace AutomationServer
{
    public class TestSuiteEntity : EntityBase
    {
        public bool Enable { get; set; }

        public TestObj Test { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Params { get; set; }

        public string Comment { get; set; }

        public TestSuiteEntity(TestObj test, string fullName)
        {
            Test = test;
            FullName = fullName;
            var f = new FileInfo(fullName);
            if (f.Exists)
                Name = f.Name.Replace(StaticFields.SUITE_EXTENTION, string.Empty);
        }

        public string ShortName()
        {
            return new FileInfo(Name).Name.Replace(StaticFields.TEST_EXTENTION, string.Empty);
        }

        public override string ToString()
        {
            var sw = new System.IO.StringWriter();
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                writer.WriteStartElement("Test");
                writer.WriteElementString("Enable", Enable.ToString());
                writer.WriteElementString("Name", FullName);
                writer.WriteElementString("Params", Params);
                writer.WriteElementString("Comment", Comment);
                writer.WriteEndElement();
            }
            string retval = sw.ToString();
            sw.Close();
            sw.Dispose();
            return retval;
        }

        public override void Execute()
        {
            Singleton.Instance<SavedData>().UpdateParams(Params);
            Test = FileHandler.ExtructTestFromFile(FullName);
            StartTime = DateTime.Now;
            Test.Execute();
            EndTime = DateTime.Now;
        }
    }
}