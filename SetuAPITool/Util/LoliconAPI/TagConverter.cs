using System.Linq;

namespace Myitian.SetuAPITool.Util.LoliconAPI
{
    /// <summary>标签转换器</summary>
    public static class TagConverter
    {
        /// <summary>二维标签转一维标签</summary>
        /// <param name="tags"></param>
        public static string[] TwoDimensions2OneDimension(string[][] tags)
        {
            return tags.Select(tag => string.Join("|", tag)).ToArray();
        }
        /// <summary>一维标签转二维标签</summary>
        public static string[][] OneDimension2TwoDimensions(string[] tags)
        {
            return tags.Select(tag => tag.Split('|')).ToArray();
        }
    }
}
