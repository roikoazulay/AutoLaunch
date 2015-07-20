using System;
using System.Collections.ObjectModel;
using System.Xml;
using AutomationCommon;

namespace AutomationServer
{
    public class TestObj : EntityBase
    {
        public ObservableCollection<TestEntity> Entities = new ObservableCollection<TestEntity>();

        public string Description { get; set; }

        public override string ToString()
        {
            var sw = new System.IO.StringWriter();
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                writer.WriteStartDocument();
                writer.WriteStartElement("Test");
                //writer.WriteElementString("Enable", Enable.ToString());
                writer.WriteElementString("Description", Description);
                writer.WriteStartElement("Scripts");
                foreach (TestEntity entity in Entities)
                    writer.WriteRaw(entity.ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            string retval = sw.ToString().Replace("utf-16", "utf-8"); ;
            sw.Close();
            sw.Dispose();
            return retval;
        }

        public override void Execute()
        {
            //  Status = Enums.Status.NoN;
            StartTime = DateTime.Now;
            foreach (TestEntity t in Entities)
            {
                if (Singleton.Instance<ActionHandler>().PauseStopOrContinueAction())
                    return;

                if (t.Enable)
                {
                    Singleton.Instance<ActionHandler>().ActiveScriptName = t.Name;
                    t.Execute();

                    t.Status = t.Script.Status;
                    if (t.Status == Enums.Status.Fail)
                        Status = Enums.Status.Fail;

                    bool stopOrSkip = Singleton.Instance<ActionHandler>().StopSkipOrContinueOnFail(t.Script.Status);
                    if (stopOrSkip)
                        break;
                }
                else
                {
                    t.Status = Enums.Status.Skipped;
                    AutoApp.Logger.WriteSkipLog(string.Format("Skipping Script {0}", t.Name));
                }
            }
            EndTime = DateTime.Now;
            //only if there are no failures on all stepentities then the script passes
            if (Status == Enums.Status.NoN)
                Status = Enums.Status.Pass;
        }
    }
}