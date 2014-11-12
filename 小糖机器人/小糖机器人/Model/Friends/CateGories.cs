using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QT
{
    public class CateGories
    {
        [JsonProperty("index")]
        public int Index { get; set; }
        [JsonProperty("sort")]
        public int Sort { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
