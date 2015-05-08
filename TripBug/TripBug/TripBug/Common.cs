using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;

namespace Trip
{
    public class Common
    {
        #region listview 点击列头 实现排序

        /// <summary>
        /// 列
        /// </summary>
        private static GridViewColumnHeader _CurSortCol = null;
        /// <summary>
        /// 图形
        /// </summary>
        private static SortAdorner _CurAdorner = null;
        /// <summary>
        /// 点击列头实现 排序 
        /// </summary>
        /// <param name="sender"> 列</param>
        /// <param name="lv">listview 控件</param>
        public static void SortColumn(GridViewColumnHeader column, ListView lv)
        {
            String field = column.Tag as String;
            if (_CurSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(_CurSortCol).Remove(_CurAdorner);//可视树中的第一个层以上。 删除掉装饰
                lv.Items.SortDescriptions.Clear();//清楚集合排序。
            }
            ListSortDirection newDir = ListSortDirection.Ascending;//升
            if (_CurSortCol == column && _CurAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;
            _CurSortCol = column;
            _CurAdorner = new SortAdorner(_CurSortCol, newDir);
            AdornerLayer.GetAdornerLayer(_CurSortCol).Add(_CurAdorner);//可视树中的第一个层以上。 增加
            lv.Items.SortDescriptions.Add(new SortDescription(field, newDir));
        }
        #endregion
    }
    /// <summary>
    ///排序  辅助类 
    /// </summary>
    public class SortAdorner : Adorner
    {
        private readonly static Geometry _AscGeometry = Geometry.Parse("M 0,0 L 10,0 L 5,5 Z");
        private readonly static Geometry _DescGeometry = Geometry.Parse("M 0,5 L 10,5 L 5,0 Z");//一个字符串，描述要创建的几何图形。 三角形
        public ListSortDirection Direction { get; private set; }//排序方式  Ascending 按升序排序。 Descending 按降序排序。

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="element"></param>
        /// <param name="dir"></param>
        public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
        {
            Direction = dir;
        }

        /// <summary>
        /// 在派生类中重写时，参与渲染操作
        /// </summary>
        /// <param name="drawingContext">绘图指令的特定元素</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (AdornedElement.RenderSize.Width < 20) return;

            drawingContext.PushTransform(new TranslateTransform(AdornedElement.RenderSize.Width - 15, (AdornedElement.RenderSize.Height - 5) / 2));//将指定的System.Windows.Media.Transform拖到绘图方面。 

            drawingContext.DrawGeometry(Brushes.Black, null, Direction == ListSortDirection.Ascending ? _AscGeometry : _DescGeometry);//使用指定的 Brush 和 Pen 绘制指定的 Geometry。 

            drawingContext.Pop();//弹出推送到绘制上下文上的最后一个不透明蒙板、不透明度、剪辑、效果或转换操作。 
        }
    }
}
