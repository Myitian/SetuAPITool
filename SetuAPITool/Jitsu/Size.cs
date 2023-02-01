using Myitian.SetuAPITool.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Myitian.SetuAPITool.Jitsu
{
    /// <summary>图片规格类型</summary>
    public enum SizeType
    {
        /// <summary>原始</summary>
        Original,
        /// <summary>第一类</summary>
        TypeA,
        /// <summary>自定义</summary>
        Custom,
        /// <summary>第二类</summary>
        TypeB
    }

    /// <summary>图片的规格</summary>
    public class Size
    {
        /// <summary>图片规格字符串</summary>
        public string SizeString { get; private set; } = "original";
        /// <summary>图片规格类型</summary>
        public SizeType SizeType { get; private set; } = SizeType.Original;

        private static readonly string[] presets = {
            "mw4096",
            "mw3840",
            "mw2048",
            "mw1920",
            "mw1024",
            "mw960",
            "mw512",
            "mw480",
            "mw256",
            "mw240",
            "mw128",
            "mw120",

            "2160p",
            "1440p",
            "1080p",
            "720p",
            "480p",

            "sq2048",
            "sq1024",
            "sq512",
            "sq256",
            "sq128"
        };
        private static readonly string[] pixivSizes = { "regular", "small", "thumb", "mini" };

        /// <summary>图片规格预设</summary>
        public static string[] Presets
        {
            get
            {
                string[] arr = new string[presets.Length];
                Array.Copy(presets, arr, presets.Length);
                return arr;
            }
        }

        ///
        public Size() { }
        /// <summary>深拷贝已有 <see cref="Size"/></summary>
        /// <param name="size">已有的 <see cref="Size"/></param>
        public Size(Size size)
        {
            SizeString = size.SizeString;
            SizeType = size.SizeType;
        }
        /// <summary>从字符串创建图片规格</summary>
        /// <param name="size">图片规格</param>
        public Size(string size)
        {
            if (string.IsNullOrWhiteSpace(size) || size == "original")
            {
                SizeString = "original";
                SizeType = SizeType.Original;
                return;
            }
            else if (presets.Contains(size))
            {
                SizeType = SizeType.TypeA;
            }
            else if (pixivSizes.Contains(size))
            {
                SizeType = SizeType.TypeB;
            }
            else
            {
                SizeType = SizeType.Custom;
            }
            SizeString = size;
        }
        /// <summary>从 <see cref="PixivSize"/> 创建图片规格</summary>
        /// <param name="pixivSize">图片规格</param>
        public Size(PixivSize pixivSize)
        {
            SizeString = EnumConverter.ToString(pixivSize);
            if (pixivSizes.Contains(SizeString))
            {
                SizeType = SizeType.TypeB;
            }
            else
            {
                SizeString = "original";
                SizeType = SizeType.Original;
            }
        }
        /// <summary>
        /// 从自定义缩放参数创建图片规格
        /// <para>参见 <see href="https://cloud.baidu.com/doc/BOS/s/gkbisf3l4"/></para></summary>
        /// <param name="customResizeParams">自定义缩放参数</param>
        public Size(Dictionary<string, string> customResizeParams)
        {
            SizeString = ParametersConverter.ToString(",", "_", customResizeParams.ToArray());
            SizeType = SizeType.Custom;
        }

        /// <summary>转换为字符串形式</summary>
        public override string ToString()
        {
            return SizeString;
        }
        ///
        public override int GetHashCode()
        {
            return SizeString.GetHashCode() ^ (int)SizeType;
        }
        ///
        public override bool Equals(object obj)
        {
            return obj is Size size
                && SizeString == size.SizeString
                && SizeType == size.SizeType;
        }
    }
}
