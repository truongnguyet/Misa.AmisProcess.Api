using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisaWebApi.Models
{
    public class Page
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<Process> Items { get; set; }
    }
}
