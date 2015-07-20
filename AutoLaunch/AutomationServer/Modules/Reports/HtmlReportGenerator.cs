using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutomationCommon;

namespace AutomationServer.Modules.Reports
{
    public class HtmlReportGenerator : IReportGenerator
    {
        private const string PASS_COLOR = "#92D050";//coloring numbers for rows
        private const string FAIL_COLOR = "#ED4D5A";
        private const string SKIP_COLOR = "#FFCC66";
        private const string NON_COLOR = "#FFFFFF";

        private TestSuite _suite;

        //Main method for generating the report
        public string GenerateReport(TestSuite suite)
        {
            _suite = suite;
            string htmlReport = string.Empty;

            htmlReport += GetCssStyle();
            htmlReport += GetReportTitle();
            htmlReport += GetSuiteTitleAndDetail();
            htmlReport += GetTestSuiteDetailedTable();
            htmlReport += GetTestDetailAndLogFile();
            return htmlReport;
        }

        //1. CSS styling file for tables headers..etc..
        private string GetCssStyle()
        {
            return Properties.Resources.Style;
        }

        //2. Report Title
        private string GetReportTitle()
        {
            return "<h1>Auto Lunch Report" + System.Environment.NewLine;
        }

        //3. Suite detail report
        private string GetSuiteTitleAndDetail()
        {
            var fail = _suite.Entities.Count(t => t.Status == AutomationCommon.Enums.Status.Fail);
            var pass = _suite.Entities.Count(t => t.Status == AutomationCommon.Enums.Status.Pass);
            var skipped = _suite.Entities.Count(t => t.Status == AutomationCommon.Enums.Status.Skipped);
            var non = _suite.Entities.Count(t => t.Status == AutomationCommon.Enums.Status.NoN);
            var retVal = new List<string>();
            retVal.Add(AddHeading2("Suite Result Summary"));
            retVal.Add(AddParagraph(string.Format("Suite Name: {0}", _suite.ShortName())));
            retVal.Add(AddParagraph(string.Format("Start Time: {0}", _suite.StartTime)));
            retVal.Add(AddParagraph(string.Format("End Time: {0}", _suite.EndTime)));
            retVal.Add(AddParagraph(string.Format("Total Duration (Sec): {0}", _suite.ExecutionDuration())));
            int minutes = _suite.ExecutionDuration() / 60;
            string seconds = (_suite.ExecutionDuration() - (60 * minutes)).ToString().PadLeft(2, '0');
            retVal.Add(AddParagraph(string.Format("Total Duration (Minutes): {0}:{1}", minutes, seconds)));
            retVal.Add(AddParagraph(string.Format("Status: {0}", fail == 0 ? AutomationCommon.Enums.Status.Pass : AutomationCommon.Enums.Status.Fail)));
            retVal.Add(AddParagraph(string.Format("Detailed Status:  {0} Failed , {1} Pass , {2} Skip , {3} Not Executed", fail, pass, skipped, non)));
            retVal.Add(AddHeading2(string.Empty));
            retVal.Add(System.Environment.NewLine);
            return string.Join(System.Environment.NewLine, retVal.ToArray());
        }

        //4. Suite Table & Status
        #  region Suite Table & status

        private string GetTestSuiteDetailedTable()
        {
            var table = new List<string>();
            table.Add("<table class=" + '"' + "reportTable" + '"' + ">");
            table.Add(StartTableRow());
            table.Add(AddTableRow("Test Name"));
            table.Add(AddTableRow("Status"));
            table.Add(AddTableRow("Duration"));
            table.Add(AddTableRow("Start Time"));
            table.Add(CloseTableRow());

            foreach (var testSuiteEntity in _suite.Entities)
            {
                table.Add(GetRowStatusColor(testSuiteEntity.Status));//Start row and color depands on the status
                table.Add(AddTableCell(testSuiteEntity.ShortName()));
                table.Add(AddTableCell(testSuiteEntity.Status.ToString()));
                table.Add(AddTableCell(testSuiteEntity.ExecutionDuration().ToString()));
                table.Add(AddTableCell(testSuiteEntity.StartTime.ToString()));
                table.Add(CloseTableRow());//close row
            }
            table.Add(CloseTable());
            table.Add(AddLineSeperator());
            return string.Join(System.Environment.NewLine, table.ToArray());
        }

        //coloring rows depands on the status
        private string GetRowStatusColor(Enums.Status status)
        {
            string row = "<tr BGCOLOR=" + '"';
            switch (status)
            {
                case Enums.Status.Fail:
                    row += FAIL_COLOR;
                    break;

                case Enums.Status.Skipped:
                    row += SKIP_COLOR;
                    break;

                case Enums.Status.Pass:
                    row += PASS_COLOR;
                    break;

                case Enums.Status.NoN:
                    row += NON_COLOR;
                    break;
            }

            return row + '"' + ">";
        }

