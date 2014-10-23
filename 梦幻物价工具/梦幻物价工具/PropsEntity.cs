using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 梦幻物价工具
{
    public class PropsEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string ImgPath { get; set; }
        /// <summary>
        /// 意向价格
        /// </summary>
        public double IntentPrice { get; set; }
        /// <summary>
        /// 标价格
        /// </summary>
        public double MarkedPrice { get; set; }
        /// <summary>
        /// 购买价格
        /// </summary>
        public double BuyPrice { get; set; }
    }
}
