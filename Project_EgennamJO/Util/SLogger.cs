using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_EgennamJO.Util
{
    public class SLogger
    {
        public enum LogType
        {
            Info,
            Error,
            Debug
        }
        private static readonly ILog _log = LogManager.GetLogger(nameof(_log));

        public static event Action<string> LogUpdated;

        static SLogger()
        {
        }
        public static void Write(string message, LogType type = LogType.Info)
        {
            //로그 파일에는 자동으로 시간이 기록되므로, 타입과 메세지만 기록
            string logMessage = $"[{type}] {message}";

            // 파일 로그 기록
            switch (type)
            {
                case LogType.Error:
                    _log.Error(logMessage);
                    break;
                case LogType.Debug:
                    _log.Debug(logMessage);
                    break;
                default:
                    _log.Info(logMessage);
                    break;
            }
            logMessage = $"[{DateTime.Now:MM-dd HH:mm:ss}] {logMessage}";

            LogUpdated?.Invoke(logMessage);

        }
    }
}