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
using BaseApiCommon;

namespace WPFDemo
{
    /// <summary>
    /// XMLCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class XMLCtrl : UserControl
    {
        public XMLCtrl()
        {
            InitializeComponent();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            List<XMLAttributes> atts = new List<XMLAttributes>();
            atts.Add(new XMLAttributes { Name = "ID", Value = "1" });
            atts.Add(new XMLAttributes { Name = "NowTime", Value = DateTime.Now.ToString("yyyy-MM-dd") });
            BaseApiCommon.XmlCommon.InsetXml("Code", "TEST", atts);

            BaseApiCommon.XmlCommon.InsetXml("Work", "", atts);

            txt_msg.Text = "读出\n\r" + BaseApiCommon.XmlCommon.GetXmlDoc();
        }

        private void btn_Init_Click(object sender, RoutedEventArgs e)
        {
            BaseApiCommon.XmlCommon.InitializationXmlDoc();
            txt_msg.Text = "初始化成功!\n\r";
            //txt_msg.Text += "刷新数据\n\r";
            //BaseApiCommon.XmlCommon.RefreshDoc();
            txt_msg.Text += "正在加载XML文件: \n\r";
            txt_msg.Text += BaseApiCommon.XmlCommon.GetXmlDoc();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            txt_msg.Text = "开始修改数据!\n\r";
            List<XMLAttributes> atts = new List<XMLAttributes>();
            atts.Add(new XMLAttributes { Name = "ID", Value = "1" });
            atts.Add(new XMLAttributes { Name = "NowTime", Value = DateTime.Now.ToString("yyyy-MM-dd") });
            BaseApiCommon.XmlCommon.UpdateNodeContent("Work", "修改的内容", atts);
            txt_msg.Text += BaseApiCommon.XmlCommon.GetXmlDoc();
        }

        private void btn_Sel_Click(object sender, RoutedEventArgs e)
        {
            List<XMLAttributes> atts = new List<XMLAttributes>();
            atts.Add(new XMLAttributes { Name = "ID", Value = "1" });
            txt_msg.Text = "查找节点\n\r";
            atts.Add(new XMLAttributes { Name = "NowTime", Value = DateTime.Now.ToString("yyyy-MM-dd") });
            txt_msg.Text += BaseApiCommon.XmlCommon.FindNodeContent("Work", atts);
            //  txt_msg.Text += BaseApiCommon.XmlCommon.FindXmlNodeList();
        }

        private void btn_ReadXml_Click(object sender, RoutedEventArgs e)
        {
            txt_msg.Text = "读出XML文件\n\r";
            txt_msg.Text += BaseApiCommon.XmlCommon.GetXmlDoc();
        }
    }
}
