using Myitian.SetuAPITool.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Myitian.SetuAPITool.LoliconAPI.V2
{
    ///
    public class Setu : PixivInfo
    {
        /// <summary>图片扩展名</summary>
        [JsonProperty("ext")]
        public string Ext { get; set; }

        /// <summary>是否是 AI 作品</summary>
        [JsonProperty("aiType")]
        public AiTypes AiType { get; set; }

        /// <summary>作品上传日期</summary>
        [JsonProperty("uploadDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(UnixTimeMillisecondConverter))]
        public DateTime UploadDate { get; set; }

        /// <summary>包含了所有指定 <see cref="Request.Size"/> 的图片地址</summary>
        [JsonProperty("urls")]
        public Dictionary<string, string> Urls { get; set; }

        ///
        public Setu() { }
        /// <summary>从 <see cref="PixivInfo"/> 创建</summary>
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
