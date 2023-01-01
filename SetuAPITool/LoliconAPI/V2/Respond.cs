using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.LoliconAPI.V2
{
    public class LoliconRespond
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("data")]
        public Setu[] Data { get; set; }

    }
}
