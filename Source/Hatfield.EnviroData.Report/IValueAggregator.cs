using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public interface IValueAggregator
    {
        object Calculate(IEnumerable<string> valuePropertyNames, IEnumerable<Tuple<string, object>> matchingRules, Dictionary<string, PropertyData> flattenData);
    }
}
