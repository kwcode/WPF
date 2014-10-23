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

namespace 梦幻物价工具
{
    /// <summary>
    /// PropsEdit.xaml 的交互逻辑
    /// </summary>
    public partial class PropsEdit : Window
    {
        public PropsEdit()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(PropsEdit_KeyDown);
        }

        void PropsEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                BitmapSource image = Clipboard.GetImage();
                if (image != null)
                {
                    if (img_photo.Source != image)
                    {
                        img_photo.Source = image;
                    }

                }
            }
        }
    }
}

