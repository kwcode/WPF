using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QT
{
    public class UserResult
    {
        [JsonProperty("uin")]
        public long Uin { get; set; }
        [JsonProperty("cip")]
        public long Cip { get; set; }
        [JsonProperty("index")]
        public int Index { get; set; }
        [JsonProperty("port")]
        public int Port { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("vfwebqq")]
        public string Vfwebqq { get; set; }
        [JsonProperty("psessionid")]
        public string Psessionid { get; set; }
        [JsonProperty("user_state")]
        public int UserState { get; set; }
        [JsonProperty("f")]
        public int F { get; set; }
    }
}
