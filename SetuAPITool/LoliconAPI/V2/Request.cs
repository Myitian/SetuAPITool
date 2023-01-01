using Newtonsoft.Json;
using SetuAPITool.Util;
using System;

namespace SetuAPITool.LoliconAPI.V2
{
    public class Request
    {
        [JsonProperty("r18", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public R18Type R18 { get; set; }
        [JsonProperty("num", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Num { get; set; }
        [JsonProperty("uid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int[] Uid { get; set; }
        [JsonProperty("Keyword", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Keyword { get; set; }
        [JsonProperty("Tag", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] Tag { get; set; }
        [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Size Size { get; set; }
        [JsonProperty("proxy", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Proxy { get; set; }
        [JsonProperty("dateAfter", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(UnixTimeMillisecondConverter))]
        public DateTime DateAfter { get; set; }
        [JsonProperty("dateBefore", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(UnixTimeMillisecondConverter))]
        public DateTime DateBefore { get; set; }
        [JsonProperty("dsc", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Dsc { get; set; }
        [JsonProperty("excludeAI", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? ExcludeAI { get; set; }
    }
}