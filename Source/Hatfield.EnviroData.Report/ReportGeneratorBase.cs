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
        protected IReportableEntityValidator _validator;
        
        public ReportGeneratorBase(IReportableEntityValidator validator)
        {
            _validator = validator;
        }

        public abstract IReportTable Generate(IEnumerable<object> data, Definition tableDefinition);
        

        protected void ValidateData(IEnumerable<object> data)
        { 
            if(data == null || !data.Any())
            {
                throw new NullReferenceException("No data for report generator to process.");
            }

            var firstElementType = data.First().GetType();
            if (!data.All(x => x.GetType() == firstElementType))
            {
                throw new ArgumentException("Data fro report generator is not in the same type.");
            }

            if (!_validator.IsSupported(data))
            {
                throw new NotSupportedException("Data is not supported by the report generator.");
            }
            
        }

        protected Dictionary<string, PropertyData> FlattenData(IEnumerable<object> data)
        {
            //here assume the data is not null, all the data is validated already
            var objectType = data.First().GetType();
            var propertyInfos = objectType.GetProperties().Where(x => x.PropertyType.IsPublic);

            var allPropertiesData = from propertyInfo in propertyInfos
                                let dataOfProperty = data.Select(x => propertyInfo.GetValue(x))                                
                                select new PropertyData(propertyInfo.Name, propertyInfo.PropertyType, dataOfProperty);            


            return allPropertiesData.ToDictionary(mc => mc.Name, mc => mc);
        }


    }
}
