using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SetuAPITool.Util.LoliconAPI
{
    public static class ValueValidator
    {
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
    }
}
