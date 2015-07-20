namespace AutomationServer.Modules.Reports
{
    //strategy pattern (Dependency injection)
    public class ReportManager
    {
        private IReportGenerator _reportGenObj;

        public ReportManager(IReportGenerator reportGenObj)
        {
            _reportGenObj = reportGenObj;
        }

        public string GenerateReport(TestSuite suite)
        {
            return _reportGenObj.GenerateReport(suite);
        }
    }
}