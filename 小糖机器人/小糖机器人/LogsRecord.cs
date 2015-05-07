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
        /// <summary>
        /// 记录日志
        /// 统一保存在/Log/{类型}/日期/Log.txt
        /// </summary>
        /// <param name="content">日志内容</param>
        /// <param name="module">日志类型 默认Other</param>
        public static void WriteLog(string content, RecordModule module = RecordModule.Other)
        {
            try
            {
                string logPath = AppDomain.CurrentDomain.BaseDirectory + "Log/" + DateTime.Now.ToString("yyyy/MM/dd/") + module + "/";
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                using (StreamWriter streamWriter = new StreamWriter(logPath + "Log.txt", true))
                {
                    streamWriter.WriteLine("------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "------------------------------------");
                    streamWriter.WriteLine(content);
                    streamWriter.WriteLine("\r\n");
                    streamWriter.Close();
                }
            }
            catch { }
        }

        /// <summary>
        /// 按照要求记录日志
        /// </summary>
        /// <param name="logtype">对应的键值</param>
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

    public enum RecordModule
    {
        Error = 0,
        Msg = 1,
        Bug = 3,
        Other = 99
    }
}
