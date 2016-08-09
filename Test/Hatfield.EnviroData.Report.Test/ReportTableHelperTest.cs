using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Hatfield.EnviroData.Report.Test
{
    [TestFixture]
    public class ReportTableHelperTest
    {   

        [Test]
        [TestCaseSource("maxDepthTestCases")]
        public void GetMaxDepthOfHeadersTest(IEnumerable<IReportHeader> testHeaders, int expectedDepth)
        {
            Assert.AreEqual(expectedDepth, ReportTableHelper.GetMaxDepthOfHeaders(testHeaders));
        }

        [Test]
        [TestCaseSource("maxWidthTestCases")]
        public void GetMaxWidthOfHeadersTest(IEnumerable<IReportHeader> testHeaders, int expectedWidth)
        {
            Assert.AreEqual(expectedWidth, ReportTableHelper.GetMaxWidthOfHeaders(testHeaders));
        }

        [Test]        
        public void GetValueOfLevelsTest()
        {
            var testArray = new List<IEnumerable<IReportHeader>> { testHeader0, testHeader1, testHeader2, testHeader3 };

            foreach(var element in testArray)
            {
                var actualValues = ReportTableHelper.GetValueOfLevels(element.FirstOrDefault());

                foreach (var key in actualValues.Keys)
                {
                    var values = actualValues[key];
                    Assert.AreEqual(1, values.Count);
                    Assert.AreEqual("header" + key.ToString(), values.First().CellValue.Value);
                }
            }
            
        }

        [Test]
        public void GetPathByLeafIndexTest()
        {
            var testHeader = new ReportHeader("Name", new Cell(typeof(string), "header0"));
            var subHeaderInLevel1 = new ReportHeader("Name", new Cell(typeof(string), "header1"));
            var subHeaderInLevel2 = new ReportHeader("Name", new Cell(typeof(string), "header2"));

            testHeader.AddSubHeader(subHeaderInLevel1);
            subHeaderInLevel1.AddSubHeader(subHeaderInLevel2);

            var actualPath = ReportTableHelper.GetPathByLeafIndex(new List<IReportHeader>{ testHeader }, 0);

            Assert.AreEqual(3, actualPath.Count());
            for (var i = 0; i < actualPath.Count(); i++)
            {
                Assert.AreEqual("header" + i.ToString(), actualPath.ElementAt(i).CellValue.Value);
            }
        }

        static IEnumerable<IReportHeader> testHeader0 = new List<IReportHeader> { 
                    new ReportHeader("Name", new Cell(typeof(string), "header0"))
        };

        static IEnumerable<IReportHeader> testHeader1 = new List<IReportHeader> { 
                    new ReportHeader("Name", new Cell(typeof(string), "header0"), new List<IReportHeader>{})
                };
        static IEnumerable<IReportHeader> testHeader2 = new List<IReportHeader> { 
                    new ReportHeader("Name", 
                                    new Cell(typeof(string), "header0"), 
                                    new List<IReportHeader>{
                                        new ReportHeader("Gender", new Cell(typeof(string), "header1"))
                                    })
                };
        static IEnumerable<IReportHeader> testHeader3 = new List<IReportHeader> { 
                    new ReportHeader("Name", 
                                    new Cell(typeof(string), "header0"), 
                                    new List<IReportHeader>{
                                        new ReportHeader("Gender", 
                                                        new Cell(typeof(string), "header1"), 
                                                        new List<IReportHeader>{
                                                            new ReportHeader("Name", new Cell(typeof(string), "header2"))
                                                        })
                                    })
                };

        static object[] maxDepthTestCases = new object[] { 
            new object[] {
                null,
                0
            },
            new object[] {
                new List<IReportHeader>(),
                0
            },
            new object[] {
                testHeader0,
                1
            },
            new object[] {
                testHeader1,
                1
            },
            new object[] {
                testHeader2,
                2
            },
            new object[] {
                testHeader3,
                3
            }
        };

        static object[] maxWidthTestCases = new object[] { 
            new object[] {
                null,
                1
            },
            new object[] {
                new List<IReportHeader>(),
                1
            },
            new object[] {
                testHeader0,
                1
            },
            new object[] {
                testHeader1,
                1
            },
            new object[] {
                testHeader2,
                1
            },
            new object[] {
                testHeader3,
                1
            }
        };
    }
}
