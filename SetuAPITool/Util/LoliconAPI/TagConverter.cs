using System.Linq;

namespace SetuAPITool.Util.LoliconAPI
{
    public static class TagConverter
    {
        public static string[] ThreeDimensions2TwoDimensions(string[][] tags)
        {
            return tags.Select(tag => string.Join("|", tag)).ToArray();
        }
        public static string[][] TwoDimensions2ThreeDimensions(string[] tags)
        {
            return tags.Select(tag => tag.Split('|')).ToArray();
        }
    }
}
