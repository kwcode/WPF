using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using WPFDemo;

namespace UserManager
{
    public class DemoHandler : TW.Client.HLUrl.EntityManagementWindowBase
    {
        protected override void LoadData(System.Windows.UIElement ui, Dictionary<string, string> param)
        {
            base.LoadData(ui, param);
        }
        protected override System.Windows.UIElement CreateUI(Dictionary<string, string> param)
        {
            TabItem ti = new TabItem();
            XMLCtrl _ui = new XMLCtrl();
            ti.Content = _ui;
            ti.Header = "XML操作";
            ti.Tag = GetURLPath();
            return ti;
        }


    }

    //public class UserEditHandler : TW.Client.HLUrl.EntityPropertyWindowBase
    //{
    //    protected override System.Windows.UIElement CreateUI(Dictionary<string, string> param)
    //    {
    //        UserEditWin win = new UserEditWin();
    //        win.ShowInTaskbar = false;
    //        return win;
    //    }
    //}
}
