using System;
using System.IO;
using System.Xml;
using AutomationCommon;

namespace AutomationServer
{
    public class FileHandler
    {
        public static ScriptObj ExtructScriptFromFile(string fileName)
        {
            string detail = string.Empty;
            if (string.IsNullOrEmpty(fileName))
                return null;

            if (!File.Exists(fileName))
            {
                return null;
            }
            ScriptObj script = new ScriptObj();
            ActionBase action = null;
            StepEntity stepEntity = null;
            using (XmlReader reader = XmlReader.Create(fileName))
            // using (XmlReader reader =XmlReader.Create(new StreamReader(fileName, Encoding.GetEncoding("ISO-8859-9"))))
            {
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Script":
                                script = new ScriptObj();
                                break;

                            case "Enable":
                                stepEntity.Enable = bool.Parse(reader.ReadInnerXml());
                                break;

                            case "Description":
                                detail = reader.ReadInnerXml();
                                detail = detail.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively
                                script.Description = detail;
                                break;

                            case "Step":
                                if (stepEntity != null)
                                {
                                    stepEntity.Action = action;
                                    stepEntity.Action.Construct();
                                    script.Entities.Add(stepEntity);
                                }
                                stepEntity = new StepEntity(action);
                                break;

                            case "OnFailureLable":
                                stepEntity.OnFailureLabel = reader.ReadInnerXml();
                                break;

                            case "Comment":
                                detail = reader.ReadInnerXml();
                                detail = detail.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively
                                stepEntity.Comment = detail;
                                break;

                            case "ID":
                                var id = Enum.Parse(typeof(Enums.ActionTypeId), reader.ReadInnerXml().ToString());
                                action = ActionFactory.GetAction((Enums.ActionTypeId)id);
                                break;

                            case "Name":
                                action.Name = reader.ReadInnerXml();
                                break;

                            case "Detail":
                                detail = reader.ReadInnerXml();
                                detail = detail.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively
                                action.Details.Add(detail);
                                break;
                        }
                    }
                }
            }
            //adding the last step entity

            if (action != null)
            {
                stepEntity.Action = action;
                stepEntity.Action.Construct();
                script.Entities.Add(stepEntity);
            }

            return script;
        }

        public static TestObj ExtructTestFromFile(string fileName)
        {
            string detail = string.Empty;
            if (string.IsNullOrEmpty(fileName))
                return null;

            var test = new TestObj();
            TestEntity testEntity = null;
            bool tmpEntityEnable = false;
            string tmpEntityName = string.Empty;
            fileName = fileName.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively

            using (XmlReader reader = XmlReader.Create(fileName))
            {
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "Test":
                                test = new TestObj();
                                break;

                            case "Enable":
                                tmpEntityEnable = bool.Parse(reader.ReadInnerXml());
                                break;

                            case "Description":
                                detail = reader.ReadInnerXml();
                                detail.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively
                                test.Description = detail;
                                break;

                            case "Script":
                                tmpEntityEnable = false;
                                tmpEntityName = string.Empty;
                                break;

                            case "Comment"://here we add the new test entity (on the last property of the entity)
                                tmpEntityName = tmpEntityName.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");
                                testEntity = new TestEntity(ExtructScriptFromFile(tmpEntityName), tmpEntityName);

                                detail = reader.ReadInnerXml();
                                detail = detail.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively

                                testEntity.Comment = detail;
                                testEntity.Enable = tmpEntityEnable;
                                test.Entities.Add(testEntity);

                                //tmpEntityName = reader.ReadInnerXml();
                                //tmpEntityName= tmpEntityName.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively
                                //testEntity = new TestEntity(ExtructScriptFromFile(tmpEntityName), tmpEntityName);
                                //testEntity.Comment = reader.ReadInnerXml();
                                //testEntity.Enable = tmpEntityEnable;
                                //test.Entities.Add(testEntity);
                                break;

                            case "Name":
                                detail = reader.ReadInnerXml();
                                detail.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively
                                tmpEntityName = detail;
                                break;

                            case "Params":
                                detail = reader.ReadInnerXml();
                                detail = detail.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively
                                testEntity.Params = detail;
                                break;
                        }
                    }
                }
            }

            return test;
        }

        public static TestSuite ExtructTestSuiteFromFile(string fileName)
        {
            string detail = string.Empty;
            if (string.IsNullOrEmpty(fileName))
                return null;

            var testSuite = new TestSuite();
            TestSuiteEntity testSuiteEntity = null;
            bool tmpEntityEnable = false;
            string tmpEntityName = string.Empty;
            fileName = fileName.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively

            using (XmlReader reader = XmlReader.Create(fileName))
            {
                while (reader.Read())
                {
                    // Only detect start elements.
                    if (reader.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "TestSuite":
                                testSuite = new TestSuite();
                                break;

                            case "Enable":
                                tmpEntityEnable = bool.Parse(reader.ReadInnerXml());
                                break;

                            case "Description":
                                detail = reader.ReadInnerXml();
                                detail.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively
                                testSuite.Description = detail;
                                break;
                            case "TearDownScript":
                                testSuite.TearDownScript = reader.ReadInnerXml();
                                break;
                            case "Test":
                                tmpEntityEnable = false;
                                tmpEntityName = string.Empty;
                                break;

                            case "Name"://here we add the new test entity (on the last property of the entity)
                                tmpEntityName = reader.ReadInnerXml();
                                tmpEntityName = tmpEntityName.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively
                                testSuiteEntity = new TestSuiteEntity(ExtructTestFromFile(tmpEntityName), tmpEntityName);
                                testSuiteEntity.Enable = tmpEntityEnable;
                                testSuite.Entities.Add(testSuiteEntity);
                                break;

                            case "Params":
                                detail = reader.ReadInnerXml();
                                detail = detail.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively
                                testSuiteEntity.Params = detail;
                                break;

                            case "Comment":
                                detail = reader.ReadInnerXml();
                                detail = detail.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");//encode special characters -The characters &, <, and > are replaced with &amp;, &lt;, and &gt;, respectively
                                testSuiteEntity.Comment = detail;
                                break;
                            
                        }
                    }
                }
            }

            return testSuite;
        }

        public static void GenerateFile(EntityBase obj, string fileName)
        {
            BackUpFile(fileName);
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fileStream))
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                string data = obj.ToString();
                //writer.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");

                writer.WriteRaw(data);
            }
        }

        public static void BackUpFile(string fileName)
        {
            FileInfo f = new FileInfo(fileName);
            if (f.Exists)
                f.CopyTo(Path.Combine(StaticFields.BACKUP_FOLDER, f.Name + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")), true);
        }
    }
}