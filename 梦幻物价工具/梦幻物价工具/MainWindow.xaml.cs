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
using System.Data;

namespace 梦幻物价工具
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClass dataClass = new DataClass();
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            List<PropsEntity> list = new List<PropsEntity>();
            DataTable dt = dataClass.data.ExecuteDataTable("p_comm_GetPropsList", new object[] { });

            foreach (DataRow item in dt.Rows)
            {
                PropsEntity entity = new PropsEntity();
                entity.ID = Convert.ToInt32(item["ID"]);
                entity.Name = item["Name"].ToString();
                entity.ImgPath = item["ImgPath"].ToString();
                entity.Level = Convert.ToInt32(item["Level"]);
                entity.IntentPrice = Convert.ToDouble(item["IntentPrice"] is DBNull ? "0" : item["IntentPrice"]);
                entity.MarkedPrice = Convert.ToDouble(item["MarkedPrice"] is DBNull ? "0" : item["MarkedPrice"]);
                entity.BuyPrice = Convert.ToDouble(item["BuyPrice"] is DBNull ? "0" : item["BuyPrice"]);
                list.Add(entity);
            }
            BingData(list);
        }

        public void BingData(List<PropsEntity> list)
        {
            lv_data.ItemsSource = list;
        }
    }
}
