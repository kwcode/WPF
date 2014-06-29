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

namespace ClientDebug
{
    /// <summary>
    /// WinTest.xaml 的交互逻辑
    /// </summary>
    public partial class WinTest : Window
    {
        public WinTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public interface IUrlHandler
    {
        string Path { get; }
        void Open(string str);
    }
    public abstract class UrlBase : IUrlHandler
    {

        public string Path
        {
            get { return GetUrlPath(); }
        }

        public void Open(string str)
        {
            MessageBox.Show("open");
        }

        protected abstract string GetUrlPath();
        protected virtual void LoadData(UIElement ui, Dictionary<string, string> param) { }
        protected abstract UIElement CreateUI(Dictionary<string, string> param);
        protected abstract bool IsTabControl();
    }
    public abstract class TabBaseHandler : UrlBase
    {
        //override sealed bool IsTabControl()
        //{
        //    return true;
        //} 

        protected override sealed bool IsTabControl()
        {
            throw new NotImplementedException();
        }
    }
    public abstract class EntityManagerWindow : TabBaseHandler
    {
        protected override string GetUrlPath()
        {
            return "UrlPaht";
        }
    }

    public class ActionHandler : EntityManagerWindow
    {
        protected override UIElement CreateUI(Dictionary<string, string> param)
        {
            UserManager.UserEditWin win = new UserManager.UserEditWin();
            return win;
        }
        protected override void LoadData(UIElement ui, Dictionary<string, string> param)
        {
            base.LoadData(ui, param);
        }
    }

}
