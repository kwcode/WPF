using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TW
{
    public interface ITWUrlHandler
    {

        /// <summary>
        /// 路径
        /// </summary>
        string Path { get; }

        /// 打开Url
        /// </summary>
        /// <param name="param">url</param>
        void Open(Dictionary<string, string> param);
    }
}
