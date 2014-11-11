using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using CsharpHttpHelper.Helper;
namespace QT
{
    /// <summary>
    /// QQ
    /// </summary>
    public class QQHelper
    {

        HttpItem _Item;
        HttpResult _Result;
        private string _UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0";
        CsharpHttpHelper.HttpHelper _Http = new CsharpHttpHelper.HttpHelper();

        public QQHelper()
        {
            DoLogin_Sig();
        }
        /// <summary>
        /// 获取安全参数 和Cookie 做缓存
        /// http://w.qq.com/页面中
        /// iframe页面中获得 注意编码 &
        /// 方式 在新的窗口打开 复制地址栏
        /// </summary>
        /// <returns></returns>
        public void DoLogin_Sig()
        {
            try
            {
                this._Item = new HttpItem
                {
                    URL = "https://ui.ptlogin2.qq.com/cgi-bin/login?daid=164&target=self&style=16&mibao_css=m_webqq&appid=501004106&enable_qlogin=0&no_verifyimg=1&s_url=http%3A%2F%2Fw.qq.com%2Fproxy.html&f_url=loginerroralert&strong_login=1&login_state=10&t=20131024001",
                    Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                    Referer = "http://w.qq.com/",
                    UserAgent = this._UserAgent,
                    Timeout = Global.TimeOut
                };

                this._Result = this._Http.GetHtml(this._Item);
                //   Global.Cookie = Utilities.LiteCookies(this._Result.Cookie);
                Global.Login_Sig = Utilities.GetMidStr(this._Result.Html, "g_login_sig=encodeURIComponent\\(\\\"(?<value>.*?)\\\"\\)", 1);

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        ///   是否需要传入验证码
        /// </summary>
        /// <param name="qqNumber"></param>
        /// <returns></returns>
        public bool IsHaveYZM(string qqNumber)
        {
            bool b = true;
            string url = string.Format(@"https://ssl.ptlogin2.qq.com/check?uin={0}&appid=501004106&js_ver=10100&js_type=0&login_sig=bvBwxw0945s7IRdDzb7QhrAMlC7s0kZiTF5cm*q6ddOD5zTxSvtiebgf0AC5Jble&u1=http%3A%2F%2Fw.qq.com%2Fproxy.html&r={1}", qqNumber, Global.GetRandNumber()); ;

            this._Item = new HttpItem
            {
                URL = url,
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                Referer = "http://w.qq.com/",
                UserAgent = this._UserAgent
            };
            _Result = _Http.GetHtml(_Item);
            //     Global.Cookie = Utilities.MergerCookies(Global.Cookie, Utilities.LiteCookies(this._Result.Cookie));
            string code = _Result.Html;
            if (code.Contains("ptui_checkVC('0','"))     //不需要手动输入
            {
                code = code.Replace("ptui_checkVC('0','", "").Replace("'", "").Replace(")", "").Replace(";", "").Substring(0, 4);
                b = false;
            }
            else if (code.Contains("ptui_checkVC('1',"))
            {
                b = true;
            }
            return b;
        }

        /// <summary>
        ///  //获取验证码 并保存Cokies
        ///  二进值
        /// </summary>
        /// <param name="qqNumber"></param>
        /// <returns></returns>
        public byte[] GetByteVCode(string qqNumber)
        {
            this._Item = new HttpItem
            {
                URL = "https://ssl.captcha.qq.com/getimage?aid=501004106&r=" + Global.GetRandNumber() + "&uin=" + qqNumber,
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                UserAgent = this._UserAgent,
                Cookie = Global.Cookie,
                ResultType = ResultType.Byte,
                ContentType = "application/x-www-form-urlencoded"

            };
            _Result = _Http.GetHtml(_Item);
            //   System.Drawing.Image img = _Http.GetImage(_Item);
            Global.Cookie = Utilities.MergerCookies(Global.Cookie, Utilities.LiteCookies(this._Result.Cookie));//缓存Cokkie
            byte[] buff = _Result.ResultByte;
            return buff;
        }


        /// <summary>
        ///  //获取验证码 并保存Cokies
        ///  二进值
        /// </summary>
        /// <param name="qqNumber"></param>
        /// <returns></returns>
        public System.Drawing.Image GetImageVCode(string qqNumber)
        {
            this._Item = new HttpItem
            {
                URL = "https://ssl.captcha.qq.com/getimage?aid=501004106&r=" + Global.GetRandNumber() + "&uin=" + qqNumber,
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                UserAgent = this._UserAgent,
                Cookie = Global.Cookie,
                ResultType = ResultType.Byte,
                ContentType = "application/x-www-form-urlencoded"

            };
            System.Drawing.Image img = _Http.GetImage(_Item);
            return img;
        }
        /// <summary>
        /// QQ登录
        /// </summary>
        /// <param name="qqumber"></param>
        /// <param name="password"></param>
        /// <param name="vcode"></param>
        /// <returns></returns>
        public bool Login(string qqumber, string password, string vcode)
        {
            this._Item = new HttpItem
            {
                URL = string.Concat(new object[]
				{
					"https://ssl.ptlogin2.qq.com/login?u=", 
					qqumber, 
					"&p=", 
					QQMd5.Encrypt(qqumber, password, vcode), 
					"&verifycode=", 
					vcode, 
					"&webqq_type=", 
					10, 
					"&remember_uin=1&login2qq=1&aid=501004106&u1=http%3A%2F%2Fw.qq.com%2Fproxy.html%3Flogin2qq%3D1%26webqq_type%3D10&h=1&ptredirect=0&ptlang=2052&daid=164&from_ui=1&pttype=1&dumy=&fp=loginerroralert&action=0-51-115221&mibao_css=m_webqq&t=3&g=1&js_type=0&js_ver=10099&login_sig=", 
				    Global.Login_Sig, 
					"&pt_vcode_v1=0&pt_verifysession_v1=", 
					Utilities.GetCookieValue(Global.Cookie, "verifysession")
				}),
                Accept = "application/javascript, */*;q=0.8",
                Referer = "https://ui.ptlogin2.qq.com/cgi-bin/login?daid=164&target=self&style=16&mibao_css=m_webqq&appid=501004106&enable_qlogin=0&no_verifyimg=1&s_url=http%3A%2F%2Fw.qq.com%2Fproxy.html&f_url=loginerroralert&strong_login=1&login_state=10&t=20131024001",
                UserAgent = this._UserAgent,
                Cookie = Global.Cookie,
                ContentType = "application/x-www-form-urlencoded"
            };
            HttpResult result = this._Http.GetHtml(this._Item);
            Global.Cookie = Utilities.MergerCookies(Global.Cookie, Utilities.LiteCookies(this._Result.Cookie));
            return true;
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        public void StartPoll()
        {
            Thread thread = new Thread(new ThreadStart(this.Poll));
            thread.Start();
            thread.IsBackground = true;
        }
        private void Poll()
        {
            while (true)
            {
                this._Item = new HttpItem
                {
                    URL = "http://d.web2.qq.com/channel/poll2",
                    Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                    UserAgent = this._UserAgent,
                    ContentType = "application/x-www-form-urlencoded; charset=UTF-8",
                    Referer = "http://d.web2.qq.com/proxy.html?v=20130916001&callback=1&id=2",
                    Cookie = Global.Cookie,
                    Method = "POST",
                    Postdata = string.Concat(new string[]
					{
						"r={\"ptwebqq\":\"", 
                        //this._ptwebqq, 
						"\",\"clientid\":", 
                        //this._clientid, 
						",\"psessionid\":\"", 
                        //this._psessionid, 
						"\",\"key\":\"\"}"
					})
                };
                this._Result = this._Http.GetHtml(this._Item);
                if (this._Result.Html != null)
                {
                    MessageResults messageResults = (MessageResults)JsonHelper.JsonToObject<MessageResults>(this._Result.Html);
                    if (messageResults.Retcode != 102 && messageResults.Retcode != 116)
                    {
                        if (messageResults != null && messageResults.MessageResult != null && this.OnReceiveMessagesHandler != null)
                        {
                            JArray jArray = new JArray();
                            foreach (MessageResult current in messageResults.MessageResult)
                            {
                                jArray = (current.MessageValue.Content as JArray);
                                if (jArray != null)
                                {
                                    this.OnReceiveMessagesHandler(Global.QQNumber, messageResults.Retcode, current.PollType, current.MessageValue, jArray[1].ToString());
                                }
                            }
                        }
                    }
                }
            }
        }

        public event ReceiveMessages OnReceiveMessagesHandler;

    }
    public delegate void ReceiveMessages(string qqNumber, int recode, string pollType, MessageValue msg, string content);
}
