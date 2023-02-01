using Myitian.SetuAPITool.Util;
using System.Collections.Generic;
using System.Linq;

namespace Myitian.SetuAPITool.Jitsu
{
    /// <summary>Jitsu 请求</summary>
    public class Request
    {
        /// <summary>图片的分类</summary>
        public Sort Sort { get; set; }
        /// <summary>图片的规格</summary>
        public Size Size { get; set; }
        /// <summary>返回的数量</summary>
        public int Num { get; set; }
        /// <summary>图片链接所使用的反代地址（仅 <c><see cref="Sort"/> &gt;= <see cref="Sort.Pixiv"/> &amp;&amp; <see cref="Sort"/> &lt; <see cref="Sort.SpR18"/></c> 时有效）</summary>
        public string Proxy { get; set; }
        /// <summary>请求的服务器</summary>
        public Source Source { get; set; }
        /// <summary>是否使用POST请求</summary>
        public bool PostRequest { get; set; }

        /// <param name="num">图片的数量</param>
        /// <param name="sort">图片的分类</param>
        public Request(int num = 1, Sort sort = Sort.Default)
        {
            Num = num;
            Sort = sort;
        }
        /// <summary>深拷贝已有 <see cref="Request"/></summary>
        /// <param name="request">已有的 <see cref="Request"/></param>
        public Request(Request request)
        {
            Sort = request.Sort;
            Size = new Size(request.Size);
            Num = request.Num;
            Proxy = request.Proxy;
            Source = request.Source;
            PostRequest = request.PostRequest;
        }
        /// <summary>从键值对参数创建 <see cref="Request"/></summary>
        /// <param name="parameters">键值对参数</param>
        public Request(params KeyValuePair<string, string>[] parameters)
        {
            foreach (KeyValuePair<string, string> patameter in parameters)
            {
                switch (patameter.Key)
                {
                    case "sort":
                        Sort = EnumConverter.ToEnum<Sort>(patameter.Value);
                        break;
                    case "size":
                        Size = new Size(patameter.Value);
                        break;
                    case "num":
                        if (int.TryParse(patameter.Value, out int iResult))
                        {
                            Num = iResult;
                        }
                        break;
                    case "proxy":
                        Proxy = patameter.Value;
                        break;
                    case "source":
                        Source = EnumConverter.ToEnum<Source>(patameter.Value);
                        break;
                    case "postrequest":
                        PostRequest = BoolConverter.ToBool(patameter.Value);
                        break;
                }
            }
        }

        /// <summary>转换为键值对参数</summary>
        /// <returns>一个键值对数组，包含请求所需属性的键值对形式</returns>
        public KeyValuePair<string, string>[] ToKeyValuePairs()
        {
            return ToRequestDictionary().ToArray();
        }
        /// <summary>转换为字典</summary>
        /// <returns>一个字典，包含请求所需属性的键值对形式</returns>
        public Dictionary<string, string> ToRequestDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (Sort.Default < Sort && Sort < Sort.SpR18)
            {
                result["sort"] = EnumConverter.ToString(Sort);
            }
            if (Size.SizeType != SizeType.Original)
            {
                result["size"] = Size.SizeString;
            }
            if (!string.IsNullOrWhiteSpace(Proxy) && (Size.SizeType == SizeType.Original || Size.SizeType == SizeType.TypeB))
            {
                result["proxy"] = Proxy;
            }
            if (Num > 1)
            {
                result["num"] = Num.ToString();
            }
            if (!string.IsNullOrWhiteSpace(Proxy))
            {
                result["proxy"] = Proxy;
            }
            return result;
        }
    }
}
