using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QT
{
    public class GmaskList
    {
        [JsonProperty("flag")]
        public int Flag { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("gid")]
        public int Gid { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
