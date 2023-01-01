using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.LoliconAPI.V2
{
    public enum Size
    {
        Default,
        Original = 0x1,
        Regular = 0x2,
        Small = 0x4,
        Thumb = 0x8,
        Mini = 0x10,

        All = Original | Regular | Small | Thumb | Mini
    }
}
