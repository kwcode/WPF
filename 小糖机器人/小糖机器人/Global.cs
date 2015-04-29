using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

namespace QT
{
    public class Global
    {
        /// <summary>
        ///   UI界面方法使用的主程序的UI主线程同步器
        /// </summary>
        public static SynchronizationContext SysContext { get { return _SysContext; } set { _SysContext = value; } }
        private static SynchronizationContext _SysContext;
        /// <summary>
        /// 缓存的Cookie
        /// </summary>
        public static string Cookie { get; set; }
        public static CookieCollection CookieCollection = new CookieCollection();
        /// <summary>
        /// 安全参数
        /// </summary>
        public static string Login_Sig { get; set; }
        /// <summary>
        /// 缓存当前登录的QQ号码
        /// </summary>
        public static string QQNickName { get; set; }
        /// <summary>
        ///访问空间的时候需要的参数 Gtk 
        /// </summary>
        public static int Gtk { get; set; }

        private static int _TimeOut = 5000;
        public static int TimeOut { get { return _TimeOut; } set { _TimeOut = value; } }
        /// <summary>
        ///登录后 Cookie里面取的
        /// </summary>
        public static string PtWebQQ { get; set; }
        public static string Hash { get; set; }
        /// <summary>
        /// 客户端ID
        /// </summary>
        public static string ClientID { get { return _ClientID; } set { _ClientID = value; } }
        private static string _ClientID = "53999199"; //GetRandNumber(100000000, 999999999).ToString();
        public static string Uin { get; set; }
        public static string VfWebQQ { get; set; }
        public static string PsessionID { get; set; }
        public static int Status = 10;
        public static string VerifySession { get; set; }
        /// <summary>
        /// 随机数字
        /// </summary>
        public static double GetRandNumber()
        {
            Random rand = new Random();
            double r = rand.NextDouble();
            return r;
        }

        /// <summary>
        /// 随机数字
        /// </summary>
        public static double GetRandNumber(int minValue, int maxValue)
        {
            Random rand = new Random();
            double r = rand.Next(minValue, maxValue);
            return r;
        }
        #region 缓存数据
        public static UserResults CurrentQQ { get; set; }
        #endregion

    }
}
