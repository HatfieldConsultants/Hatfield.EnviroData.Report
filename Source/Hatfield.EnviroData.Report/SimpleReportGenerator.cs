using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Hatfield.EnviroData.Report
{
    public class SimpleReportGenerator : ReportGeneratorBase
    {
        public SimpleReportGenerator(IReportableEntityValidator validator)
            : base(validator)
        { 
        
        }

        public override IReportTable Generate(IEnumerable<object> data, Definition tableDefinition)
        {
            ValidateData(data);

            var flattenData = FlattenData(data);

            throw new NotImplementedException();
        }

        
    }
}
