using CsharpHttpHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QT
{
    public class ChatAPI
    {
        public string AskXiaodoub(string content)
        {
            HttpItem _Item;
            HttpResult _Result;
            CsharpHttpHelper.HttpHelper _Http = new CsharpHttpHelper.HttpHelper();
            _Item = new HttpItem
            {
                URL = "http://www.xiaodoubi.com/bot/api.php?chat=" + content
            };
            _Result = _Http.GetHtml(_Item);
            string result;
            if (!string.IsNullOrEmpty(_Result.Html))
            {
                result = _Result.Html.Replace("www.xiaodoubi.com", "^_^").Replace("小逗比网页版", "我是小糖机器人");
            }
            else
            {

                result = "尼玛、、、说的什么东东？听不懂啊！";
            }
            return result;
        }
        public static YunTuResult AskYunTu(string content)
        {
            YunTuResult result = new YunTuResult();
            HttpItem _Item;
            HttpResult _Result;
            CsharpHttpHelper.HttpHelper _Http = new CsharpHttpHelper.HttpHelper();
            String APIKEY = "6bc94b45b9fd2ec68487742f8e443e57";
            String url = "http://www.tuling123.com/openapi/api?key=" + APIKEY + "&info=" + content;
            _Item = new HttpItem
           {
               URL = url
           };
            _Result = _Http.GetHtml(_Item);

            if (!string.IsNullOrEmpty(_Result.Html))
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<YunTuResult>(_Result.Html);
            }
            else
            {
                result = null;
            }
            return result;
        }


    }
    public class YunTuResult
    {
        //{"code":100000,"text":"很高兴和你聊天"} 
        public int code { get; set; }
        public string text { get; set; }
    }

}
