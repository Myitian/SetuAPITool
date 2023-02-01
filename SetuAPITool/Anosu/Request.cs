using Myitian.SetuAPITool.Util;
using System.Collections.Generic;
using System.Linq;

namespace Myitian.SetuAPITool.Anosu
{
    /// <summary>Anosu 请求</summary>
    public class Request
    {
        /// <summary>图片的数量</summary>
        public int Num { get; set; }
        /// <summary>年龄分级</summary>
        public R18Type R18 { get; set; }
        /// <summary>图片尺寸</summary>
        public PixivSize Size { get; set; }
        /// <summary>图片tags所包含的关键字</summary>
        public string Keyword { get; set; }
        /// <summary>图片链接使用的反代地址</summary>
        public string Proxy { get; set; }
        /// <summary>使用的图库（数据库）</summary>
        public int Db { get; set; }
        /// <summary>是否使用POST请求</summary>
        public bool PostRequest { get; set; }

        /// <param name="num">图片的数量</param>
        /// <param name="r18">年龄分级</param>
        /// <param name="keyword">关键字</param>
        public Request(int num = 1, R18Type r18 = R18Type.NonR18, string keyword = null)
        {
            Num = num;
            R18 = r18;
            Keyword = keyword;
        }
        /// <summary>深拷贝已有 <see cref="Request"/></summary>
        /// <param name="request">已有的 <see cref="Request"/></param>
        public Request(Request request)
        {
            Num = request.Num;
            R18 = request.R18;
            Size = request.Size;
            Proxy = request.Proxy;
            Keyword = request.Keyword;
            Db = request.Db;
        }
        /// <summary>从键值对参数创建 <see cref="Request"/></summary>
        /// <param name="parameters">键值对参数</param>
        public Request(params KeyValuePair<string, string>[] parameters)
        {
            int iResult;
            foreach (KeyValuePair<string, string> patameter in parameters)
            {
                switch (patameter.Key)
                {
                    case "r18":
                        R18 = EnumConverter.ToEnum<R18Type>(patameter.Value);
                        break;
                    case "size":
                        Size = EnumConverter.ToEnum<PixivSize>(patameter.Value);
                        break;
                    case "num":
                        if (int.TryParse(patameter.Value, out iResult))
                        {
                            Num = iResult;
                        }
                        break;
                    case "proxy":
                        Proxy = patameter.Value;
                        break;
                    case "keyword":
                        Keyword = patameter.Value;
                        break;
                    case "db":
                        if (int.TryParse(patameter.Value, out iResult))
                        {
                            Db = iResult;
                        }
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
            if (Num > 1)
            {
                result["num"] = Num.ToString();
            }
            if (R18 != R18Type.NonR18)
            {
                result["r18"] = ((int)R18).ToString();
            }
            if (Size != PixivSize.Default)
            {
                result["size"] = EnumConverter.ToString(Size);
            }
            if (!string.IsNullOrWhiteSpace(Proxy))
            {
                result["proxy"] = Proxy;
            }
            if (Db > 0)
            {
                result["db"] = Db.ToString();
            }
            return result;
        }
    }
}
