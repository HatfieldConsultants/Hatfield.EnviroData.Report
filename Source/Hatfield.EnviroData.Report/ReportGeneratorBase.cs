using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Hatfield.EnviroData.Report
{
    public abstract class ReportGeneratorBase : IReportGenerator
    {
        public virtual IReportTable Generate(IEnumerable<ReportableEntityBase> data, Definition tableDefinition)
        {
            ValidateData(data);

            return this.Generate(data, tableDefinition);

        }

        protected void ValidateData(IEnumerable<ReportableEntityBase> data)
        { 
            if(!data.Any())
            {
                throw new NullReferenceException("No data for report generator to process.");
            }

            if(data.Any(x => !x.IsValid()))
            {
                throw new NotSupportedException("Data is not supported by the report generator.");
            }
            
        }


    }
}
