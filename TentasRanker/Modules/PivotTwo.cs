using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentasRanker.Modules
{
    internal class PivotTwo
    {
        public string first { get; set; }
        public string second { get; set; }

        public PivotTwo()
        {
            first = string.Empty;
            second = string.Empty;
        }

        public PivotTwo(string first, string second)
        {
            this.first = first;
            this.second = second;
        }
    }
}
