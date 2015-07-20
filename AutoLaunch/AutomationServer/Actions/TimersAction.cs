using System;
using System.Diagnostics;

using AutomationCommon;

namespace AutomationServer
{
    public class TimersAction : ActionBase
    {
        private TimersActionData _actionData;
        private TimersActionType _type;

        public enum TimersActionType
        {
            Create, Delete, Start, Stop, GetValue, GetValueSec, Reset
        }

        public TimersAction()
            : base(Enums.ActionTypeId.Timers)
        {
        }

        public TimersAction(TimersActionType type, TimersActionData actionData)
            : base(Enums.ActionTypeId.Timers)
        {
            _type = type;
            _actionData = actionData;
            Details.Add(type.ToString());
            Details.Add(_actionData.TimerName);
            Details.Add(_actionData.TargetVar);
        }

        public override void Execute()
        {
            long elapsedSec;
            if (_type != TimersActionType.Create)
            {
                if (!Singleton.Instance<SavedData>().TimerList.ContainsKey(_actionData.TimerName))
                {
                    AutoApp.Logger.WriteFailLog(string.Format("Timer {0} does not exist", _actionData.TimerName));
                    return;
                }
            }

            switch (_type)
            {
                case TimersActionType.Create:
                    AutoApp.Logger.WriteInfoLog(string.Format("Creating Timer {0}", _actionData.TimerName));
                    if (Singleton.Instance<SavedData>().TimerList.ContainsKey(_actionData.TimerName))
                    {
                        AutoApp.Logger.WriteWarningLog(
                            string.Format("Timer {0} already exist ,stopping & resetting timer value",
                                          _actionData.TimerName));
                        Singleton.Instance<SavedData>().TimerList[_actionData.TimerName].Stop();
                        Singleton.Instance<SavedData>().TimerList[_actionData.TimerName].Reset();
                    }
                    else
                    {
                        Singleton.Instance<SavedData>().TimerList.Add(_actionData.TimerName, new Stopwatch());
                    }
                    AutoApp.Logger.WritePassLog(string.Format("Timer {0} created", _actionData.TimerName));
                    break;

                case TimersActionType.Reset:
                    Singleton.Instance<SavedData>().TimerList[_actionData.TimerName].Reset();
                    AutoApp.Logger.WritePassLog(string.Format("Timer {0} Restarted", _actionData.TimerName));
                    break;

                case TimersActionType.Stop:
                    Singleton.Instance<SavedData>().TimerList[_actionData.TimerName].Stop();
                    AutoApp.Logger.WritePassLog(string.Format("Timer {0} Stopped", _actionData.TimerName));
                    break;

                case TimersActionType.Start:
                    Singleton.Instance<SavedData>().TimerList[_actionData.TimerName].Start();
                    AutoApp.Logger.WritePassLog(string.Format("Timer {0} started", _actionData.TimerName));
                    break;

                case TimersActionType.Delete:
                    Singleton.Instance<SavedData>().TimerList.Remove(_actionData.TimerName);
                    AutoApp.Logger.WritePassLog(string.Format("Timer {0} Deleted", _actionData.TimerName));
                    break;

                case TimersActionType.GetValue:
                    elapsedSec = Singleton.Instance<SavedData>().TimerList[_actionData.TimerName].ElapsedMilliseconds;
                    if (Singleton.Instance<SavedData>().Variables.ContainsKey(_actionData.TargetVar))
                    {
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(elapsedSec.ToString());
                        AutoApp.Logger.WritePassLog(string.Format("Timer {0} value saved to {1} Variable",
                                                                  _actionData.TimerName, _actionData.TargetVar));
                    }
                    else
                    {
                        AutoApp.Logger.WriteFailLog(string.Format(
                            "Variable {0} does not exist , can't save timer value", _actionData.TargetVar));
                        return;
                    }
                    break;

                case TimersActionType.GetValueSec:
                    elapsedSec = (Singleton.Instance<SavedData>().TimerList[_actionData.TimerName].ElapsedMilliseconds) / 1000;
                    if (Singleton.Instance<SavedData>().Variables.ContainsKey(_actionData.TargetVar))
                    {
                        Singleton.Instance<SavedData>().Variables[_actionData.TargetVar].SetValue(elapsedSec.ToString());
                        AutoApp.Logger.WritePassLog(string.Format("Timer {0} value saved to {1} Variable",
                                                                  _actionData.TimerName, _actionData.TargetVar));
                    }
                    else
                    {
                        AutoApp.Logger.WriteFailLog(string.Format(
                            "Variable {0} does not exist , can't save timer value", _actionData.TargetVar));
                        return;
                    }
                    break;
            }

            ActionStatus = Enums.Status.Pass;
        }

        public override void Construct()
        {
            _type = (TimersActionType)Enum.Parse(typeof(TimersActionType), Details[0]);
            _actionData = new TimersActionData() { TimerName = Details[1], TargetVar = Details[2] };
        }

        public struct TimersActionData
        {
            public string TimerName { get; set; } //1

            public string TargetVar { get; set; } //2
        }
    }
}