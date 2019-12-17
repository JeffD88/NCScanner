using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCScanner.DataTypes
{
    public class NCData
    {
        public List<string> Tools { get; set; }

        public string ToolString { get; set; }

        public List<string> WorkOffsets { get; set; }

        public string WorkOffsetString { get; set; }

        public double XMin { get; set; }

        public double YMin { get; set; }

        public double ZMin { get; set; }

        public double XMax { get; set; }

        public double YMax { get; set; }

        public double ZMax { get; set; }

        public NCData()
        {
            Tools = new List<string>();
            WorkOffsets = new List<string>();
        }
    }
}
