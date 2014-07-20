using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using BaseApiCommon;

namespace WorkNotes
{
    /// <summary>
    /// WorkNotesManagerCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class WorkNotesManagerCtrl : UserControl
    {
        public WorkNotesManagerCtrl()
        {
            InitializeComponent();
            RegistrationEvent();
            this.Loaded += new RoutedEventHandler(WorkNotesManagerCtrl_Loaded);
        }

        private void RegistrationEvent()
        {
            this.KeyDown += new KeyEventHandler(WorkNotesManagerCtrl_KeyDown);
            date_Box.DoClickEvent += new WPF.DoClick(date_Box_DoClickEvent);
        }

        void date_Box_DoClickEvent(object sender)
        {
            string content = sender.ToString();
            MessageBox.Show(content);
        }
        void WorkNotesManagerCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            //bool isKey = Keyboard.IsKeyDown(Key.LeftCtrl);
            //if (isKey)
            //{
            //    if (e.Key == Key.S)
            //    {
            //        MessageBox.Show(rich_Box.RBContent.ToString());
            //    }
            //}
            if (e.Key == Key.S && (Keyboard.Modifiers &
(ModifierKeys.Control)) == (ModifierKeys.Control))
            {
                Save();
            }
        }

        void Save()
        {
            string content = rich_Box.RBContent;
            string toDay = DateTime.Now.ToString("yyyy-MM-dd");
            List<BaseApiCommon.XMLAttributes> atts = new List<BaseApiCommon.XMLAttributes>();
            atts.Add(new BaseApiCommon.XMLAttributes() { Name = "NowTime", Value = toDay });
            BaseApiCommon.XmlCommon.UpdateNodeContent("Work", content, atts);
            Msg("保存成功！");
        }

        void WorkNotesManagerCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            new Thread(o =>
            {
                string toDay = DateTime.Now.ToString("yyyy-MM-dd");
                List<BaseApiCommon.XMLAttributes> atts = new List<BaseApiCommon.XMLAttributes>();
                atts.Add(new BaseApiCommon.XMLAttributes() { Name = "NowTime", Value = toDay });
                string content = BaseApiCommon.XmlCommon.FindNodeContent("Work", new List<BaseApiCommon.XMLAttributes>());
                Global.SysContext.Send(a =>
                {
                    rich_Box.RBContent = content;
                }, null);

            }).Start();
        }


        private void Msg(string msg)
        {
            txt_msg.Text = DateTime.Now.ToString("MM-dd hh:mm") + ": " + msg;
        }
    }
}
