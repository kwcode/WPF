using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace UserManager
{
    public class UserManangerHandler : TW.Client.HLUrl.EntityManagementWindowBase
    {
        protected override void LoadData(System.Windows.UIElement ui, Dictionary<string, string> param)
        {
            base.LoadData(ui, param);
        }
        protected override System.Windows.UIElement CreateUI(Dictionary<string, string> param)
        {
            TabItem ti = new TabItem();
            UserManagerCtrl _UI = new UserManagerCtrl();
            ti.Content = _UI;
            ti.Header = "用户管理";
            ti.Tag = GetURLPath();
            return ti;
        }


    }

    public class UserEditHandler : TW.Client.HLUrl.EntityPropertyWindowBase
    {
        protected override System.Windows.UIElement CreateUI(Dictionary<string, string> param)
        {
            UserEditWin win = new UserEditWin(); 
            win.ShowInTaskbar = false;
            return win;
        } 
    }
}
