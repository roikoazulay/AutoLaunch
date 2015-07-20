using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AutomationCommon
{
    [DataContract]
    public class BreakPointHandler
    {
        [DataMember]
        public bool BreakPointEnable { get; set; }

        [DataMember]
        public List<BreakPointObj> BreakPointObjList { get; set; }

        public void AddStepIndex(string scriptName, int stepIndex)
        {
            //scriptName = scriptName.Substring(0, scriptName.IndexOf("."));
            var bko = (from bk in BreakPointObjList where bk.SriptName == scriptName select bk).FirstOrDefault();
            if (bko != null)
            {
                bko.AddStepIndex(stepIndex);
            }
            else
            {
                var bk = new BreakPointObj(scriptName);
                bk.AddStepIndex(stepIndex);
                BreakPointObjList.Add(bk);
            }
        }

        public void RemoveStepIndex(string scriptName, int stepIndex)
        {
            var bko = (from bk in BreakPointObjList where bk.SriptName == scriptName select bk).FirstOrDefault();
            if (bko != null)
            {
                bko.RemoveStepIndex(stepIndex);
                if (bko.GetStepsIndexs().Count == 0)
                {
                    BreakPointObjList.Remove(bko);
                }
            }
        }

        public BreakPointHandler()
        {
            BreakPointObjList = new List<BreakPointObj>();
            BreakPointEnable = true;
        }
    }

    [DataContract]
    public class BreakPointObj
    {
        [DataMember]
        public string SriptName { get; set; }

        [DataMember]
        public List<int> StepIndexList;

        public bool Enable { get; set; }

        public BreakPointObj(string name)
        {
            SriptName = name;
            StepIndexList = new List<int>();
            Enable = true;
        }

        public BreakPointObj(BreakPointObj bko)
        {
            SriptName = bko.SriptName;
            StepIndexList = bko.GetStepsIndexs();
            Enable = true;
        }

        public void AddStepIndex(int index)
        {
            StepIndexList.Add(index);
        }

        public void RemoveStepIndex(int index)
        {
            StepIndexList.Remove(index);
        }

        public List<int> GetStepsIndexs()
        {
            return StepIndexList;
        }
    }
}