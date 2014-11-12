using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace QT
{
    public class MessageResults
    {
        [JsonProperty("retcode")]
        public int Retcode
        {
            get;
            set;
        }
        [JsonProperty("result")]
        public List<MessageResult> MessageResult
        {
            get;
            set;
        }
    }
}
