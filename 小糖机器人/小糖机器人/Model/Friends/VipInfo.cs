using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QT
{
    public class VipInfo
    {
        [JsonProperty("vip_level")]
        public int VipLevel { get; set; }
        [JsonProperty("u")]
        public int U { get; set; }
        [JsonProperty("is_vip")]
        public int IsVip { get; set; }
    }
}
