using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.Jitsu
{
    internal class Respond
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("pics")]
        public List<string> Pics { get; set; }
    }
}
