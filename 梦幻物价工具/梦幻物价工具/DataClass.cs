using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCommon;

namespace 梦幻物价工具
{
    public class DataClass
    {

        public DataPool data = new DataPool(10, System.Configuration.ConfigurationManager.AppSettings["sqlconnstring"]);
    }
}
