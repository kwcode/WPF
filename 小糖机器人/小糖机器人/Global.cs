using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QT
{
    public class Global
    {
        /// <summary>
        /// 缓存的Cookie
        /// </summary>
        public static string Cookie { get; set; }
        /// <summary>
        /// 安全参数
        /// </summary>
        public static string Login_Sig { get; set; }
        /// <summary>
        /// 缓存当前登录的QQ号码
        /// </summary>
        public static string QQNumber { get; set; }
        /// <summary>
        ///访问空间的时候需要的参数 Gtk 
        /// </summary>
        public static int Gtk { get; set; }

        private static int _TimeOut = 5000;
        public static int TimeOut { get { return _TimeOut; } set { _TimeOut = value; } }

        /// <summary>
        /// 随机数字
        /// </summary>
        public static double GetRandNumber()
        {
            Random rand = new Random();
            double r = rand.NextDouble();
            return r;
        }
    }
}
