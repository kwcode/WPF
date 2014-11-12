using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QT
{
    public class GroupResult
    {
        [JsonProperty("gmasklist")]
        public List<GmaskList> GmaskList
        {
            get;
            set;
        }
    }
}
