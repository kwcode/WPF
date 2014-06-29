using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TW.Client.HLUrl
{
    public abstract class EntityManagementWindowBase : TabBaseUrlHandler
    {
        private const string URLPATH = "Entity/ManagementUI/";
        protected override sealed string GetURLPath()
        {
            return URLPATH + this.GetType().Name;
        }
        /// <summary>
        /// 获取窗口的键值，本处以类的全面作为键值，按照标准模式编程的情况下，可以确保只有一个实例被创建
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override sealed string GetKey(Dictionary<string, string> param)
        {
            return this.GetType().FullName;
        }
    }
}