        # endregion

        private string GetTestDetailAndLogFile()
        {
            var retVal = new List<string>();

            foreach (var t in _suite.Entities)
            {
                if (t.Status != Enums.Status.Skipped)
                {
                    retVal.Add(AddHeading2(string.Format("Test: {0}", t.ShortName())));
                    //get the test report (scripts & steps)
                    retVal.Add(GetTestDetailTable(t.Test));
                    retVal.Add(AddLineSeperator());
                }
            }

            retVal.Add(AddHeading2("Suite Log File"));
            retVal.Add(GetLogFile());
            return string.Join(System.Environment.NewLine, retVal.ToArray());
        }

        private string GetTestDetailTable(TestObj test)
        {
            var scriptTable = new List<string>();

            var testTable = new List<string>();
            testTable.Add("<table class=" + '"' + "reportTable" + '"' + ">");
            testTable.Add(StartTableRow());
            testTable.Add(AddTableRow("Script Name"));
            testTable.Add(AddTableRow("Status"));
            testTable.Add(AddTableRow("Duration"));
            testTable.Add(AddTableRow("Start Time"));
            testTable.Add(CloseTableRow());

            foreach (TestEntity testEntity in test.Entities)
            {
                testTable.Add(GetRowStatusColor(testEntity.Status));//Start row and color depands on the status
                testTable.Add(AddTableCell(testEntity.ShortName()));
                testTable.Add(AddTableCell(testEntity.Status.ToString()));
                testTable.Add(AddTableCell(testEntity.Script.ExecutionDuration().ToString()));
                testTable.Add(AddTableCell(testEntity.Script.StartTime.ToString()));
                testTable.Add(CloseTableRow());//close row

                //get script table
                scriptTable.Add(GetScripDetailTable(testEntity));
            }

            testTable.Add(CloseTable());//close Table
            testTable.Add(AddHeading2(string.Empty));
            testTable.Add(string.Join(System.Environment.NewLine, scriptTable.ToArray()));
            return string.Join(System.Environment.NewLine, testTable.ToArray());
        }

        private string GetScripDetailTable(TestEntity testEntity)
        {
            var table = new List<string>();
            table.Add(AddHeading3(string.Format("Script: {0}", testEntity.ShortName())));
            table.Add("<table class=" + '"' + "reportTable" + '"' + ">");
            table.Add(StartTableRow());
            table.Add(AddTableRow("Step Name"));
            table.Add(AddTableRow("Status"));
            table.Add(AddTableRow("Duration"));
            table.Add(AddTableRow("Start Time"));
            table.Add(CloseTableRow());

            foreach (StepEntity s in testEntity.Script.Entities)
            {
                table.Add(GetRowStatusColor(s.Status));//Start row and color depands on the status
                table.Add(AddTableCell(s.Name));
                table.Add(AddTableCell(s.Status.ToString()));
                table.Add(AddTableCell(s.ExecutionDuration().ToString()));
                table.Add(AddTableCell(s.StartTime.ToString()));
                table.Add(CloseTableRow());//close row
            }

            table.Add(CloseTable());//close Table
            table.Add(AddHeading2(string.Empty));
            return string.Join(System.Environment.NewLine, table.ToArray());
        }

        public string GetLogFile()
        {
            return AddLogFile(CommonHelper.ReadTextFileAsList(AutoApp.Logger.GetLogFileName()));
        }

        //HTML TAGS HELPERS
        private string AddHeading3(string data)
        {
            return "<h3>" + data;
        }

        private string AddHeading2(string data)
        {
            return "<h2>" + data;
        }

        private string StartTableRow()
        {
            return "<tr>";
        }

        private string CloseTableRow()
        {
            return "</tr>" + System.Environment.NewLine;
        }

        private string CloseTable()
        {
            return "</table>" + System.Environment.NewLine;
        }

        private string AddTableRow(string data)
        {
            return "\t" + "<th>" + data;
        }

        private string AddTableCell(string data)
        {
            return "\t" + "<td>" + data;
        }

        private string AddParagraph(string data)
        {
            return "<p>" + data;
        }

        private string AddLineSeperator()
        {
            return "<hr>" + System.Environment.NewLine; ;
        }

        private string AddParagraphs(string data)
        {
            return "<pre>" + System.Environment.NewLine + data + System.Environment.NewLine + "</pre>" +
                   System.Environment.NewLine;
        }

        private string AddLogFile(IEnumerable<string> data)
        {
            var log = new StringBuilder();

            foreach (var row in data)
            {
                if (row.Contains("| Error |") || row.Contains("| Fatal |") || row.Contains("| Fail |"))
                    log.Append("<p><font color=" + '"' + "#990000" + '"' + ">" + row + "</font></p>");
                else
                    log.Append("<p>" + row);
            }
            return log.ToString();
        }
    }
}