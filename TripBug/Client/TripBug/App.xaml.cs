using BaseApiCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Trip
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //TripMain win = new TripMain();
            //win.Show();
            base.OnStartup(e);
            Global.SysContext = System.Threading.SynchronizationContext.Current;
            BaseApiCommon.ServiceProxy.URL = UserConfig.Config.GetValue("ServiceUrl");// "http://localhost:2015/HLService.aspx?Type=0";
        }
    }
}
