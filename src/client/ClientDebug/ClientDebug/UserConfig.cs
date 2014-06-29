using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ClientDebug
{
    public class UserConfig
    {
        XDocument _Doc;
        string _AttrName = "value";
        string _Uri = "UserConfig.xml";
        public UserConfig()
        {
            _Doc = XDocument.Load(_Uri);
        }

        public List<string> GetDllsName()
        {
            List<string> result = new List<string>();

            XAttribute attr = null;
            IEnumerable<XElement> dlls = _Doc.Descendants("Dll");
            foreach (XElement ele in dlls)
            {
                attr = ele.Attribute(_AttrName);
                if (!string.IsNullOrEmpty(attr.Value))
                {
                    result.Add(attr.Value);
                }
            }

            return result;
        }

        public void SetValue(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (value == null) value = "";
            IEnumerable<XElement> eles = _Doc.Descendants(name);
            if (eles.Count() > 0)
            {
                XElement ele = eles.ElementAt(0);
                if (ele != null)
                {
                    XAttribute attr = ele.Attribute(_AttrName);
                    if (attr == null)
                    {
                        attr = new XAttribute(_AttrName, value);
                        ele.Add(attr);
                    }
                    else
                    {
                        attr.Value = value;
                    }
                }
            }
        }
        public void Save()
        {
            _Doc.Save(_Uri);
        }

        public string GetValue(string name)
        {
            string result = "";
            if (string.IsNullOrEmpty(name)) return null;
            IEnumerable<XElement> eles = _Doc.Descendants(name);
            if (eles.Count() > 0)
            {
                XElement ele = eles.ElementAt(0);
                if (ele != null)
                {
                    XAttribute attr = ele.Attribute(_AttrName);
                    result = attr.Value;
                }
            }
            return result;
        }
    }
}
