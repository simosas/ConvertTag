using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertTagWF
{
    public class UniversalTag
    {
        // name - type - isArray(?) - array min - array max - kintamuju sarasas - atminties uzemimas (bit)
        public string Name { get; set; }
        public string Type { get; set; }
        public int ArrayMin { get; set; }
        public int ArrayMax { get; set; }
        public UniversalTag[] TagList { get; set; }
        public int MemUsage { get; set; }
    }
}
