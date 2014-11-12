using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QT
{
    public class DnameList
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("did")]
        public int Did { get; set; }

    }
}
