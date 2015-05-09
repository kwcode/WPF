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
    /// TripMain.xaml 的交互逻辑
    /// </summary>
    public partial class TripMain : Window
    {
        public TripMain()
        {
            InitializeComponent();
            this.Loaded += TripMain_Loaded;
        }

        void TripMain_Loaded(object sender, RoutedEventArgs e)
        {
            txt_nickname.Text = HC.UserInfo.NickName;
        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
