using System.Collections.Generic;

namespace AutomationClient.Models
{
    internal interface IToolBox
    {
        string GetName();

        AutomationCommon.Enums.ActionTypeId GetId();

        List<string> GetProperties();
    }
}