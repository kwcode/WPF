using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CsharpHttpHelper;
using System.IO;
using CsharpHttpHelper.Enum;
using System.Text.RegularExpressions;
using System.Threading;
using CsharpHttpHelper.Helper;

namespace QT
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _Cookies;
        HttpItem _Item;
        string _login_sig;
        HttpResult _Result;
        SynchronizationContext Current;
        private string _UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0";
        public MainWindow()
        {
            InitializeComponent();
            img_yzm.MouseDown += new MouseButtonEventHandler(img_yzm_MouseDown);
            txt_QQ.MouseLeave += new MouseEventHandler(txt_QQ_MouseLeave);
            this.DoMsgEvent += MainWindow_DoMsgEvent;
            Current = System.Threading.SynchronizationContext.Current;
        }



        void txt_QQ_MouseLeave(object sender, MouseEventArgs e)
        {
            SetVCode();

        }

        void img_yzm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetVCode();
        }
        private void SetVCode()
        {
            string qqNumber = txt_QQ.Text;
            //bool b = IsHaveYZM(qqNumber);
            //if (b)
            //{
            byte[] buff = GetVCode(qqNumber);
            img_yzm.Source = ConvertToImg(buff);
            //}

        }
        CsharpHttpHelper.HttpHelper _Http = new CsharpHttpHelper.HttpHelper();
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {

            Login("210819644", "tangkaiwen", txt_vCode.Text);
        }

        //是否需要传入验证码
        public bool IsHaveYZM(string qqNumber)
        {
            bool b = true;
            Random rand = new Random();
            double r = rand.NextDouble();
            string url = string.Format(@"https://ssl.ptlogin2.qq.com/check?uin={0}&appid=501004106&js_ver=10100&js_type=0&login_sig=bvBwxw0945s7IRdDzb7QhrAMlC7s0kZiTF5cm*q6ddOD5zTxSvtiebgf0AC5Jble&u1=http%3A%2F%2Fw.qq.com%2Fproxy.html&r={1}", qqNumber, r); ;

            this._Item = new HttpItem
            {
                URL = url,
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                Referer = "http://w.qq.com/",
                //UserAgent = this._UserAgent 
            };
            _Result = _Http.GetHtml(_Item);
            _Cookies = Utilities.MergerCookies(this._Cookies, Utilities.LiteCookies(this._Result.Cookie));
            this._login_sig = Utilities.GetMidStr(this._Result.Html, "g_login_sig=encodeURIComponent\\(\\\"(?<value>.*?)\\\"\\)", 1);
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
        //获取验证码 并保存Cokies
        public byte[] GetVCode(string qqNumber)
        {
            Random rand = new Random();
            double r = rand.NextDouble();
            this._Item = new HttpItem
            {
                URL = "https://ssl.captcha.qq.com/getimage?aid=501004106&r=" + r + "&uin=" + qqNumber,
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                Referer = "http://w.qq.com/",
                //UserAgent = this._UserAgent  
                ResultType = ResultType.Byte,
                Cookie = _Cookies
            };
            _Result = _Http.GetHtml(_Item);
            this._login_sig = Utilities.GetMidStr(this._Result.Html, "g_login_sig=encodeURIComponent\\(\\\"(?<value>.*?)\\\"\\)", 1);
            byte[] buff = _Result.ResultByte;
            _Cookies = Utilities.MergerCookies(this._Cookies, Utilities.LiteCookies(this._Result.Cookie));//缓存Cokkie
            return buff;
        }
        public ImageSource ConvertToImg(byte[] buff)
        {
            ImageSource img = BaseApiCommon.ImageConvertCommon.ConvertByteToBitmapImage(buff);
            return img;
        }

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
				 this._login_sig, 
					"&pt_vcode_v1=0&pt_verifysession_v1=", 
					Utilities.GetCookieValue(this._Cookies, "verifysession")
				}),
                Accept = "application/javascript, */*;q=0.8",
                Referer = "https://ui.ptlogin2.qq.com/cgi-bin/login?daid=164&target=self&style=16&mibao_css=m_webqq&appid=501004106&enable_qlogin=0&no_verifyimg=1&s_url=http%3A%2F%2Fw.qq.com%2Fproxy.html&f_url=loginerroralert&strong_login=1&login_state=10&t=20131024001",
                UserAgent = this._UserAgent,
                Cookie = this._Cookies,
                ContentType = "application/x-www-form-urlencoded"
            };
            _Result = this._Http.GetHtml(this._Item);

            this._Cookies = Utilities.MergerCookies(this._Cookies, this._Result.Cookie);
            this._ptwebqq = Utilities.GetCookieValue(this._Cookies, "ptwebqq");
            Match match = new Regex("ptuiCB\\(\\'(.*)\\',\\'(.*)\\',\\'(.*)\\',\\'(.*)\\',\\'(.*)\\',[\\s]\\'(.*)\\'\\);").Match(this._Result.Html);
            if (match.Groups[1].Value != "0")
            {
                //  throw new Exception(match.Groups[5].Value);

                MessageBox.Show(match.Groups[5].Value);
            }
            qqumber = match.Groups[6].Value;
            this._Item = new HttpItem
            {
                URL = match.Groups[3].Value,
                Accept = "text/html, application/xhtml+xml, */*",
                Cookie = this._Cookies
            };
            this._Result = this._Http.GetHtml(this._Item);
            this._Cookies = Utilities.MergerCookies(this._Cookies, this._Result.Cookie);

            this._ptwebqq = Utilities.GetCookieValue(this._Cookies, "ptwebqq");
            Channel(_ptwebqq);
            if (DoMsgEvent != null)
            {
                DoMsgEvent(this._Result.Html);
            }
            return true;
        }
        private string _ptwebqq;
        private string _clientid = (new Random().Next(100000, 1000000)).ToString();
        string _vfwebqq;
        string _psessionid;
        private void Channel(string _ptwebqq)
        {
            string postdata = "r=" + Utilities.UTF8(string.Concat(new string[]
			{
				"{\"ptwebqq\":\"", 
				this._ptwebqq, 
				"\",\"clientid\":", 
				this._clientid, 
				",\"psessionid\":\"\",\"status\":\"", 
				this.GetStatusByKey(10), 
				"\"}"
			}), true);
            this._Item = new HttpItem
            {
                URL = "http://d.web2.qq.com/channel/login2",
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                Referer = "http://d.web2.qq.com/proxy.html?v=20130916001&callback=1&id=2",
                UserAgent = this._UserAgent,
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",
                Cookie = this._Cookies,
                Method = "POST",
                Postdata = postdata
            };
            this._Result = this._Http.GetHtml(this._Item);
            this._Cookies = Utilities.MergerCookies(this._Cookies, Utilities.LiteCookies(this._Result.Cookie));
            if (!this._Result.Html.Contains("\"retcode\":0"))
            {
                MessageBox.Show(this._Result.Html);
            }
            this._vfwebqq = Utilities.GetMidStr(this._Result.Html, "vfwebqq\":\"", "\",");
            this._psessionid = Utilities.GetMidStr(this._Result.Html, "psessionid\":\"", "\",");
        }
        private string GetStatusByKey(int status)
        {
            string result;
            if (status <= 40)
            {
                if (status == 10)
                {
                    result = "online";
                    return result;
                }
                if (status == 30)
                {
                    result = "away";
                    return result;
                }
                if (status == 40)
                {
                    result = "hidden";
                    return result;
                }
            }
            else
            {
                if (status == 50)
                {
                    result = "busy";
                    return result;
                }
                if (status == 60)
                {
                    result = "callme";
                    return result;
                }
                if (status == 70)
                {
                    result = "silent";
                    return result;
                }
            }
            result = "online";
            return result;
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            StartPoll();
        }
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
                    Cookie = this._Cookies,
                    Method = "POST",
                    Postdata = string.Concat(new string[]
					{
						"r={\"ptwebqq\":\"", 
						this._ptwebqq, 
						"\",\"clientid\":", 
						this._clientid, 
						",\"psessionid\":\"", 
						this._psessionid, 
						"\",\"key\":\"\"}"
					})
                };
                this._Result = this._Http.GetHtml(this._Item);
                if (this._Result.Html != null)
                {
                    if (DoMsgEvent != null)
                    {
                        DoMsgEvent(this._Result.Html);
                    }
                    //MessageResults messageResults = JsonHelper.DeserializeToObj<MessageResults>(this._Result.Html);
                    //if (messageResults.Retcode != 102 && messageResults.Retcode != 116)
                    //{
                    //    if (messageResults != null && messageResults.MessageResult != null && this.OnReceiveMessagesHandler != null)
                    //    {
                    //        JArray jArray = new JArray();
                    //        foreach (MessageResult current in messageResults.MessageResult)
                    //        {
                    //            jArray = (current.MessageValue.Content as JArray);
                    //            if (jArray != null)
                    //            {
                    //                this.OnReceiveMessagesHandler(this._QQNumber, messageResults.Retcode, current.PollType, current.MessageValue, jArray[1].ToString());
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
        }
        void MainWindow_DoMsgEvent(object msg)
        {
            //System.Threading.Thread.CurrentThread;
            Current.Send(o =>
            {
                txt_msg.Text = msg.ToString();
            }, null);
        }
        public event DoMsgHander DoMsgEvent;
    }
    public delegate void DoMsgHander(object msg);
}
