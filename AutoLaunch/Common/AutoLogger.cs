using System;
using System.Collections.Concurrent;

//using NLog;
//using NLog.Targets;
using System.IO;
using System.Threading;
using System.Timers;

namespace AutomationCommon
{
    //    public class AutoLogger
    //    {
    //        private static Logger logger = LogManager.GetCurrentClassLogger();

    //        private string _logFileName;

    //        ConcurrentQueue<LoggerMessage> _loggerQueue;
    //        Timer _LogMessagesTimer= new Timer(1000);//timer for retrieving messages from the server

    //        public AutoLogger()
    //        {
    //            _LogMessagesTimer.Elapsed += new ElapsedEventHandler(LoggerTimer_Elapsed);
    //            _LogMessagesTimer.Start();
    //            _loggerQueue = new ConcurrentQueue<LoggerMessage>();
    //        }

    //        private void LoggerTimer_Elapsed(object sender, ElapsedEventArgs e)
    //        {
    //            _LogMessagesTimer.Stop();
    //            int count = _loggerQueue.Count;
    //            LoggerMessage msg;
    //            for (int i = 0; i < count; i++)
    //            {
    //                _loggerQueue.TryDequeue(out msg);
    //                 logger.Log(msg.Level, msg.Message);
    //            }
    //            _LogMessagesTimer.Start();
    //        }
    //        public void WriteInfoLog(string message)
    //        {
    //            Singleton.Instance<ClientReportMailBox>().AddMsgToMailBox(new ClientMessage
    //                                                                          {
    //                                                                              Time = DateTime.Now,
    //                                                                              Info = message,
    //                                                                              Status = Enums.Status.Info
    //                                                                          });

    //            _loggerQueue.Enqueue(new LoggerMessage(LogLevel.Info,System.DateTime.Now +" " + message));
    //           // logger.Log(LogLevel.Info, message);
    //        }

    //        public void WriteWarningLog(string message)
    //        {
    //            Singleton.Instance<ClientReportMailBox>().AddMsgToMailBox(new ClientMessage
    //            {
    //                Time = DateTime.Now,
    //                Info = message,
    //             //   Status = Enums.Status.Warning
    //                Status = Enums.Status.Skipped
    //            });
    //          //  logger.Log(LogLevel.Warn, message);
    //            _loggerQueue.Enqueue(new LoggerMessage(LogLevel.Warn, System.DateTime.Now + " " + message));
    //        }

    //        public void WriteFailLog(string message)
    //        {
    //            Singleton.Instance<ClientReportMailBox>().AddMsgToMailBox(new ClientMessage
    //                                                                          {
    //                                                                              Time = DateTime.Now,
    //                                                                              Info = message,
    //                                                                              Status = Enums.Status.Fail
    //                                                                          });
    //          //  logger.Log(LogLevel.Error, message);
    //            _loggerQueue.Enqueue(new LoggerMessage(LogLevel.Error, System.DateTime.Now + " " + message));
    //        }

    //        public void WritePassLog(string message)
    //        {
    //            Singleton.Instance<ClientReportMailBox>().AddMsgToMailBox(new ClientMessage
    //                                                                          {
    //                                                                              Time = DateTime.Now,
    //                                                                              Info = message,
    //                                                                              Status = Enums.Status.Pass
    //                                                                          });
    //           // logger.Log(LogLevel.Info, message);
    //            _loggerQueue.Enqueue(new LoggerMessage(LogLevel.Info, System.DateTime.Now + " " + message));
    //        }

    //        public void WriteSkipLog(string message)
    //        {
    //            Singleton.Instance<ClientReportMailBox>().AddMsgToMailBox(new ClientMessage
    //                                                                          {
    //                                                                              Time = DateTime.Now,
    //                                                                              Info = message,
    //                                                                              Status = Enums.Status.Skipped
    //                                                                          });
    //        //    logger.Log(LogLevel.Info, message);
    //            _loggerQueue.Enqueue(new LoggerMessage(LogLevel.Info, System.DateTime.Now + " " + message));
    //        }

    //        public void WriteFatalLog(string message)
    //        {
    //            Singleton.Instance<ClientReportMailBox>().AddMsgToMailBox(new ClientMessage
    //                                                                          {
    //                                                                              Time = DateTime.Now,
    //                                                                              Info = message,
    //                                                                              Status = Enums.Status.Exception
    //                                                                          });
    //           // logger.Log(LogLevel.Fatal, message);
    //            _loggerQueue.Enqueue(new LoggerMessage(LogLevel.Fatal, System.DateTime.Now + " " + message));
    //        }

    //        public void SetLogFileName(string name)
    //        {
    //            FileTarget target = LogManager.Configuration.FindTargetByName("file") as FileTarget;
    //            string logfile =Path.Combine(StaticFields.LOG_PATH, name);
    //            _logFileName = logfile;
    //            target.FileName = logfile;
    //        }

    //        public string GetLogFileName()
    //        {
    //            return _logFileName;
    //        }
    //    }

    public class LoggerMessage
    {
        public AutoLogger.LogLevel Level { get; set; }

        public string Message { get; set; }

        public DateTime MsgTime { get; set; }

        public LoggerMessage(DateTime time, AutoLogger.LogLevel level, string msg)
        {
            Level = level;
            Message = msg;
            MsgTime = time;
        }
    }

    public class AutoLogger : IDisposable
    {
        private static ManualResetEvent manualReset = new ManualResetEvent(false);
        private StreamWriter sw;
        private string _logFileName;
        private ConcurrentQueue<LoggerMessage> _loggerQueue;
        private System.Timers.Timer _LogMessagesTimer = new System.Timers.Timer(1000);//timer for retrieving messages from the server
        private bool _closeLogger = false;
        private bool _loggingFinished;

