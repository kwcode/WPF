using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QT
{
    public class LogsRecord
    {
        public LogsRecord()
        {

        }
        public static void WriteLog(string content)
        {
            try
            {

                //"ErrLog"
                string logPath = AppDomain.CurrentDomain.BaseDirectory + "Log/" + DateTime.Now.ToString("yyyy/MM/dd/");
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                using (StreamWriter streamWriter = new StreamWriter(logPath + "ErrLog.txt", true))
                {

                    streamWriter.WriteLine("------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "------------------------------------");
                    streamWriter.WriteLine(content);
                    streamWriter.WriteLine("\r\n");
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logtype"></param>
        /// <param name="args">:分隔 内修改为=></param>
        public static void WriteLog(string logtype, params string[] args)
        {
            string content = logtype + "->";
            if (args != null && args.Length > 0)
            {
                foreach (string item in args)
                {
                    content += item.Replace(":", "=>").Replace("：", "=>") + "\r\n";
                }
            }
            WriteLog(content);
        }

    }
}
