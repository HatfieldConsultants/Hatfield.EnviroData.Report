using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Hatfield.EnviroData.Report.Test
{
    [TestFixture]
    public class ReportableEntityValidatorTest
    {
        [Test]
        public void ValidateTest()
        {
            var validator = new ReportableEntityValidator();

            var entityInTestClass = new TestClass();
            Assert.True(validator.IsSupported(entityInTestClass));

            var entityInTestInvalidClass = new TestInvalidClass();
            Assert.False(validator.IsSupported(entityInTestInvalidClass));

            var entityInAnotherTestInvalidClass = new AnotherTestInvalidClass();
            Assert.False(validator.IsSupported(entityInAnotherTestInvalidClass));
        }
    }
}
