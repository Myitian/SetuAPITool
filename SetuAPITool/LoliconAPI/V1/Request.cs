using Myitian.SetuAPITool.Util;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Myitian.SetuAPITool.LoliconAPI.V1
{
    /// <summary>Lolicon API V1 请求</summary>
    public class Request
    {
        /// <summary>在库中的分类，不等同于作品本身的 R18 标识</summary>
        [JsonProperty("r18")]
        public R18Type R18 { get; set; }

        /// <summary>若指定关键字，将会返回从插画标题、作者、标签中模糊搜索的结果</summary>
        [JsonProperty("keyword")]
        public string Keyword { get; set; }

        /// <summary>一次返回的结果数量，范围为 <c>1</c> 到 <c>20</c>；在指定关键字的情况下，结果数量可能会不足指定的数量</summary>
        [JsonProperty("num")]
        public int Num { get; set; }

        /// <summary>设置返回的原图链接的域名，你也可以设置为 <c>disable</c> 来得到真正的原图链接</summary>
        [JsonProperty("proxy")]
        public string Proxy { get; set; }

        /// <summary>是否使用 master_1200 缩略图，即长或宽最大为 1200px 的缩略图，以节省流量或提升加载速度（某些原图的大小可以达到十几MB）</summary>
        [JsonProperty("size1200")]
        public bool Size1200 { get; set; }

        ///
        public Request() { }
        /// <summary>从键值对参数创建 <see cref="Request"/></summary>
        /// <param name="parameters">键值对参数</param>
        public Request(params KeyValuePair<string, string>[] parameters)
        {
            bool r18 = true;
            bool num = true;
            bool keyword = true;
            bool size1200 = true;
            bool proxy = true;

            int iResult;
            foreach (KeyValuePair<string, string> patameter in parameters)
            {
                switch (patameter.Key)
                {
                    case "r18" when r18:
                        if (int.TryParse(patameter.Value, out iResult))
                        {
                            R18 = (R18Type)iResult;
                        }
                        r18 = false;
                        break;
                    case "num" when num:
                        if (int.TryParse(patameter.Value, out iResult))
                        {
                            Num = iResult;
                        }
                        num = false;
                        break;
                    case "keyword" when keyword:
                        Keyword = patameter.Value;
                        keyword = false;
                        break;
                    case "size1200" when size1200:
                        Size1200 = BoolConverter.ToBool(patameter.Value);
                        size1200 = false;
                        break;
                    case "proxy" when proxy:
                        Proxy = patameter.Value;
                        proxy = false;
                        break;
                }
            }
        }

        /// <summary>转换为键值对参数</summary>
        /// <returns>一个键值对数组，包含各属性的键值对形式</returns>
        public KeyValuePair<string, string>[] ToKeyValuePairs()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            //
            if (R18 != R18Type.NonR18)
            {
                result.Add(new KeyValuePair<string, string>("r18", EnumConverter.ToString(R18)));
            }
            //
            if (Num > 1)
            {
                result.Add(new KeyValuePair<string, string>("num", Num.ToString()));
            }
            //
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                result.Add(new KeyValuePair<string, string>("keyword", Keyword));
            }
            //
            if (Size1200)
            {
                result.Add(new KeyValuePair<string, string>("size1200", BoolConverter.ToString(Size1200)));
            }
            //
            if (!string.IsNullOrWhiteSpace(Proxy))
            {
                result.Add(new KeyValuePair<string, string>("proxy", Proxy));
            }
            //
            return result.ToArray();
        }
    }
}
