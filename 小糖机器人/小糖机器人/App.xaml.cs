using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace QT
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Global.SysContext = System.Threading.SynchronizationContext.Current;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
           //  AppDomain.CurrentDomain.//.DispatcherUnhandledException

        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = "";
            Exception ex = e.ExceptionObject as Exception;
            string str2 = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            if (ex != null)
            {
                str = string.Format(str2 + "Application UnhandledException:{0};\n\r堆栈信息:{1}", ex.Message, ex.StackTrace);
            }
            else
            {
                str = string.Format("Application UnhandledError:{0}", e);
            }
            writeLog(str);

        }
        private static void writeLog(string str)
        {
            if (!Directory.Exists("ErrLog"))
            {
                Directory.CreateDirectory("ErrLog");
            }
            using (StreamWriter streamWriter = new StreamWriter("ErrLog\\ErrLog.txt", true))
            {
                streamWriter.WriteLine(str);
                streamWriter.WriteLine("---------------------------------------------------------");
                streamWriter.Close();
            }
        }
    }

}
