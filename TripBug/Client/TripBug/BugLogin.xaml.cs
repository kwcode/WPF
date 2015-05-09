using BaseApiCommon;
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
using System.Windows.Shapes;

namespace Trip
{
    /// <summary>
    /// BugLogin.xaml 的交互逻辑
    /// </summary>
    public partial class BugLogin : Window
    {
        public BugLogin()
        {
            InitializeComponent();
            this.Loaded += BugLogin_Loaded;
        }

        void BugLogin_Loaded(object sender, RoutedEventArgs e)
        {
            string autoLogin = UserConfig.Config.GetValue("AutoLogin");
            string name = UserConfig.Config.GetValue("UserID");
            string pwd = UserConfig.Config.GetValue("Password");
            txt_loginname.Text = name;
            txt_password.Password = pwd;
            if (!string.IsNullOrEmpty(pwd))
            {
                cb_keep.IsChecked = true;
            }
            if (autoLogin == "1" && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pwd))
            {
                cb_auto.IsChecked = true;
                TryLogin(txt_loginname.Text, txt_password.Password);
            }


        }
        public void TryLogin(string loginname, string pwd)
        {
            UserInfo user = new UserInfo() { NickName = "管理员" }; //Services.UserService.Login(loginname, pwd);
            EndLogin();
            //if (row == 1)
            //{

            //}
            //登录成功！
            HC.UserInfo = user;
            this.Hide();
            TripMain win = new TripMain();
            win.Show();
            this.Close();
        }
        public void EndLogin()
        {
            if (cb_keep.IsChecked == false)
            {
                UserConfig.Config.SetValue("Password", "");
            }
            else
            {
                UserConfig.Config.SetValue("Password", txt_password.Password);
            }
            UserConfig.Config.SetValue("UserID", txt_loginname.Text);
            UserConfig.Config.SetValue("AutoLogin", Convert.ToInt32(cb_auto.IsChecked).ToString());
            UserConfig.Config.Save();
        }
        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string name = txt_loginname.Text;
            string pwd = txt_password.Password;
            TryLogin(name, pwd);
        }

        private void btn_out_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("是否退出？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

    }
}
