using System;
using System.IO;

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
            string fullpath = string.Format(path + "Logs\\{0}.txt", logfilename);
            Directory.CreateDirectory(path + "Logs");
            using (StreamWriter sw = new StreamWriter(fullpath, true))
            {
                sw.WriteLine(logText);
            }
        }

        public static void Write(string logText)
        {
            WriteAsync(logText, DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        public static void Write(string logText, string logfilename)
        {
            WriteAsync(logText, logfilename);
        }
    }
}
