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
        static object[] reportHeaderTestCases = new object[] { 
            new object[] {
                null,
                0
            },
            new object[] {
                new List<IReportHeader>(),
                0
            },
            new object[] {
                new List<IReportHeader> { 
                    new ReportHeader("Name", new Cell(typeof(string), "header0"))
                },
                1
            },
            new object[] {
                new List<IReportHeader> { 
                    new ReportHeader("Name", new Cell(typeof(string), "header0"), new List<IReportHeader>{})
                },
                1
            },
            new object[] {
                new List<IReportHeader> { 
                    new ReportHeader("Name", 
                                    new Cell(typeof(string), "header0"), 
                                    new List<IReportHeader>{
                                        new ReportHeader("Gender", new Cell(typeof(string), "header1"))
                                    })
                },
                2
            },
            new object[] {
                new List<IReportHeader> { 
                    new ReportHeader("Name", 
                                    new Cell(typeof(string), "header0"), 
                                    new List<IReportHeader>{
                                        new ReportHeader("Gender", 
                                                        new Cell(typeof(string), "header1"), 
                                                        new List<IReportHeader>{
                                                            new ReportHeader("Name", new Cell(typeof(string), "header2"))
                                                        })
                                    })
                },
                3
            }
        };

        [Test]
        [TestCaseSource("reportHeaderTestCases")]
        public void GetMaxDepthOfHeadersTest(IEnumerable<IReportHeader> testHeaders, int expectedDepth)
        {
            Assert.AreEqual(expectedDepth, ReportTableHelper.GetMaxDepthOfHeaders(testHeaders));
        }
    }
}
