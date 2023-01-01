using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetuAPITool.LoliconAPI.V1
{
    public class Respond
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
        public Setu[] Data { get; set; }
    }
}
