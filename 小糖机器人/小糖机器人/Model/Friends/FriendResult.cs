using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QT
{
    public class FriendResult
    {
        [JsonProperty("friends")]
        public List<Friends> Friends { get; set; }
        [JsonProperty("marknames")]
        public List<MarkNames> MarkNames { get; set; }
        [JsonProperty("categories")]
        public List<CateGories> CateGories { get; set; }
        [JsonProperty("vipinfo")]
        public List<VipInfo> VipInfo { get; set; }
        [JsonProperty("info")]
        public List<Info> Info { get; set; }
    }
}
