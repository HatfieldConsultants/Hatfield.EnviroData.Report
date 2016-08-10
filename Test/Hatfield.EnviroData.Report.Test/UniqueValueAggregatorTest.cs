using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Hatfield.EnviroData.Report.Aggregators;

namespace Hatfield.EnviroData.Report.Test
{
    [TestFixture]
    public class UniqueValueAggregatorTest
    {
        [Test]
        public void InvalidPropertyNameTest()
        {
            var calculator = new UniqueValueAggregator();

            Assert.IsNull(calculator.Calculate(null, null, null));
            Assert.IsNull(calculator.Calculate(new List<string>(), null, null));

            Assert.Throws<ArgumentException>(() => calculator.Calculate(new List<string> { "Name", "Age" }, null, null));
            Assert.Throws<IndexOutOfRangeException>(() => calculator.Calculate(new List<string> { "Name" }, new List<Tuple<string, object>>(), null));
        }

        [Test]
        [TestCaseSource("CalculateTestCases")]
        public void CalculateTest(IEnumerable<string> valuePropertyNames,
                                IEnumerable<Tuple<string, object>> matchingRules,
                                Dictionary<string, PropertyData> flattenData,
                                object expectedValue)
        {
            var calculator = new UniqueValueAggregator();
            var actualValue = calculator.Calculate(valuePropertyNames, matchingRules, flattenData);


            Assert.AreEqual(expectedValue, actualValue);
        
        }

        static object[] CalculateTestCases = new object[] 
        { 
            new object[]
            {
                new List<string>{ "Age" },
                new List<Tuple<string, object>>
                {
                    Tuple.Create<string, object>("Name", "Jack"),
                    Tuple.Create<string, object>("Gender", "Male"),
                },
                new Dictionary<string, PropertyData>
                {
                
                },
                null
            },
            new object[]
            {
                new List<string>{ "Age" },
                new List<Tuple<string, object>>
                {
                    Tuple.Create<string, object>("Name", "Jack"),
                    Tuple.Create<string, object>("Gender", "Male"),
                },
                new Dictionary<string, PropertyData>
                {
                    {"Name", new PropertyData("Name", typeof(string), new List<object>{"Peter", "Jack", "Amy", "Jack"}) },
                    {"Gender", new PropertyData("Gender", typeof(string), new List<object>{"Male", "Female", "Female", "Male"}) },
                    {"Age", new PropertyData("Age", typeof(int), new List<object>{1, 2, 3, 4}) },
                },
                4
            },
            new object[]
            {
                new List<string>{ "Age" },
                new List<Tuple<string, object>>
                {
                    Tuple.Create<string, object>("Name", "Jack"),
                    Tuple.Create<string, object>("Gender", "Male"),
                },
                new Dictionary<string, PropertyData>
                {
                    {"Name", new PropertyData("Name", typeof(string), new List<object>{"Peter", "Jack", "Amy", "Jack", "Jack"}) },
                    {"Gender", new PropertyData("Gender", typeof(string), new List<object>{"Male", "Female", "Female", "Male", "Male"}) },
                    {"Age", new PropertyData("Age", typeof(int), new List<object>{1, 2, 3, 4, 4}) },
                },
                4
            }
        };


    }
}
