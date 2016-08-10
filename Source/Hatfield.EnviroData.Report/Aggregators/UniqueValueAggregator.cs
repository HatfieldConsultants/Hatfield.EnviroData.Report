using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report.Aggregators
{
    public class UniqueValueAggregator : IValueAggregator
    {
        public object Calculate(IEnumerable<string> valuePropertyNames, 
                                IEnumerable<Tuple<string, object>> matchingRules, 
                                Dictionary<string, PropertyData> flattenData)
        {
            if(valuePropertyNames == null || valuePropertyNames.Count() == 0)
            {
                return null;
            }

            if(valuePropertyNames.Count() != 1)
            {
                throw new ArgumentException("Unique value aggregator can only accpet one property.");
            }

            var matchedIndex = DecideMatchedIndexOfData(matchingRules, flattenData);
            var matchedValue = GetValueOfProperties(matchedIndex, valuePropertyNames.FirstOrDefault(), flattenData);

            return matchedValue;            
        }

        private int? DecideMatchedIndexOfData(IEnumerable<Tuple<string, object>> matchingRules, Dictionary<string, PropertyData> flattenData)
        {
            var matchIndexesOfProperties = new List<int[]>();

            foreach(var rule in matchingRules)
            {
                if (flattenData.ContainsKey(rule.Item1))
                {
                    var allIndex = FindAllIndexof(flattenData[rule.Item1].Data, rule.Item2);

                    if (allIndex.Length == 0)
                    {
                        return null;
                    }
                    else
                    {
                        matchIndexesOfProperties.Add(allIndex);
                    }
                }
                else
                {
                    return null;
                }
            }

            if(!matchIndexesOfProperties.Any())
            {
                throw new IndexOutOfRangeException("System can not decide index from the matching rule");
            }

            var intersection = matchIndexesOfProperties
                                    .Skip(1)
                                    .Aggregate(
                                        new HashSet<int>(matchIndexesOfProperties.First()),
                                        (h, e) => { h.IntersectWith(e); return h; }
                                    );

            if (intersection.Count == 0)
            {
                return null;
            }
            else
            {
                return intersection.FirstOrDefault();
            }
        }

        private object GetValueOfProperties(int? matchedIndex, string valuePropertyName, Dictionary<string, PropertyData> flattenData)
        {
            if(matchedIndex.HasValue)
            {
                return flattenData[valuePropertyName].Data.ElementAt(matchedIndex.Value);
            }

            return null;            
        }

        private static int[] FindAllIndexof<T>(IEnumerable<T> values, T val)
        {
            return values.Select((b, i) => object.Equals(b, val) ? i : -1).Where(i => i != -1).ToArray();
        }
    }
}
