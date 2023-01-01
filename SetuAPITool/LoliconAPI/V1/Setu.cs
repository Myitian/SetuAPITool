using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.LoliconAPI.V1
{
    public class Setu
    {
        public int Pid { get; set; }
        public int P { get; set; }
        public int Uid { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool R18 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string[] Tags { get; set; }
        public string Url { get; set; }
    }
}
