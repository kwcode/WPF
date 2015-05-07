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
        QQHelper QQ = new QQHelper();
        public string _QQNumber { get; set; }
        public MainWindow()
        {
            InitializeComponent(); 
            this.Loaded += MainWindow_Loaded;

        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            img_yzm.MouseDown += new MouseButtonEventHandler(img_yzm_MouseDown);
            txt_QQ.MouseLeave += new MouseEventHandler(txt_QQ_MouseLeave);
            QQ.OnMessagesNoticeEvent += new MessagesNoticeHandler(QQ_OnMessagesNoticeEvent);
            InitLoad();
            if (!string.IsNullOrEmpty(txt_QQ.Text) && !string.IsNullOrEmpty(txt_Pwd.Password))
                SetVCode();//设置验证码 
        }
        //初始化加载
        private void InitLoad()
        {

        }


        private void QQ_OnMessagesNoticeEvent(object msg)
        {
            Msg(msg.ToString());
        }
        /// <summary>
        /// 格式yyyy-MM-dd HH:mm:AAAbbb
        /// </summary>
        /// <param name="msg"></param>
        public void Msg(string msg)
        {
            //Global.SysContext.Send(o =>
            //{
            //    txt_msg.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ": " + msg + "\n";
            //    txt_msg.ScrollToEnd();
            //}, null);
            Global.SysContext.Send(o =>
            {
                txt_msg.Text = msg;
            }, null);
        }
        void txt_QQ_MouseLeave(object sender, MouseEventArgs e)
        {

            if (!txt_QQ.Text.Equals(_QQNumber))
            {
                SetVCode();
                _QQNumber = txt_QQ.Text;
            }
        }
        void img_yzm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetVCode();
        }

        private void SetVCode()
        {
            string qqNumber = txt_QQ.Text;
            string vccode = QQ.GetDefaultVC(qqNumber);
            if (string.IsNullOrEmpty(vccode))
            {
                byte[] buff = QQ.GetByteVCode(qqNumber);
                img_yzm.Source = ConvertToImg(buff);
            }
            else
            {
                txt_vCode.Text = vccode;
            }
        }


        public ImageSource ConvertToImg(byte[] buff)
        {
            ImageSource img = BaseApiCommon.ImageConvertCommon.ConvertByteToBitmapImage(buff);
            return img;
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string qq = txt_QQ.Text;
            string pwd = txt_Pwd.Password;
            if (string.IsNullOrEmpty(qq))
            {
                QQ.msg("请输入QQ帐号");
                return;
            }
            if (string.IsNullOrEmpty(pwd))
            {
                QQ.msg("请输入QQ密码");
                return;
            }
            QQ.msg("正在登录。。。。");
            bool b = QQ.Login(qq, pwd, txt_vCode.Text);
            if (b)
            {
                QQ.msg("登录成功");
                this.Hide();
                QQMain win = new QQMain();
                win.Show();
                this.Close();
            }
            else
            {
                //QQ.msg("登录失败");
                SetVCode();
            }
        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            YunTuResult txt = QT.ChatAPI.AskYunTu("你好");
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            QQ.msg("正在关闭。。。");
            this.Close();
        }
    }

}
