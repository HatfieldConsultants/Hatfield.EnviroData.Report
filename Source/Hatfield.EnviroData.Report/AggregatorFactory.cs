using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hatfield.EnviroData.Report.Aggregators;

namespace Hatfield.EnviroData.Report
{
    public class AggregatorFactory
    {
        public static IValueAggregator Build(string aggregatorName)
        {
            return new UniqueValueAggregator();
        }
    }
}
