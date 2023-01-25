using System.Collections.Generic;

namespace SetuAPITool.Util
{
    public enum PixivSize
    {
        Default,
        Original = 0x1,
        Regular = 0x2,
        Small = 0x4,
        Thumb = 0x8,
        Mini = 0x10,

        All = Original | Regular | Small | Thumb | Mini
    }

    public static class PixivSizeExtend
    {
        private readonly static string[] _sizeStr = { "original", "regular", "small", "thumb", "mini" };

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
