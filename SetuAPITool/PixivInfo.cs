using Newtonsoft.Json;
using SetuAPITool.LoliconAPI.V2;
using SetuAPITool.Util;
using System;
using System.Linq;

namespace SetuAPITool
{
    public class PixivInfo
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

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("aiType")]
        public AiTypes AiType { get; set; }

        [JsonProperty("uploadDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(UnixTimeMillisecondConverter))]
        public DateTime UploadDate { get; set; }

        public PixivInfo() { }
        public PixivInfo(Setu setu)
        {
            Pid = setu.Pid;
            P = setu.P;
            Uid = setu.Uid;
            Title = setu.Title;
            Author = setu.Author;
            R18 = setu.R18;
            Width = setu.Width;
            Height = setu.Height;
            Tags = setu.Tags;
            if (setu.Urls.Count > 0)
            {
                Url = setu.Urls.Values.ElementAt(0);
            }
        }
    }
}
