using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TW.Client.HLUrl
{
    public abstract class EntityPropertyWindowBase : WindowBaseURLHandler
    {
        private const string URLPATH = "Entity/PropertyUI/";
        protected override string GetURLPath()
        {
            return URLPATH + this.GetType().Name;
        }

        protected override string GetKey(Dictionary<string, string> param)
        {
            string id = OnGetObjectID(param);
            if (string.IsNullOrEmpty(id)) return KeyPath; //throw new Exception("id参数不允许为空。");
            return KeyPath + "." + id;
        }
        /// <summary>
        /// 获取对象的唯一标识号，在url里面如果已经有id=xxx参数，则默认使用以url中的参数
        /// </summary>
        /// <param name="param">url中以键值对的方式出现的参数列表</param>
        /// <returns>返回界面需要加载的对象的唯一标识号</returns>
        protected virtual string OnGetObjectID(Dictionary<string, string> param)
        {
            string id = "";
            foreach (KeyValuePair<string, string> itemPair in param)
            {
                if (itemPair.Key.Equals("id", StringComparison.OrdinalIgnoreCase))
                {
                    id = itemPair.Value;
                    break;
                }
            }
            return id;
        }
    }
}
