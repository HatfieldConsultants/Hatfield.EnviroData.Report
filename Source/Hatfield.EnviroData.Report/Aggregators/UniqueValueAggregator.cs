using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report.Aggregators
{
    public class UniqueValueAggregator : IValueAggregator
    {
        public object Calculate(IEnumerable<string> valuePropertyNames, IEnumerable<Tuple<string, object>> matchingRules, IEnumerable<PropertyData> flattenData)
        {
            return "N/A";
            //throw new NotImplementedException();
        }
    }
}
