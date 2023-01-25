using Newtonsoft.Json;
using SetuAPITool.Util;
using System;
using System.Collections.Generic;

namespace SetuAPITool.LoliconAPI.V2
{
    public class Setu
    {
        [JsonProperty("pid")]
        public int Pid { get; set; }

        [JsonProperty("p")]
        public int P { get; set; }

        [JsonProperty("uid")]
        public int Uid { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("r18")]
        public bool R18 { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("ext")]
        public string Ext { get; set; }

        [JsonProperty("aiType")]
        public AiTypes AiType { get; set; }

        [JsonProperty("uploadDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(UnixTimeMillisecondConverter))]
        public DateTime UploadDate { get; set; }

        [JsonProperty("urls")]
        public Dictionary<string, string> Urls { get; set; }
    }
}
