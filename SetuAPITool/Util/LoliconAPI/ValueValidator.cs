using System.Linq;

namespace SetuAPITool.Util.LoliconAPI
{
    public static class ValueValidator
    {
        private readonly static string[] _sizeStr = { "original", "regular", "small", "thumb", "mini" };

        public static bool Validate2DTags(string[] tags)
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
        public static bool Validate3DTags(string[][] tags)
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
        public static bool ValidateSizeString(string size)
        {
            return _sizeStr.Contains(size);
        }
    }
}
