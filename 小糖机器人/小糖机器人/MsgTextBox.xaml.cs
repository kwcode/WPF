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

namespace QT
{
    /// <summary>
    /// MsgTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class MsgTextBox : UserControl
    {
        public MsgTextBox()
        {
            InitializeComponent();
        }

        private void copy_Click(object sender, RoutedEventArgs e)
        {
            string txt = txt_msg.SelectedText;
            Clipboard.SetText(txt);
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            txt_msg.Text = "";
        }
        public string MsgContent
        {
            get { return txt_msg.Text; }
            set
            {
                txt_msg.Text = value;
            }
        }
        /// <summary>
        /// 格式yyyy-MM-dd HH:mm:AAAbbb
        /// </summary>
        /// <param name="msg"></param>
        public void Msg(string msg)
        {
            txt_msg.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ": " + msg + "\n";
            txt_msg.ScrollToEnd();
        }

    }
}
