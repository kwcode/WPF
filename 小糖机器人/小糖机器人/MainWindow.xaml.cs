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
            yiwoSDK.CFun.I_LOVE_BBS("yiwowang.com");
            this.Loaded += MainWindow_Loaded;
            img_yzm.MouseDown += new MouseButtonEventHandler(img_yzm_MouseDown);
            txt_QQ.MouseLeave += new MouseEventHandler(txt_QQ_MouseLeave);
            QQ.OnMessagesNoticeEvent += new MessagesNoticeHandler(QQ_OnMessagesNoticeEvent);

        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //  QQ.DoLogin_Sig();
            SetVCode();
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
            Global.SysContext.Send(o =>
            {
                txt_msg.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ": " + msg + "\n";
                txt_msg.ScrollToEnd();
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
            bool b = QQ.Login(txt_QQ.Text, txt_Pwd.Text, txt_vCode.Text);
            if (b)
            {
                QQ.msg("登录成功\n");
                img_yzm.MouseDown -= new MouseButtonEventHandler(img_yzm_MouseDown);
                txt_QQ.MouseLeave -= new MouseEventHandler(txt_QQ_MouseLeave);
                img_yzm.Visibility = Visibility.Collapsed;
                QQMain win = new QQMain();
                win.Show();
                this.Hide();
                this.Close();
            }
            else
            {
                QQ.msg("登录失败\n");
                SetVCode();
            }
        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
