using Newtonsoft.Json;
using System.Collections.Generic;

namespace SetuAPITool.LoliconAPI.V2
{
    public class Setu : PixivInfo
    {
        [JsonProperty("ext")]
        public string Ext { get; set; }

        [JsonProperty("urls")]
        public Dictionary<string, string> Urls { get; set; }

        public Setu() { }
        public Setu(PixivInfo pixivInfo)
        {
            Pid = pixivInfo.Pid;
            P = pixivInfo.P;
            Uid = pixivInfo.Uid;
            Title = pixivInfo.Title;
            Author = pixivInfo.Author;
            R18 = pixivInfo.R18;
            Width = pixivInfo.Width;
            Height = pixivInfo.Height;
            Tags = pixivInfo.Tags;
            Urls = new Dictionary<string, string> { { "Fallback", pixivInfo.Url } };
        }
    }
}
