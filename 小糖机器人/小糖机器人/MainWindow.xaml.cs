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
        private string _UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0";
        public MainWindow()
        {
            InitializeComponent();
            img_yzm.MouseDown += new MouseButtonEventHandler(img_yzm_MouseDown);
            txt_QQ.MouseLeave += new MouseEventHandler(txt_QQ_MouseLeave);
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
            HttpResult result = this._Http.GetHtml(this._Item);
            return true;
        }
    }
}
