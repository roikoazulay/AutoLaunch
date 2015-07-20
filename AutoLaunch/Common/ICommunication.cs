using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace AutomationCommon
{
    [ServiceContract]
    public interface ICommunication
    {
        [OperationContract]
        void ExecuteSuite(string suiteName, int cycles, AutomationCommon.Enums.OnFailerAction action);

        [OperationContract]
        List<ClientMessage> GetMailBox();

        [OperationContract]
        bool IsConnected();

        [OperationContract]
        void StopExecution();

        [OperationContract]
        void PauseExecution();

        [OperationContract]//true if current executing suite
        bool IsActive();

        [OperationContract]
        int GetCycles();

        [OperationContract]
        int GetProgressPercentage();

        [OperationContract]
        SuiteProgressInfo GetSuiteProgressInfo();

        [OperationContract]
        List<string> GetVariablesNames();

        [OperationContract]
        string GetVariableData(string variableName);

        [OperationContract]
        string GetVersion();

        [OperationContract]
        string GetActiveStepInfo();

        [OperationContract]
        void SetBreakPointList(List<BreakPointObj> bpList, bool isEnable);

        [OperationContract]
        string GetLastExecutionStatus();
    }

    public struct SuiteProgressInfo
    {
        public string SuiteName { get; set; }

        public string ActiveTestName { get; set; }

        public string PassedCycles { get; set; }

        public string SuiteProgressPersantage { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SuiteName=" + SuiteName);
            sb.AppendLine("ActiveTestName=" + ActiveTestName);
            sb.AppendLine("PassedCycles=" + PassedCycles);
            sb.AppendLine("SuiteProgressPersantage=" + SuiteProgressPersantage);
            return sb.ToString();
        }
    }
}