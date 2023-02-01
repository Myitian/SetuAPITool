using System.Collections.Generic;

namespace Myitian.SetuAPITool
{
    /// <summary>Pixiv图像尺寸</summary>
    public enum PixivSize
    {
        /// <summary>默认</summary>
        Default,

        /// <summary>原始</summary>
        Original = 0x1,
        /// <summary>标准</summary>
        Regular = 0x2,
        /// <summary>小</summary>
        Small = 0x4,
        /// <summary>缩略图</summary>
        Thumb = 0x8,
        /// <summary>迷你</summary>
        Mini = 0x10,

        /// <summary>所有</summary>
        All = Original | Regular | Small | Thumb | Mini
    }

    /// <summary>Pixiv图像尺寸扩展</summary>
    public static class PixivSizeExtend
    {
        private readonly static string[] _sizeStr = { "original", "regular", "small", "thumb", "mini" };

        /// <summary>Pixiv图像尺寸转字符串列表</summary>
        public static string[] ToStrings(this PixivSize size)
        {
            List<string> result = new List<string>(5);
            int j = 0;
            for (int i = (int)size & 0b11111; i != 0; i >>= 1)
            {
                if ((i & 1) == 1)
                {
                    result.Add(_sizeStr[j]);
                }
                j++;
            }
            return result.ToArray();
        }
    }
}
