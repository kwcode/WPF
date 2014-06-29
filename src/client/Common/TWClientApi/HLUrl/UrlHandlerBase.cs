using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TW;
using System.Windows; 

namespace TW.Client
{
    public abstract class UrlHandlerBase : ITWUrlHandler
    {
        public string Path
        {
            get { return GetURLPath(); }
        }
        /// <summary>
        /// 获取本类处理的URL路径，用于注册url处理器时使用
        /// </summary>
        /// <returns></returns>
        protected abstract string GetURLPath();
        protected abstract string GetKey(Dictionary<string, string> param);
        /// <summary>
        /// 确定本类的UI界面是在主窗口的TabControl里面打开还是在新窗口中打开
        /// </summary>
        /// <returns></returns>
        protected abstract bool OpenInTabControl();
        public void Open(Dictionary<string, string> param)
        {

            string id = GetKey(param);
            bool inTabCtrl = OpenInTabControl();
            UIElement ui = CreateUI(param);
            TWWindowManager.Instance.Add(id, ui, inTabCtrl);
            LoadData(ui, param);
        }
        /// <summary>
        /// 加载数据，需要在执行界面的初始化之前加载数据的，在此覆盖本方法
        /// </summary>
        protected virtual void LoadData(System.Windows.UIElement ui, Dictionary<string, string> param)
        {
        }
        protected abstract UIElement CreateUI(Dictionary<string, string> param);
    }
    /// <summary>
    /// 以TabControl标签的方式打开的UrlHandler的基类
    /// </summary>
    public abstract class TabBaseUrlHandler : UrlHandlerBase
    {
        /// <summary>
        /// 标示是在TabControl中打开界面
        /// </summary>
        /// <returns></returns>
        protected override sealed bool OpenInTabControl()
        {
            return true;
        }
    }

    /// <summary>
    /// 以窗口的方式打开的urlHandler的基类
    /// </summary>
    public abstract class WindowBaseURLHandler : UrlHandlerBase
    {
        /// <summary>
        /// 标示不是在TabControl中打开界面
        /// </summary>
        /// <returns></returns>
        protected override sealed bool OpenInTabControl()
        {
            return false;
        }

        /// <summary>
        /// 获取本类窗口的键值的基础部分，该部分有类的全名组成，键值用于在管理的窗口中作为唯一标示
        /// </summary>
        public string KeyPath
        {
            get { return this.GetType().FullName; }
        }
    }

}
