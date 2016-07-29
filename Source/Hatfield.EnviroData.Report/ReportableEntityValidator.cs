using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;

namespace Hatfield.EnviroData.Report
{
    public class ReportableEntityValidator : IReportableEntityValidator
    {
        static Type stringType = typeof(string);
        static Type dateTimeType = typeof(DateTime);

        private bool IsPropertyInfoValid(PropertyInfo propertyInfo)
        {
            //if it is string or primitive type, it is valid
            //otherwise it is not valid
            return stringType.IsAssignableFrom(propertyInfo.PropertyType) ||
                    dateTimeType.IsAssignableFrom(propertyInfo.PropertyType) ||
                    propertyInfo.PropertyType.IsPrimitive;
        }

        public bool IsSupported(object data)
        {
            var propertyInfos = data.GetType().GetProperties();
            if (propertyInfos.Any(x => !IsPropertyInfoValid(x)))
            {
                return false;
            }

            return true;            
        }

        public bool IsSupported(IEnumerable<object> data)
        {
            return !data.Any(x => !this.IsSupported(x));            
        }
    }
}
