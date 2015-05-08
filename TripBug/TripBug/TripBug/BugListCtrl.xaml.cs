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

namespace Trip
{
    /// <summary>
    /// BugListCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class BugListCtrl : UserControl
    {
        public BugListCtrl()
        {
            InitializeComponent();

            List<BugEntity> list = new List<BugEntity>();
            list.Add(new BugEntity() { ID = "1", Author = "tkw", CreateTS = DateTime.Now.ToString() });
            lv_data.ItemsSource = list;
        }
        private void SortClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            Common.SortColumn(column, lv_data);
 
        }
        protected void HandleDoubleFileClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void btn_search_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
