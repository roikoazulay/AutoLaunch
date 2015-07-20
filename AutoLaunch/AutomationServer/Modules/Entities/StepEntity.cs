using System;
using System.Xml;
using AutomationCommon;

namespace AutomationServer
{
    public class StepEntity : EntityBase
    {
        public bool Enable { get; set; }

        public string Comment { get; set; }

        public string OnFailureLabel { get; set; }

        public ActionBase Action { get; set; }

        public string StepDetails
        {
            get
            {
                if (Singleton.Instance<AppSettings>().ShowStepDetails)
                    return string.Join(",", Action.Details);
                else
                    return string.Empty;
            }
        }

        public StepEntity(ActionBase action)
        {
            Action = action;
        }

        public StepEntity(StepEntity entity)
        {
            try
            {
                Enable = entity.Enable;
                Comment = entity.Comment;
                Action = entity.Action;
                OnFailureLabel = entity.OnFailureLabel;
            }
            catch { }
        }

        public string Name
        {
            get { return Action.Name; }
        }

        public bool HasFinished()
        {
            return Action.HasFinished;
        }

        public override string ToString()
        {
            var sw = new System.IO.StringWriter();
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                writer.WriteStartElement("Step");
                writer.WriteElementString("Enable", Enable.ToString());
                writer.WriteElementString("Comment", Comment);
                writer.WriteElementString("OnFailureLable", OnFailureLabel);
                writer.WriteRaw(Action.ToString());
                writer.WriteEndElement();
            }
            string retval = sw.ToString();
            sw.Close();
            sw.Dispose();
            return retval;
        }

        #region IExecute Members

        public override void Execute()
        {
            StartTime = DateTime.Now;
            Action.ActionStatus = Enums.Status.Fail;
            Action.Execute();
            EndTime = DateTime.Now;
        }

        #endregion IExecute Members
    }
}