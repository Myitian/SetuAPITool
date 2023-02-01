using Myitian.SetuAPITool.Util;
using System.Collections.Generic;
using System.Linq;

namespace Myitian.SetuAPITool.MirlKoi
{
    /// <summary>MirlKoi 请求</summary>
    public class Request
    {
        /// <summary>分类</summary>
        public Sort Sort { get; set; }
        /// <summary>图片数量(最高100)</summary>
        public int Num { get; set; }

        /// <param name="num">图片数量</param>
        /// <param name="sort">分类</param>
        public Request(int num = 1, Sort sort = Sort.Random)
        {
            Num = num;
            Sort = sort;
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
                    case "num":
                        if (int.TryParse(patameter.Value, out int iResult))
                        {
                            Num = iResult;
                        }
                        break;
                }
            }
        }

        /// <summary>转换为键值对参数</summary>
        /// <returns>一个键值对数组，包含各属性的键值对形式</returns>
        public KeyValuePair<string, string>[] ToKeyValuePairs()
        {
            return ToDictionary().ToArray();
        }
        /// <summary>转换为字典</summary>
        /// <returns>一个字典，包含请求所需属性的键值对形式</returns>
        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>
            {
                { "sort", EnumConverter.ToString(Sort) }
            };
            if (Num > 1)
            {
                result["num"] = Num.ToString();
            }
            return result;
        }
    }
}
