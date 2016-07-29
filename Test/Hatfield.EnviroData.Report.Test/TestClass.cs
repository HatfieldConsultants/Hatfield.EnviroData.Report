using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report.Test
{
    public class TestClass
    {
        public int IntData { get; set; }
        public float FloatData { get; set; }
        public string StringData { get; set; }
        public bool BooleanData { get; set; }
        public DateTime DateTimeData { get; set; }
    }

    public class TestInvalidClass
    {
        public IEnumerable<int> Data { get; set; }
    }

    public class AnotherTestInvalidClass
    {
        public TestInvalidClass Data { get; set; }
    }
}
