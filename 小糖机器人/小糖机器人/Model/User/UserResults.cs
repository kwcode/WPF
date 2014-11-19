using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace QT
{
    public class UserResults
    {

        [JsonProperty("retcode")]
        public int Retcode
        {
            get;
            set;
        }
        [JsonProperty("result")]
        public UserResult UserResult
        {
            get;
            set;
        }
    }
}
