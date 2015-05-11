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
            btn_Home.Click += new RoutedEventHandler(btn_Home_Click);
            btn_Bug.Click += new RoutedEventHandler(btn_Bug_Click);
        }

        void TripMain_Loaded(object sender, RoutedEventArgs e)
        {
          //  txt_nickname.Text = HC.UserInfo.NickName; 
        }
        #region 按钮=====================================================


        void btn_Bug_Click(object sender, RoutedEventArgs e)
        {
            AddChildren(HCControl.bug);
        }

        void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            AddChildren(HCControl.home);
        }
        Dictionary<HCControl, UserControl> _DicHCControl = new Dictionary<HCControl, UserControl>();
        private void AddChildren(HCControl hc)
        {
            if (_DicHCControl.ContainsKey(hc))
            {
                UserControl ctrl = _DicHCControl[hc];
                gridContent.Children.Clear();
                gridContent.Children.Add(ctrl);
            }
            else
            {
                gridContent.Children.Clear();
                if (hc == HCControl.home)
                {
                    HomeCtrl home = new HomeCtrl();
                    gridContent.Children.Add(home);
                    _DicHCControl.Add(hc, home);
                }
                else if (hc == HCControl.bug)
                {
                    BugListCtrl bugctrl = new BugListCtrl();
                    gridContent.Children.Add(bugctrl);
                    _DicHCControl.Add(hc, bugctrl);
                }
                else if (hc == HCControl.setting)
                {
                }
            }
        }
        #endregion
        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {

        }

    }
    public enum HCControl
    {
        home = 1,
        bug = 2,
        setting = 3,
    }
}
