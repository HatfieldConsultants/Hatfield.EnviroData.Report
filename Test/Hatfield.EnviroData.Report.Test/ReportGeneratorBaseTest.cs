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
    }
}
