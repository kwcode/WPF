using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using WorkNotes;

namespace UserManager
{
    public class WorkNotesHandler : TW.Client.HLUrl.EntityManagementWindowBase
    {
        protected override void LoadData(System.Windows.UIElement ui, Dictionary<string, string> param)
        {
            base.LoadData(ui, param);
        }
        protected override System.Windows.UIElement CreateUI(Dictionary<string, string> param)
        {
            TabItem ti = new TabItem();
            WorkNotesManagerCtrl _ui = new WorkNotesManagerCtrl();
            ti.Content = _ui;
            ti.Header = "工作记录";
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
