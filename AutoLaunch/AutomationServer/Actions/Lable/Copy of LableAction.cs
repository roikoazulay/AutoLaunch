using System;

using AutomationCommon;

namespace AutomationServer
{
    public class LableAction1 : ActionBase
    {
        public string LableName { get; set; }

        public LableType Type;

        public enum LableType
        {
            Create, GoTo
        }

        public LableAction1()
            : base(Enums.ActionTypeId.Lable)
        {
        }

        public LableAction1(LableType type, string lableName)
            : base(Enums.ActionTypeId.Lable)
        {
            LableName = lableName;
            Type = type;

            Details.Add(type.ToString());
            Details.Add(lableName);
        }

        public override void Execute()
        {
            switch (Type)
            {
                case LableType.Create: //nothing to do here ..it's gust a referance
                    AutoApp.Logger.WritePassLog(string.Format("Lable {0} was createed", LableName));
                    ActionStatus = Enums.Status.Pass;
                    break;

                case LableType.GoTo:
                    AutoApp.Logger.WriteInfoLog(string.Format("Searching for Lable {0}", LableName));

                    //if (Singleton.Instance<ActionHandler>().Flowhandler.FindLableIndex(LableName))
                    //{
                    //    AutoApp.Logger.WritePassLog(string.Format("Found Lable index {0}",
                    //                                              Singleton.Instance<ActionHandler>().Flowhandler.LableIndex));
                    //    ActionStatus = Enums.Status.Pass;
                    //}
                    //else
                    //{
                    //    AutoApp.Logger.WriteFailLog(string.Format("Lable {0} was not found on script {1}", LableName,
                    //                                              Singleton.Instance<ActionHandler>().Flowhandler.ActiveScriptEntity.
                    //                                                  ShortName()));
                    //    ActionStatus = Enums.Status.Fail;
                    //}
                    break;
            }
        }

        public override void Construct()
        {
            Type = (LableType)Enum.Parse(typeof(LableType), Details[0]);
            LableName = Details[1];
        }
    }
}