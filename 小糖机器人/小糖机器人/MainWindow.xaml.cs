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
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            img_yzm.MouseDown += new MouseButtonEventHandler(img_yzm_MouseDown);
            txt_QQ.MouseLeave += new MouseEventHandler(txt_QQ_MouseLeave);
            QQ.OnMsgEvent += QQ_OnMsgEvent;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetVCode();
        }
        private void Msg(object msg)
        {
            txt_msg.Text += msg.ToString();
            txt_msg.ScrollToEnd();
        }
        void QQ_OnMsgEvent(object msg)
        {
            Msg(msg);
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
            bool b = QQ.IsHaveYZM(qqNumber);
            if (b)
            {
                byte[] buff = QQ.GetByteVCode(qqNumber);
                img_yzm.Source = ConvertToImg(buff);
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
                Msg("登录成功\n");
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
                Msg("登录失败\n");
                SetVCode();
            }
        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            string uin = Global.Uin;// txt_Status.Text;
            Msg(QQ.GetHash(uin, Global.PtWebQQ));

            //Msg(QQ.GetHash("761607380", "81c6158d4b703b6d5a0cb4bf0b4ec7ada0ae538ed9e69cddff1b7c7c4964e766"));
        }

        private void btn_Friends_Click(object sender, RoutedEventArgs e)
        {
            QQ.GetFriendResults();
            //QQ.GetGroupResults();
            //QQ.GetVfWebqq("", "");
        }

    }

}
