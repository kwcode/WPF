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
using System.Reflection;
using TW;

namespace ClientDebug
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string Parameter = "Parameter";
        private const string SelectedIndex = "SelectedIndex";
        public MainWindow()
        {
            InitializeComponent();
            StartUp();

            btLoadModule.Click += new RoutedEventHandler(btLoadModule_Click);
            btSaveSetting.Click += new RoutedEventHandler(btSaveSetting_Click);
        }


        private void StartUp()
        {

            GetAndRegAssemblyUrlHandlers();
            RegWindowManagerMessageProcessor();
            LoadDebuggingModulesInfo();

        }

        private void RegWindowManagerMessageProcessor()
        {
            TWWindowManager.Instance.HLTabCtrl = txTabContainer;
            TWWindowManager.Instance.Owner = this;
        }

        private void LoadDebuggingModulesInfo()
        {
            HandlerData hd = null;
            List<HandlerData> source = new List<HandlerData>();
            //加上主机头。
            Uri uri = new Uri("http://localhost");
            string url = "tw://" + uri.Host + "/";
            foreach (ITWUrlHandler iiit in _urlHandlers)
            {
                hd = new HandlerData();
                hd.Name = iiit.Path;
                hd.Handler = iiit;
                hd.Url = url + hd.Name;
                source.Add(hd);
            }
            cmbHandlers.ItemsSource = source;
            cmbHandlers.DisplayMemberPath = "Name";

            int ind = LoadModuleFromConfig();
            if (ind >= 0 && ind < source.Count)
            {
                cmbHandlers.SelectedIndex = ind;
                //LoadModule();
            }
            else if (source.Count == 1)
            {
                cmbHandlers.SelectedIndex = 0;
                //LoadModule();
            }
        }

        private int LoadModuleFromConfig()
        {
            int ind = -1;
            if (cmbHandlers.Items.Count <= 0) return ind;
            string para = _UC.GetValue(Parameter);
            txtParas.Text = para;
            string index = _UC.GetValue(SelectedIndex);
            try
            {
                int i = Convert.ToInt16(index);
                if (i >= 0 && i < cmbHandlers.Items.Count)
                {
                    ind = i;
                }
            }
            catch { }

            return ind;
        }
        private UserConfig _UC = new UserConfig();
        private List<ITWUrlHandler> _urlHandlers = new List<ITWUrlHandler>();
        private void GetAndRegAssemblyUrlHandlers()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            List<string> dllsName = _UC.GetDllsName();
            foreach (string str in dllsName)
            {
                try
                {
                    Assembly ass = Assembly.LoadFile(System.IO.Path.Combine(path, str));
                    Type[] tps = ass.GetTypes();
                    foreach (Type tp in tps)
                    {
                        if (tp.IsClass && tp.GetInterface("TW.ITWUrlHandler") != null)
                        {
                            ITWUrlHandler handler = Activator.CreateInstance(tp) as ITWUrlHandler;
                            TWUrlManager.RegHandler(handler);
                            _urlHandlers.Add(handler);
                        }
                    }
                }
                catch { }
            }
        }
        void btLoadModule_Click(object sender, RoutedEventArgs e)
        {
            if (cmbHandlers.SelectedItem == null || cmbHandlers.SelectedIndex == -1) return;
            HandlerData hd = cmbHandlers.SelectedItem as HandlerData;
            string url = hd.Url;
            if (!string.IsNullOrEmpty(txtParas.Text))
            {
                url += "?" + txtParas.Text;
            }
            TWUrlManager.Open(url);
        }
        void btSaveSetting_Click(object sender, RoutedEventArgs e)
        {
            if (cmbHandlers.SelectedIndex == -1) return;
            _UC.SetValue(Parameter, txtParas.Text);
            _UC.SetValue(SelectedIndex, cmbHandlers.SelectedIndex.ToString());
            _UC.Save();
            MessageBox.Show("保存状态成功。");
        }

    }
    internal class HandlerData
    {
        public string Name { get; set; }
        public ITWUrlHandler Handler { get; set; }
        public string Url { get; set; }
    }

}
