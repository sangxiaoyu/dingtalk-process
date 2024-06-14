using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process.Utils
{
    public sealed class Log
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Trace(string strMsg)
        {
            _logger.Trace(strMsg);
        }

        //有很多重载，可以看提示
        public static void Trace(Exception e, string strMsg)
        {
            _logger.Trace(e, strMsg);
        }

        public static void Debug(string strMsg)
        {
            _logger.Debug(strMsg);
        }

        public static void Info(string strMsg)
        {
            _logger.Info(strMsg);
        }

        public static void Warn(string strMsg)
        {
            _logger.Warn(strMsg);
        }

        public static void Warn(Exception e)
        {
            _logger.Warn(e);
        }

        public static void Warn(Exception e, string strMsg)
        {
            _logger.Warn(e, strMsg);
        }

        public static void Error(string strMsg)
        {
            _logger.Error(strMsg);
        }

        public static void Error(Exception e, string strMsg)
        {
            _logger.Error(strMsg);
        }

        public static void Error(Exception e)
        {
            _logger.Error(e);
        }

        public static void Fatal(string strMsg)
        {
            _logger.Fatal(strMsg);
        }

    }
}
