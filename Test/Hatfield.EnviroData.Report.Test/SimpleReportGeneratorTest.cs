using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Hatfield.EnviroData.Report.Writers;

namespace Hatfield.EnviroData.Report.Test
{
    [TestFixture]
    public class SimpleReportGeneratorTest
    {
        [Test]
        public void GenerateTest()
        {
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

            var definition = new Definition
            {
                Cols = new List<string> { "Province", "Gender" },
                Rows = new List<string> { "Name", "Age" },
                Vals = new List<string> { "Age" },
                AggregatorName = "List Unique Values",
                RendererName = "Table"
            };

            var generator = new SimpleReportGenerator(new ReportableEntityValidator());

            var resultTable = generator.Generate(testData, definition);

            Assert.NotNull(resultTable);

            var htmlWriter = new HtmlWriter();
            var html = htmlWriter.Write(resultTable);
            Assert.NotNull(html);
        }
    }
}
