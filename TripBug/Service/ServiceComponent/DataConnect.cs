using DBCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Trip
{
    public class DataConnect
    {
        public static DataPool Data = new DataPool(100, ConfigurationManager.AppSettings["ConnString"]);
    }
}
