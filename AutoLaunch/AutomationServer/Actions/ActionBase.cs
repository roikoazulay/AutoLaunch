using System;
using System.Collections.Generic;
using System.Xml;
using AutomationCommon;

namespace AutomationServer
{
    public abstract class ActionBase
    {
        public Enums.ActionTypeId TypeId { get; set; }

        public string Name;

        public Enums.Status ActionStatus { get; set; } //Pass,Fail,Skipped

        public string FailureReason { get; set; }

        public List<string> Details { get; set; }

        public bool HasFinished { get; set; }//not implemented

        protected ActionBase(Enums.ActionTypeId typeId)
        {
            ActionStatus = Enums.Status.Fail;
            HasFinished = false;
            Details = new List<string>();
            TypeId = typeId;
            Name = SplitCamelCase(typeId.ToString());
        }

        public static string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }

        public abstract void Execute();

        public override string ToString()
        {
            var sw = new System.IO.StringWriter();
            using (var writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                writer.WriteStartElement("Action");
                writer.WriteElementString("ID", ((int)TypeId).ToString());
                writer.WriteElementString("Name", Name);
                writer.WriteStartElement("Details");
                foreach (string data in Details)
                    writer.WriteElementString("Detail", data);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
            string retval = sw.ToString();
            sw.Close();
            sw.Dispose();
            return retval;
        }

        public abstract void Construct();
    }

    public abstract class EntityBase
    {
        public abstract void Execute();

        public Enums.Status Status { get; set; }//Pass,Fail,Skipped

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int ExecutionDuration()
        {
            return (int)EndTime.Subtract(StartTime).TotalSeconds;
        }

        public EntityBase()
        {
            Status = Enums.Status.NoN;
        }
    }
}