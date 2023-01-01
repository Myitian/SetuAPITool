﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.Anosu
{
    public class Respond // note: respond root is a list
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
        public int R18 { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
