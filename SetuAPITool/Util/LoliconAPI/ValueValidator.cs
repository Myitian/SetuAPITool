using System.Linq;

namespace Myitian.SetuAPITool.Util.LoliconAPI
{
    /// <summary>值验证器</summary>
    public static class ValueValidator
    {
        private readonly static string[] _sizeStr = { "original", "regular", "small", "thumb", "mini" };

        /// <summary>验证一维标签</summary>
        /// <param name="tags">一维标签</param>
        /// <returns>一维标签是否正确</returns>
        public static bool Validate1DTags(string[] tags)
        {
            if (tags != null)
            {
                if (tags.Length > 3)
                {
                    return false;
                }
                foreach (string orTags in tags)
                {
                    if (orTags.Split('|').Length > 20)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>验证二维标签</summary>
        /// <param name="tags">二维标签</param>
        /// <returns>二维标签是否正确</returns>
        public static bool Validate2DTags(string[][] tags)
        {
            if (tags != null)
            {
                if (tags.Length > 3)
                {
                    return false;
                }
                foreach (string[] orTags in tags)
                {
                    if (orTags.Length > 20)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>验证尺寸</summary>
        /// <param name="size">尺寸</param>
        /// <returns>尺寸是否正确</returns>
        public static bool ValidateSizeString(string size)
        {
            return _sizeStr.Contains(size);
        }
    }
}
