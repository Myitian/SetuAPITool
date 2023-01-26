using SetuAPITool.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SetuAPITool.Jitsu
{
    public enum SizeType
    {
        Original,
        TypeA,
        Custom,
        TypeB
    }

    public class Size
    {
        public string SizeString { get; private set; } = "original";
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

        public static string[] Presets
        {
            get
            {
                string[] arr = new string[presets.Length];
                Array.Copy(presets, arr, presets.Length);
                return arr;
            }
        }

        public Size() { }
        public Size(Size size)
        {
            SizeString = size.SizeString;
            SizeType = size.SizeType;
        }
        public Size(string preset)
        {
            if (string.IsNullOrWhiteSpace(preset) || preset == "original")
            {
                SizeString = "original";
                SizeType = SizeType.Original;
                return;
            }
            else if (presets.Contains(preset))
            {
                SizeType = SizeType.TypeA;
            }
            else if (pixivSizes.Contains(preset))
            {
                SizeType = SizeType.TypeB;
            }
            else
            {
                // 参见 https://cloud.baidu.com/doc/BOS/s/gkbisf3l4
                SizeType = SizeType.Custom;
            }
            SizeString = preset;
        }
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
        public Size(Dictionary<string, string> customResizeParams)
        {
            // 参见 https://cloud.baidu.com/doc/BOS/s/gkbisf3l4
            SizeString = ParametersConverter.ToString(",", "_", customResizeParams.ToArray());
            SizeType = SizeType.Custom;
        }

        public override string ToString()
        {
            return SizeString;
        }

        public override int GetHashCode()
        {
            return SizeString.GetHashCode() ^ (int)SizeType;
        }

        public override bool Equals(object obj)
        {
            return obj is Size size
                && SizeString == size.SizeString
                && SizeType == size.SizeType;
        }
    }
}
