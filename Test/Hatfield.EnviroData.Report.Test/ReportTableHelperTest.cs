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
    }
}
