using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QT
{
    public class Info
    {
        [JsonProperty("face")]
        public int Face { get; set; }
        [JsonProperty("flag")]
        public long Flag { get; set; }
        [JsonProperty("nick")]
        public string Nick { get; set; }
        [JsonProperty("uin")]
        public long Uin { get; set; }

    }
}
