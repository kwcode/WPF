using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QT
{
    public class Friends
    {
        [JsonProperty("flag")]
        public int Flag { get; set; }
        [JsonProperty("uin")]
        public int Uin { get; set; }
        [JsonProperty("categories")]
        public int CateGories { get; set; }

    }
}