        public int ActiveStepIndex { get; set; }

        public enum LogLevel
        {
            //  Error, Fatal, Warn, Info
            Pass, Fail, Skipped, NoN, Info, Exception, Warning
        }

        private void LoggerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _loggingFinished = false;
            _LogMessagesTimer.Stop();
            int count = _loggerQueue.Count;

            LoggerMessage msg;
            for (int i = 0; i < count; i++)
            {
                _loggerQueue.TryDequeue(out msg);
                Log(msg);
            }

            if (_closeLogger)
            {
                sw.Close();
                _closeLogger = false;
                _loggingFinished = true;
            }
            else
                _LogMessagesTimer.Start();
        }

        public AutoLogger()
        {
            _logFileName = string.Format(StaticFields.LOG_PATH + "\\" + "{0:dd-MM-yyyy_HH-mm-ss}_{1}", DateTime.Now, ".log");
            sw = new StreamWriter(_logFileName, true);

            _loggerQueue = new ConcurrentQueue<LoggerMessage>();
            _LogMessagesTimer.Elapsed += new ElapsedEventHandler(LoggerTimer_Elapsed);
            _LogMessagesTimer.Start();
        }

        public void SetLogFileName(string name)
        {
            if (_logFileName != name)
                sw.Close();
            _logFileName = name;
            _closeLogger = false;
            _loggingFinished = false;
            sw = new StreamWriter(_logFileName, true);
            _LogMessagesTimer.Start();
            Log(new LoggerMessage(DateTime.Now, LogLevel.Info, "Starting new Log"));
        }

        public string GetLogFileName()
        {
            return _logFileName;
        }

        public void CloseLogFile()
        {
            _closeLogger = true;
            AutoApp.Logger.WriteInfoLog("Closing Log File");
            while (!_loggingFinished)
            {
            }
        }

        public void Log(LoggerMessage message)
        {
            sw.WriteLine(string.Format("[{0}] | {1} | {2}", message.MsgTime, message.Level, message.Message));
            sw.Flush();
        }

        public void WriteInfoLog(string message, bool isMarkedColor = false)
        {
            DateTime time = DateTime.Now;
            Console.ResetColor();
            if (isMarkedColor)
                Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine(string.Format("[{0}] | {1} | {2} | Step Number: {3}", time, "INFO", message, ActiveStepIndex));
            _loggerQueue.Enqueue(new LoggerMessage(time, LogLevel.Info, message));
            Singleton.Instance<ClientReportMailBox>().AddMsgToMailBox(new ClientMessage
                                                                              {
                                                                                  Time = time,
                                                                                  Info = message,
                                                                                  Status = Enums.Status.Info
                                                                              });
        }

        public void WritePassLog(string message)
        {
            DateTime time = DateTime.Now;
            Console.ResetColor();
            Console.WriteLine(string.Format("[{0}] | {1} | {2} | Step Number: {3}", time, "INFO", message, ActiveStepIndex));
            _loggerQueue.Enqueue(new LoggerMessage(time, LogLevel.Info, message));
            Singleton.Instance<ClientReportMailBox>().AddMsgToMailBox(new ClientMessage
            {
                Time = time,
                Info = message,
                Status = Enums.Status.Pass
            });
        }

        public void WriteSkipLog(string message)
        {
            DateTime time = DateTime.Now;
            Console.ResetColor();
            Console.WriteLine(string.Format("[{0}] | {1} | {2} | Step Number: {3}", time, "INFO", message, ActiveStepIndex));
            _loggerQueue.Enqueue(new LoggerMessage(time, LogLevel.Info, message));
            Singleton.Instance<ClientReportMailBox>().AddMsgToMailBox(new ClientMessage
            {
                Time = time,
                Info = message,
                Status = Enums.Status.Skipped
            });
        }

        public void WriteFailLog(string message)
        {
            DateTime time = DateTime.Now;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("[{0}] | {1} | {2} | Step Number: {3}", time, "ERROR", message, ActiveStepIndex));
            _loggerQueue.Enqueue(new LoggerMessage(time, LogLevel.Fail, message));
            Singleton.Instance<ClientReportMailBox>().AddMsgToMailBox(new ClientMessage
            {
                Time = time,
                Info = message,
                Status = Enums.Status.Fail
            });
        }

        public void WriteFatalLog(string message)
        {
            DateTime time = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("[{0}] | {1} | {2} | Step Number: {3}", time, "FATAL", message, ActiveStepIndex));
            _loggerQueue.Enqueue(new LoggerMessage(time, LogLevel.Exception, message));
            Singleton.Instance<ClientReportMailBox>().AddMsgToMailBox(new ClientMessage
            {
                Time = time,
                Info = message,
                Status = Enums.Status.Exception
            });
        }

        public void WriteWarningLog(string message)
        {
            DateTime time = DateTime.Now;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("[{0}] | {1} | {2} | Step Number: {3}", time, "WARNING", message, ActiveStepIndex));
            _loggerQueue.Enqueue(new LoggerMessage(time, LogLevel.Warning, message));
            Singleton.Instance<ClientReportMailBox>().AddMsgToMailBox(new ClientMessage
            {
                Time = time,
                Info = message,
                Status = Enums.Status.Warning
            });
        }

        #region IDisposable Members

        public void Dispose()
        {
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }

        #endregion IDisposable Members
    }
}