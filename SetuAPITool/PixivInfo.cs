using Myitian.SetuAPITool.LoliconAPI.V2;
using Newtonsoft.Json;
using System.Linq;

namespace Myitian.SetuAPITool
{
    /// <summary>Pixiv图像信息</summary>
    public class PixivInfo
    {
        /// <summary>作品 pid</summary>
        [JsonProperty("pid")]
        public int Pid { get; set; }

        /// <summary>作品所在页</summary>
        [JsonProperty("p")]
        public int P { get; set; }

        /// <summary>作者 uid</summary>
        [JsonProperty("uid")]
        public int Uid { get; set; }

        /// <summary>Pixiv图像信息</summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>作者名</summary>
        [JsonProperty("author")]
        public string Author { get; set; }
        /// <inheritdoc cref="Author"/>
        [JsonProperty("user")]
        public string User { set => Author = value; }


        /// <summary>是否 R18</summary>
        [JsonProperty("r18")]
        public bool R18 { get; set; }

        /// <summary>原图宽度 px</summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>原图高度 px</summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>作品标签，包含标签的中文翻译（有的话）</summary>
        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        /// <summary>图片链接（可能存在有些作品因修改或删除而导致 404 的情况）</summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        ///
        public PixivInfo() { }
        /// <summary>从 <see cref="Setu"/> 创建</summary>
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
