using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public class Definition
    {
        public IEnumerable<string> Cols { get; set; }
        public IEnumerable<string> Rows { get; set; }
        public IEnumerable<string> Vals { get; set; }
        public string AggregatorName { get; set; }
        public string RendererName { get; set; }
    }
}
