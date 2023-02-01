using Newtonsoft.Json;
using System.Collections.Generic;

namespace Myitian.SetuAPITool.MirlKoi
{
    /// <summary>MirlKoi 响应</summary>
    public class Response
    {
        /// <summary>图像列表</summary>
        [JsonProperty("pic")]
        public List<string> Pic { get; set; }
    }
}
