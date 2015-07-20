using AutomationCommon;

namespace AutomationServer
{
    public class FlowHandler
    {
        //public TestEntity ActiveScriptEntity1 { get; set; }
        //public int LableIndex1 { get; set; }
        //public string LableName { get; set; }

        //public bool ActivateGoTo1()
        //{
        //    return LableIndex1 != -1;
        //}

        //private FlowHandler()
        //{
        //    LableIndex1 = -1;
        //}

        //public bool FindLableIndex1(string lable)
        //{
        //    LableName = lable;
        //    StepEntity s;
        //    for (int i = 0; i < ActiveScriptEntity.Script.Entities.Count; i++)
        //    {
        //        s = ActiveScriptEntity.Script.Entities[i];
        //        if (s.Action.TypeId == Enums.ActionTypeId.Lable)
        //        {
        //            if (((LabelAction)s.Action)._type == LabelAction.ActionType.Create)
        //            {
        //                if (((LabelAction)s.Action)._actionData.LableName == lable)
        //                {
        //                    LableIndex1 = i;
        //                    return true;
        //                }
        //            }

        //        }
        //    }
        //    return false;

        //}

        public void FindLableIndex(string lable, ScriptObj scriptObj)
        {
            StepEntity s;
            for (int i = 0; i < scriptObj.Entities.Count; i++)
            {
                s = scriptObj.Entities[i];
                if (s.Action.TypeId == Enums.ActionTypeId.Lable)
                {
                    if (((LabelAction)s.Action)._type == LabelAction.ActionType.Create)
                    {
                        if (((LabelAction)s.Action)._actionData.LableName == lable)
                        {
                            scriptObj.NextepIndex = i;
                            return;
                        }
                    }
                }
            }

            AutoApp.Logger.WriteFailLog(string.Format("Label {0} was not found "));
            scriptObj.NextepIndex = -1;
        }
    }
}