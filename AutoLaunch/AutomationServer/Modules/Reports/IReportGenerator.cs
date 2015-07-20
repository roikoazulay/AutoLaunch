namespace AutomationServer.Modules.Reports
{
    public interface IReportGenerator
    {
        string GenerateReport(TestSuite suite);
    }
}