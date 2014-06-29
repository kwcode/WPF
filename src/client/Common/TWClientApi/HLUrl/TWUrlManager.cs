using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace TW
{
    public class TWUrlManager
    {
        private static object _lock = new object();
        private static Dictionary<string, ITWUrlHandler> _lstHandler = new Dictionary<string, ITWUrlHandler>();
        /// <summary>
        /// 注册处理接口
        /// </summary>
        /// <param name="handler"></param>
        public static void RegHandler(ITWUrlHandler handler)
        {
            if (handler == null)
                return;
            string pt = handler.Path;
            if (string.IsNullOrEmpty(pt))
                return;
            if (!_lstHandler.ContainsKey(pt))
                _lstHandler.Add(pt, handler);
        }
        /// <summary>
        /// 打开Url
        /// </summary>
        /// tw://localhost/Entity/ManagementUI/UserManangerHandler
        /// <param name="url"></param>
        public static void Open(string url)
        {
            string host, path;

            Dictionary<string, string> param;
            if (!IsHLProtocol(url, out host, out path, out param))
            {
                OpenDefault(url);
            }
            else
            {
                ITWUrlHandler handler = GetUrlHandler(path);
                if (handler != null) handler.Open(param);
            }
        }

        [DllImport("shell32.dll")]
        extern static IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        private static void OpenDefault(object url)
        {
            ShellExecute(IntPtr.Zero, "open", (string)url, null, null, (int)ShowWindowCommands.SW_SHOW);
        }
        /// <summary>
        /// 分解url
        /// </summary>
        /// <param name="url">输入参数：完整的url</param>
        /// <param name="host">输出参数：如果是HL://协议，解析出的主机，不带协议和url分隔符</param>
        /// <param name="path">输出参数：如果是HL://协议，解析出不含参数的url的路径，也不带协议字符串和主机</param>
        /// <param name="param">输出参数：如果是HL://协议，解析出url中的参数键值对</param>
        /// <returns></returns>
        private static bool IsHLProtocol(string url, out string host, out string path, out Dictionary<string, string> param)
        {
            host = "";
            path = "";
            param = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(url))
                return false;
            int i = url.IndexOf("://");
            if (i < 0)
                return false;
            if (url.Substring(0, i).Trim().ToLower() != "tw")
                return false;
            string s = url.Substring(i + 3);
            i = s.IndexOf("/");
            if (i < 0)
            {
                host = s;
                return true;
            }
            host = s.Substring(0, i);
            s = s.Substring(i + 1);
            i = s.IndexOf("?");
            if (i < 0)
            {
                path = s;
                return true;
            }
            path = s.Substring(0, i);
            s = s.Substring(i + 1);
            string[] prs = s.Split('&');
            foreach (string pr in prs)
            {
                string k, v;
                i = pr.IndexOf("=");
                if (i >= 0)
                {
                    k = pr.Substring(0, i);
                    v = pr.Substring(i + 1);
                }
                else
                {
                    k = pr;
                    v = "";
                }
                param.Add(k.ToLower(), v);
            }
            return true;
        }

        private static ITWUrlHandler GetUrlHandler(string path)
        {
            ITWUrlHandler handler = null;
            if (!_lstHandler.TryGetValue(path, out handler))
            {
                Assembly assembly = null;
                lock (_lock)
                {
                    //assembly = GetLoadedAssemblyByHrlPath(path);
                    //if (assembly == null) assembly = LoadHandler(path);
                }
                if (assembly != null)
                {
                    // RegistHandlers(assembly);
                    _lstHandler.TryGetValue(path, out handler);
                }
            }
            return handler;
        }


    }
    enum ShowWindowCommands : int
    {
        SW_HIDE = 0,
        SW_SHOWNORMAL = 1,
        SW_NORMAL = 1,
        SW_SHOWMINIMIZED = 2,
        SW_SHOWMAXIMIZED = 3,
        SW_MAXIMIZE = 3,
        SW_SHOWNOACTIVATE = 4,
        SW_SHOW = 5,
        SW_MINIMIZE = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA = 8,
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10,
        SW_MAX = 10
    }

}
