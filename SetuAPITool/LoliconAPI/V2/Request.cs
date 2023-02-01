using Myitian.SetuAPITool.Util;
using Myitian.SetuAPITool.Util.LoliconAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Myitian.SetuAPITool.LoliconAPI.V2
{
    /// <summary>Lolicon API V2 请求</summary>
    public class Request
    {
        /// <summary>在库中的分类，不等同于作品本身的 R18 标识</summary>
        [JsonProperty("r18", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public R18Type R18 { get; set; }

        /// <summary>一次返回的结果数量，范围为 <c>q</c> 到 <c>20</c> ；在指定关键字或标签的情况下，结果数量可能会不足指定的数量</summary>
        [JsonProperty("num", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Num { get; set; } = 1;

        /// <summary>返回指定 <c>uid</c> 作者的作品，最多20个</summary>
        [JsonProperty("uid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int[] Uid { get; set; }

        /// <summary>返回从标题、作者、标签中按指定关键字模糊匹配的结果，大小写不敏感，性能和准度较差且功能单一，建议使用 <see cref="Tag"/> 代替</summary>
        [JsonProperty("keyword", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Keyword { get; set; }

        /// <summary>返回匹配指定标签的作品</summary>
        [JsonProperty("tag", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[][] Tag { get; set; }

        /// <summary>返回指定图片规格的地址</summary>
        [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(PixivSizeConverter))]
        public PixivSize Size { get; set; }

        /// <summary>设置图片地址所使用的在线反代服务</summary>
        [JsonProperty("proxy", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Proxy { get; set; }

        /// <summary>返回在这个时间及以后上传的作品</summary>
        [JsonProperty("dateAfter", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(UnixTimeMillisecondConverter))]
        public DateTime DateAfter { get; set; }

        /// <summary>返回在这个时间及以前上传的作品</summary>
        [JsonProperty("dateBefore", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(UnixTimeMillisecondConverter))]
        public DateTime DateBefore { get; set; }

        /// <summary>禁用对某些缩写 <see cref="Keyword"/> 和 <see cref="Tag"/> 的自动转换</summary>
        [JsonProperty("dsc", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Dsc { get; set; }

        /// <summary>排除 AI 作品</summary>
        [JsonProperty("excludeAI", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool ExcludeAI { get; set; }

        /// <summary>是否使用POST请求</summary>
        [JsonIgnore]
        public bool PostRequest { get; set; }

        ///
        public Request() { }
        /// <summary>从键值对参数创建 <see cref="Request"/></summary>
        /// <param name="parameters">键值对参数</param>
        public Request(params KeyValuePair<string, string>[] parameters)
        {
            if (parameters == null)
            {
                return;
            }
            bool r18 = true;
            bool num = true;
            HashSet<int> uid = new HashSet<int>();
            bool keyword = true;
            HashSet<string> tag = new HashSet<string>();
            bool proxy = true;
            bool dateAfter = true;
            bool dateBefore = true;
            bool dsc = true;
            bool excludeAI = true;

            int iResult;
            long lResult;
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
                    case "uid":
                        if (int.TryParse(patameter.Value, out iResult))
                        {
                            uid.Add(iResult);
                        }
                        break;
                    case "keyword" when keyword:
                        Keyword = patameter.Value;
                        keyword = false;
                        break;
                    case "tag":
                        if (patameter.Value != null)
                        {
                            tag.Add(patameter.Value);
                        }
                        break;
                    case "size":
                        Size |= EnumConverter.ToEnum<PixivSize>(patameter.Value);
                        break;
                    case "proxy" when proxy:
                        Proxy = patameter.Value;
                        proxy = false;
                        break;
                    case "dateAfter" when dateAfter:
                        if (long.TryParse(patameter.Value, out lResult))
                        {
                            DateAfter = Time.FromUnixTimeMillisecond(lResult);
                        }
                        dateAfter = false;
                        break;
                    case "dateBefore" when dateBefore:
                        if (long.TryParse(patameter.Value, out lResult))
                        {
                            DateBefore = Time.FromUnixTimeMillisecond(lResult);
                        }
                        dateBefore = false;
                        break;
                    case "dsc" when dsc:
                        Dsc = BoolConverter.ToBool(patameter.Value);
                        dsc = false;
                        break;
                    case "excludeAI" when excludeAI:
                        ExcludeAI = BoolConverter.ToBool(patameter.Value);
                        excludeAI = false;
                        break;
                    case "postrequest":
                        PostRequest = BoolConverter.ToBool(patameter.Value);
                        break;
                }
            }

            Uid = new int[uid.Count];
            uid.CopyTo(Uid);

            string[] tag2d = new string[tag.Count];
            tag.CopyTo(tag2d);
            Tag = TagConverter.OneDimension2TwoDimensions(tag2d);
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
            if (Uid != null && Uid.Length > 0)
            {
                foreach (int uid in Uid)
                {
                    result.Add(new KeyValuePair<string, string>("uid", uid.ToString()));
                }
            }
            //
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                result.Add(new KeyValuePair<string, string>("keyword", Keyword));
            }
            //
            if (Tag != null && Tag.Length > 0)
            {
                foreach (string tag in TagConverter.TwoDimensions2OneDimension(Tag))
                {
                    if (!string.IsNullOrWhiteSpace(tag))
                    {
                        result.Add(new KeyValuePair<string, string>("tag", tag));
                    }
                }
            }
            //
            if (Size != PixivSize.Default || Size != PixivSize.Original)
            {
                string[] sizes = Size.ToStrings();
                foreach (string size in sizes)
                {
                    result.Add(new KeyValuePair<string, string>("size", size));
                }
            }
            //
            if (!string.IsNullOrWhiteSpace(Proxy))
            {
                result.Add(new KeyValuePair<string, string>("proxy", Proxy));
            }
            //
            long l = Time.ToUnixTimeMillisecond(DateAfter);
            if (l > 0)
            {
                result.Add(new KeyValuePair<string, string>("dateAfter", l.ToString()));
            }
            //
            l = Time.ToUnixTimeMillisecond(DateBefore);
            if (l > 0)
            {
                result.Add(new KeyValuePair<string, string>("dateBefore", l.ToString()));
            }
            //
            if (Dsc)
            {
                result.Add(new KeyValuePair<string, string>("dsc", BoolConverter.ToString(Dsc)));
            }
            //
            if (ExcludeAI)
            {
                result.Add(new KeyValuePair<string, string>("excludeAI", BoolConverter.ToString(ExcludeAI)));
            }
            //
            return result.ToArray();
        }
    }
}