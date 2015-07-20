using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AutomationCommon
{
    public class CommonHelper
    {
        public static class PopWindow
        {
        }

        public static string ExtructValueFromRow(string title, string segment)
        {
            string tmpString = "";
            int startPos = segment.IndexOf(title);
            if (startPos != -1)
            {
                int endPos = segment.IndexOf(System.Environment.NewLine, startPos);
                if (endPos != -1)
                    tmpString = segment.Substring(startPos + title.Length, endPos - startPos - title.Length);
            }

            return tmpString;
        }

        /// <summary>Finds the value from the end of the startString to the start of the endString </summary>
        public static string ExtructValueBetween(string startString, string endString, string segment)
        {
            string tmpString = "";
            try
            {
                int startPos = segment.IndexOf(startString);
                if (startPos == -1)
                    return "";
                int endPos = segment.IndexOf(endString, startPos + 1);

                if (endPos == -1)
                    return "";

                tmpString = segment.Substring(startPos + startString.Length, endPos - startPos - startString.Length);
            }
            catch
            {
            }
            return tmpString;
        }

        public static bool FileExist(string fileName)
        {
            FileInfo f = new FileInfo(fileName);
            return f.Exists;
        }

        public static DataTable ImportCsvToDataTable(string filename, string separatorChar, out List<string> errors)
        {
            errors = new List<string>();
            var table = new DataTable("Table");
            if (!CommonHelper.FileExist(filename))
            {
                AutoApp.Logger.WriteFailLog(string.Format("file {0} does not exist", filename));
                return table;
            }

            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(fs);
            //var sr = new StreamReader(filename, Encoding.Default);

            string line;
            var i = 0;
            int columnName = 0;
            while (sr.Peek() >= 0)
            {
                try
                {
                    line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    var values = line.Split(new[] { separatorChar }, StringSplitOptions.None);

                    var row = table.NewRow();
                    for (var colNum = 0; colNum < values.Length; colNum++)
                    {
                        
                        var value = values[colNum];
                        if (i == 0)
                        {
                           // table.Columns.Add(value, typeof(String));
                            table.Columns.Add(columnName.ToString(), typeof(String));
                            columnName++;
                        }
                        else
                        {
                            row[table.Columns[colNum]] = value;
                        }
                    }
                    if (i != 0) table.Rows.Add(row);
                }
                catch (Exception ex)
                {
                    AutoApp.Logger.WriteFatalLog(ex.ToString());
                }
                i++;
            }

            sr.Close();
            sr.Dispose();
            fs.Close();
            fs.Dispose();
            return table;
        }

        public static DataTable ConvertCSVtoDataTable(string filename, string separatorChar)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(filename))
            {
                var headers = sr.ReadLine().Split(new[] { separatorChar }, StringSplitOptions.None);
                int columCount = 0;
                foreach (string header in headers)
                {
                    dt.Columns.Add("Col"+columCount.ToString());
                    columCount++;
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(new[] { separatorChar }, StringSplitOptions.None);
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < columCount; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }


            return dt;
        }

        public static string DataTableToCSV(DataTable table)
        {
            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
                }
            }

            return result.ToString();
        }

        public static bool DeleteFile(string fileName)
        {
            try
            {
                File.Delete(fileName);
                return !File.Exists(fileName);
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
            }

            return false;
        }

        public static void AddToLogFile(string fileName, string text)
        {
            try
            {
                StreamWriter SW;
                SW = File.AppendText(fileName);
                SW.Write(text);
                SW.Close();
                SW.Dispose();
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
            }
        }

        public static void SaveStringToFile(string data, string fileName)
        {
            var retval = SaveToFile(data, fileName);

            if (retval.Contains("it is being used by another process"))
            {
                AutoApp.Logger.WriteFatalLog("exception , file is being used by another process.... retrying in 1322 msec");
                new System.Threading.ManualResetEvent(false).WaitOne(1322);//waiting for file to release
                retval = SaveToFile(data, fileName);
                if (!string.IsNullOrEmpty(retval))
                    AutoApp.Logger.WriteFailLog("Failed to write file for the second time");
            }
        }

        private static string SaveToFile(string data, string fileName)
        {
            string retVal = string.Empty;
            try
            {
                var output = new StreamWriter(fileName);//, false, Encoding.Unicode); // System.Text.Encoding.ASCII;
                output.Write(data);
                output.Close();
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
                retVal = ex.ToString();
            }

            return retVal;
        }

        public static bool AppandStringToFile(string data, string fileName)
        {
            bool res = false;
            // AutoApp.Logger.WriteInfoLog(string.Format("Appending text to file {0}", fileName));

            try
            {
                var output = File.AppendText(fileName);
                output.WriteLine(data);
                output.Flush();
                output.Close();
                res = true;
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
            }

            return true;
        }

        public static bool ReadTextFile(string fineName, ref string retVal)
        {
            //supports reading of files open by another process (secureCRT)
            string data = string.Empty;
            UTF8Encoding encoding = new UTF8Encoding(true);

            retVal = data;
            FileInfo file = new FileInfo(fineName);
            if (!file.Exists)
            {
                AutoApp.Logger.WriteWarningLog(string.Format("file {0} does not exist",fineName));
                return false;
            }
               

            //  var encoding = Encoding.GetEncoding("iso-8859-1");
            //StreamReader reader = new StreamReader(fineName, e, true);
            //retVal = reader.ReadToEnd();
            //reader.Close();
            //reader.Dispose();

            var reader = File.Open(fineName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            byte[] byteData = new byte[reader.Length];
            reader.Read(byteData, 0, byteData.Length);
            data = encoding.GetString(byteData);
            reader.Close();
            reader.Dispose();
            retVal = data;

            return true;
        }

        public static List<string> ReadTextFileAsListOld(string fineName)
        {
            var data = new List<string>();

            string line;
            try
            {
                var reader = File.OpenText(fineName);
                while ((line = reader.ReadLine()) != null)
                    data.Add(line);
                reader.Close();
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
            }

            return data;
        }

        public static List<string> ReadTextFileAsList(string fileName)
        {
            var data = new List<string>();

            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                long bufferSize = fs.Length;
                byte[] bytBuffer = new byte[bufferSize];
                fs.Read(bytBuffer, 0, bytBuffer.Length);//reading new segment
                string sBuffer = System.Text.Encoding.ASCII.GetString(bytBuffer);
                foreach (var myString in sBuffer.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                    data.Add(myString);

                fs.Close();
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.ToString());
            }

            return data;
        }

        public static string ExtructLineByString(string value, string text, bool allLines = false)
        {
            StringReader sr = new StringReader(text);
            StringBuilder sb = new StringBuilder();
            string line = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains(value))
                {
                    sb.AppendLine(line);
                    if (!allLines)
                        break;
                }
            }

            return sb.ToString();
        }

        public static string ExtructLineByRegexString(string pattern, string text, bool allLines = false)
        {
            StringReader sr = new StringReader(text);
            StringBuilder sb = new StringBuilder();
            string line = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                if (Regex.Match(line, pattern).Success)
                {
                    sb.AppendLine(line);
                    if (!allLines)
                        break;
                }
            }

            return sb.ToString();
        }


        public static string ConvertHexToAscii(string data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i += 2)
            {
               
                try
                {
                    string hs = data.Substring(i, 2);
                    sb.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
                }
                catch
                {

                }
                
            }

            return sb.ToString();
        }


        public static void GenerateSuiteLookUpTable()
        {
            StringBuilder sb = new StringBuilder();

            string[] files = Directory.GetFiles(StaticFields.SUITE_PATH, "*.tsu", SearchOption.AllDirectories);
            FileInfo f;
            foreach (string file in files)
            {
                f = new FileInfo(file);
                sb.Append(f.Name.Replace(".tsu", "") + "," + f.Directory + System.Environment.NewLine);
            }
            f = new FileInfo(StaticFields.SUITE_LOOKUP_TABLE);
            f.Delete();
            SaveStringToFile(sb.ToString(), f.FullName);
        }

        public static string GetSuiteLocation(string suiteName)
        {
            try
            {
                string line;
                var reader = File.OpenText(StaticFields.SUITE_LOOKUP_TABLE);
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    if (data[0] == suiteName)
                        return data[1] + "\\" + data[0] + ".tsu";
                }
                reader.Close();
            }
            catch
            { }

            return string.Empty;
        }
    }
}