using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace TW
{
    public class TWWindowManager
    {
        private static object _LockObj = new object();
        private object _ModallessDialogLockObj = new object();
        private object _TabItemLockObj = new object();

        /// <summary>
        /// 该类的唯一实例。
        /// </summary>
        private static TWWindowManager _HLWinManager = null;
        /// <summary>
        /// 非模态对话框类型窗体的缓存器。
        /// </summary>
        private Dictionary<string, Window> _ModallessDialogDic = new Dictionary<string, Window>();
        private Dictionary<string, TabItem> _TabItemDic = new Dictionary<string, TabItem>();

        /// <summary>
        /// 静态实例。
        /// </summary>
        public static TWWindowManager Instance
        {
            get
            {
                if (_HLWinManager == null)
                {
                    lock (_LockObj)
                    {
                        if (_HLWinManager == null)
                        {
                            _HLWinManager = new TWWindowManager();
                        }
                    }
                }

                return _HLWinManager;
            }
            set
            {
                lock (_LockObj)
                {
                    _HLWinManager = value;
                }
            }
        }
        private TabControl _TabCtrl = null;
        /// <summary>
        /// 接收主窗体客户区的容器：HLTabControl。
        /// </summary>
        public TabControl HLTabCtrl
        {
            get { return _TabCtrl; }
            set { _TabCtrl = value; }
        }
        private Object _owner;
        public Object Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }
        public void Open(string key, bool inTabCtrl)
        {
            if (inTabCtrl)   //在TabControl里面打开
            {
                lock (_TabItemLockObj)
                {
                    TabItem hlti = null;
                    if (_TabItemDic.TryGetValue(key.ToLower(), out hlti))
                    {
                        _TabCtrl.SelectedItem = hlti;
                        //  hlti.HLControlCloseButton2 = (CustomerSetting.ShowCloseButton ? Visibility.Visible : Visibility.Collapsed);
                    }
                    else
                    {
                        throw new Exception("打开失败！");
                    }
                }
            }
            else
            {

                lock (_ModallessDialogLockObj)
                {
                    Window w = null;
                    if (_ModallessDialogDic.TryGetValue(key.ToLower(), out w))
                    {
                        w.Show();
                        w.Activate();
                    }
                    else
                    {
                        throw new Exception("打开失败！");
                    }
                }
            }
        }

        /// <summary>
        /// 将窗体加入容器，如果是Window类型的窗体，这里调用Show（），如果是HLTabItem，将其加入主窗口指定的容器。
        /// 注意：这里没有检查key是否存在，需要调用者自己确保key的唯一性。如果可以已经存在，需要调用Open来打开UI。
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="key">窗口唯一标识。</param>
        /// <param name="ownerType"></param>
        public bool Add(string key, UIElement ui, bool inTabCtrl)
        {
            bool IsOpened = false;

            Window w = null;
            if (inTabCtrl)  //在TabControl中打开
            {
                TabItem t = ui as TabItem;

                lock (_TabItemLockObj)
                {
                    if (!_TabItemDic.ContainsKey(key.ToLower()))
                    {
                        _TabItemDic.Add(key.ToLower(), t);
                        //t.HLControlCloseButton2 = (CustomerSetting.ShowCloseButton ? Visibility.Visible : Visibility.Collapsed);
                        //t.Closed -= t_Closed;
                        //t.Closed += t_Closed;
                        if (_TabCtrl != null)
                        {
                            _TabCtrl.Items.Add(t);
                            _TabCtrl.SelectedItem = t;

                            //给该项添加右键菜单。
                            //HLSeparator hlSp = new HLSeparator();
                            //t.AddContextMenuItem(hlSp);

                            //HLMenuItem hlMI = CreatePinFunctionModule(t.Tag);
                            //t.AddContextMenuItem(hlMI);

                            //TODO:任务收藏暂时屏蔽。
                            //hlMI = CreatePinTask(t.Tag);
                            //t.AddContextMenuItem(hlMI);
                        }
                    }
                }
            }
            else    // 在新的窗口中打开
            {
                w = ui as Window;
                lock (_ModallessDialogLockObj)
                {
                    if (!_ModallessDialogDic.ContainsKey(key.ToLower()))
                    {
                        _ModallessDialogDic.Add(key.ToLower(), w);
                        w.Owner = _owner as Window;
                        w.Show();
                        w.Activate();
                        //w.Closed -= w_Closed;
                        //w.Closed += w_Closed;
                    }
                }
            }
            return IsOpened;
        }

    }
}
