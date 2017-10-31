using System;
using System.IO;
using System.Threading;

namespace pdx_ymlValidator.Util
{
    /// <summary>
    /// 日志记录类
    /// </summary>
    public static class LogHelper
    {
        private static string path = Directory.GetCurrentDirectory() + "\\";

        private static void WriteAsync(string logText, string logfilename)
        {
            ThreadPool.QueueUserWorkItem(s =>
            {
                string fullpath = string.Format(path + "Logs\\{0}.txt", logfilename);
                Directory.CreateDirectory(path + "Logs");
                using (StreamWriter sw = new StreamWriter(fullpath, true))
                {
                    sw.WriteLine(logText);
                }
            });
        }

        public static void Write(string logText)
        {
            WriteAsync(logText, DateTime.Now.ToString("yyyy-MM-dd"));
        }

        public static void Write(string logText, string logfilename)
        {
            WriteAsync(logText, DateTime.Now.ToString("yyyy-MM-dd"));
        }
    }
}
