using System;
using System.IO;
using System.Reflection;
using System.Xml;
using AutomationCommon;

namespace AutomationServer
{
    public class TestEntity : EntityBase
    {
        public bool Enable { get; set; }

        public string Comment { get; set; }

        public ScriptObj Script { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Params { get; set; }

        public string ShortName()
        {
            return new FileInfo(Name).Name;
        }

        public TestEntity(ScriptObj script, string fullName)
        {
            Script = script;
            FullName = fullName;
            FileInfo f = new FileInfo(fullName);
            if (f.Exists)
                Name = f.Name.Replace(StaticFields.SCRIPT_EXTENTION, string.Empty);
        }

        // Copy Constructor via reflection
        public TestEntity(TestEntity msg)
        {
            // get all the fields in the class
            FieldInfo[] fields_of_class = this.GetType().GetFields(
             BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            // copy each value over to 'this'
            foreach (FieldInfo fi in fields_of_class)
            {
                fi.SetValue(this, fi.GetValue(msg));
            }
        }

        public override string ToString()
        {
            var sw = new System.IO.StringWriter();
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                writer.WriteStartElement("Script");
                writer.WriteElementString("Enable", Enable.ToString());
                writer.WriteElementString("Name", FullName);
                writer.WriteElementString("Comment", Comment);
                writer.WriteElementString("Params", Params);
                writer.WriteEndElement();
            }
            string retval = sw.ToString();
            sw.Close();
            sw.Dispose();
            return retval;
        }

        public override void Execute()
        {
            StartTime = DateTime.Now;
            Singleton.Instance<SavedData>().UpdateParams(Params);
            // Singleton.Instance<FlowHandler>().ActiveScriptEntity = this;//setting the active script for flow handler
            AutoApp.Logger.WriteInfoLog(string.Format("============== Starting script {0} ==============", Name));
            Singleton.Instance<ActionHandler>().ActiveScript = Script;
            Script.Execute();
            AutoApp.Logger.WriteInfoLog(string.Format("============== Finished script {0} ==============" + System.Environment.NewLine, Name));
            EndTime = DateTime.Now;
        }
    }
}