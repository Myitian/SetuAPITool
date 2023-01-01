using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.LoliconAPI.V1
{
    public class Request
    {
        public R18Type R18 { get; set; }
        public string Keyword { get; set; }
        public int Num { get; set; }
        public string Proxy { get; set; }
        public bool Size1200 { get; set; }
    }
}
