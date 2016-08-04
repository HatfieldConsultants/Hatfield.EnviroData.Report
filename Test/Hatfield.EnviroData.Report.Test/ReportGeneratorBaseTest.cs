using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Hatfield.EnviroData.Report.Test
{
    [TestFixture]
    public class ReportGeneratorBaseTest
    {
        [Test]
        public void ValidateDataSuccessTest()
        {
            var validator = new ReportableEntityValidator();
            var testDefinition = new Definition();

            var testReportGenerator = new TestReportGenerator(validator);

            var entitiesInTestClass = new List<TestClass> { new TestClass() };

            var table = testReportGenerator.Generate(entitiesInTestClass, testDefinition);

            Assert.Null(table);
        }

        [Test]
        public void ValidateDataFailTest()
        {
            var validator = new ReportableEntityValidator();
            var testDefinition = new Definition();

            var testReportGenerator = new TestReportGenerator(validator);

            var entitiesInTestClass = new List<TestInvalidClass> { new TestInvalidClass() };

            Assert.Throws<NotSupportedException>(() => testReportGenerator.Generate(entitiesInTestClass, testDefinition), "Data is not supported by the report generator.");
            Assert.Throws<NullReferenceException>(() => testReportGenerator.Generate(null, testDefinition), "No data for report generator to process.");
            Assert.Throws<NullReferenceException>(() => testReportGenerator.Generate(new List<TestClass>(), testDefinition), "No data for report generator to process.");

            var notSameTestData = new List<object> { new TestClass(), new TestInvalidClass() };
            Assert.Throws<ArgumentException>(() => testReportGenerator.Generate(notSameTestData, testDefinition), "Data fro report generator is not in the same type.");
        }

        [Test]
        public void FlattenDataTest()
        {
            var validator = new ReportableEntityValidator();
            var testDefinition = new Definition();
            var testReportGenerator = new TestReportGenerator(validator);
            var testData = new List<TestReportEntity> 
            { 
                new TestReportEntity
                {
                    Age = 1,
                    Gender = "Male",
                    Name = "Peter",
                    Province = "B.C."
                },
                new TestReportEntity
                {
                    Age = 1,
                    Gender = "Female",
                    Name = "Jane",
                    Province = "B.C."
                },
                new TestReportEntity
                {
                    Age = 2,
                    Gender = "Male",
                    Name = "Jack",
                    Province = "A.B."
                },
                new TestReportEntity
                {
                    Age = 3,
                    Gender = "Female",
                    Name = "Sammy",
                    Province = "O.N."
                }
            };

            var flatternData = testReportGenerator.TestFlattenData(testData);

            Assert.NotNull(flatternData);
            Assert.AreEqual(4, flatternData.Count());

            AssertPropertyData(flatternData.ElementAt(0).Value, "Name", typeof(string), new List<object> { "Peter", "Jane", "Jack", "Sammy" });
            AssertPropertyData(flatternData.ElementAt(1).Value, "Gender", typeof(string), new List<object> { "Male", "Female", "Male", "Female" });
            AssertPropertyData(flatternData.ElementAt(2).Value, "Age", typeof(int), new List<object> { 1, 1, 2, 3 });
            AssertPropertyData(flatternData.ElementAt(3).Value, "Province", typeof(string), new List<object> { "B.C.", "B.C.", "A.B.", "O.N." });
        }

        private void AssertPropertyData(PropertyData data, string actualName, Type actualType, IEnumerable<object> actualData)
        {
            Assert.AreEqual(actualName, data.Name);
            Assert.AreEqual(actualType, data.Type);
            Assert.AreEqual(actualData, data.Data);
        }
    }


    public class TestReportGenerator : ReportGeneratorBase {

        public TestReportGenerator(IReportableEntityValidator validator)
            : base(validator)
        { 
        
        }

        public override IReportTable Generate(IEnumerable<object> data, Definition tableDefinition)
        {
            this.ValidateData(data);
            return null;
        }

        public Dictionary<string, PropertyData> TestFlattenData(IEnumerable<object> data)
        {
            return this.FlattenData(data);
        }
    }
}
