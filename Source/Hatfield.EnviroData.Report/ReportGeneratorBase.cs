﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Hatfield.EnviroData.Report
{
    public abstract class ReportGeneratorBase : IReportGenerator
    {
        protected IReportableEntityValidator _validator;
        
        public ReportGeneratorBase(IReportableEntityValidator validator)
        {
            _validator = validator;
        }

        public virtual IReportTable Generate(IEnumerable<object> data, Definition tableDefinition)
        {
            ValidateData(data);

            return this.Generate(data, tableDefinition);

        }

        protected void ValidateData(IEnumerable<object> data)
        { 
            if(!data.Any())
            {
                throw new NullReferenceException("No data for report generator to process.");
            }

            if (!_validator.IsSupported(data))
            {
                throw new NotSupportedException("Data is not supported by the report generator.");
            }
            
        }


    }
}
