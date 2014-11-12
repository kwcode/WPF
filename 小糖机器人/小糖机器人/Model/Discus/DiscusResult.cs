using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QT
{
    public class DiscusResult
    {
        [JsonProperty("dnamelist")]
        public List<DnameList> DnameList { get; set; }
    }
}
