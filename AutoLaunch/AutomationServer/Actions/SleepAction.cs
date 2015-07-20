using System;
using System.Threading;
using System.Timers;
using AutomationCommon;

namespace AutomationServer
{
    public class SleepAction : ActionBase
    {
        private string _sec;
        private System.Timers.Timer _timer = new System.Timers.Timer();
        private ManualResetEvent _manualResetEvent = new System.Threading.ManualResetEvent(false);
        private DateTime _startActionTime = new DateTime();
        private DateTime _endActionTime = new DateTime();

        public SleepAction(string value = "10")
            : base(Enums.ActionTypeId.Sleep)
        {
            Details.Add(value);
        }

        public int GetRemainingDuration()
        {
            int duration = 0;
            try
            {
                DateTime currentTime = DateTime.Now;
                int res = DateTime.Compare(_endActionTime, currentTime);
                if (res <= 0)
                    return 0;

                //if t1 is less than t2 then result is Less than zero

                //if t1 equals t2 then result is Zero

                //if t1 is greater than t2 then result isGreater zero

                duration = (int)_endActionTime.Subtract(currentTime).TotalSeconds;
            }
            catch { }

            return duration;
        }

        //private void Sleep()
        //{
        //    try
        //    {
        //        string delay = Singleton.Instance<SavedData>().GetVariableData(_sec);
        //        AutoApp.Logger.WriteInfoLog(string.Format("Starting Sleep for {0} Sec ", delay));
        //        var msec = decimal.Parse(delay) * 1000;
        //        new System.Threading.ManualResetEvent(false).WaitOne((int)msec);
        //        AutoApp.Logger.WritePassLog("Finished Sleep Action");
        //        ActionStatus = Enums.Status.Pass;
        //    }
        //    catch(Exception ex)
        //    {
        //        AutoApp.Logger.WriteFatalLog(ex.Message);
        //    }

        //    HasFinished = true;
        //}

        public void OnTimer(Object source, ElapsedEventArgs e)
        {
            _timer.Stop();
            _manualResetEvent.Set();
        }

        public override void Execute()
        {
            // Sleep();
            try
            {
                _endActionTime = new DateTime();
                _startActionTime = DateTime.Now;
                _manualResetEvent = new System.Threading.ManualResetEvent(false);
                _timer.Elapsed += new ElapsedEventHandler(OnTimer);
                string delay = Singleton.Instance<SavedData>().GetVariableData(_sec);

                try
                {
                    _endActionTime = _startActionTime.AddSeconds(double.Parse(delay));
                }
                catch
                {
                }

                AutoApp.Logger.WriteInfoLog(string.Format("Starting Sleep for {0} Sec , Action will be finished at {1}", delay, _endActionTime.ToString("T")));
                var msec = double.Parse(delay) * 1000;
                if (msec != 0)
                {
                    _timer.Interval = msec;
                    _timer.Start();
                    _manualResetEvent.WaitOne();
                }
                AutoApp.Logger.WritePassLog("Finished Sleep Action");
                ActionStatus = Enums.Status.Pass;
            }
            catch (Exception ex)
            {
                AutoApp.Logger.WriteFatalLog(ex.Message);
            }

            HasFinished = true;
        }

        public override void Construct()
        {
            _sec = Details[0];
        }

        public void StopSleep()
        {
            AutoApp.Logger.WritePassLog("Stopping sleep");
            _timer.Stop();
            _manualResetEvent.Set();
        }
    }
}