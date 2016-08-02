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

            //var flattenData = FlattenData(data);

            var columnHeader = CalculateColumnHeader(data, tableDefinition);
            var rowHeader = CalculateRowHeader(data, tableDefinition);
            var cells = CalculateCells(data, tableDefinition);

            return new SimpleReportTable(rowHeader, columnHeader, cells);
        }

        private IEnumerable<IReportHeader> CalculateColumnHeader(IEnumerable<object> data, Definition tableDefinition)
        {
            var headers = new List<IReportHeader>();
            foreach(var columnName in tableDefinition.Cols)
            {
                var dataGroups = GroupDataByPropertyName(data, columnName);

                foreach(var dataGroup in dataGroups)
                {
                    var header = new ReportHeader(columnName, new Cell(dataGroup.Key.GetType(), dataGroup.Key));
                    headers.Add(header);

                    
                }

                
            }

            return headers;
        }

        private IEnumerable<IReportHeader> CalculateRowHeader(IEnumerable<object> data, Definition tableDefinition)
        {
            throw new NotImplementedException();
        }

        private ICell[][] CalculateCells(IEnumerable<object> data, Definition tableDefinition)
        {
            throw new NotImplementedException();
        }

        private Dictionary<object, IEnumerable<object>> GroupDataByPropertyName(IEnumerable<object> dataOfProperty, string propertyName)
        {
            //IEnumerable<dynamic> dynamicObjects = data;
            var groups = dataOfProperty.GroupBy(x => GetValueByProperty(x, propertyName));
            var results = groups.ToDictionary(gdc => gdc.Key, gdc => gdc.AsEnumerable());
            return results;
        }

        private void AddSubHeaders(IReportHeader reportHeader, string propertyName, IEnumerable<object> data)
        {
            throw new NotImplementedException();
        }

        private object GetValueByProperty(object data, string propertyName)
        {
            return data.GetType().GetProperty(propertyName).GetValue(data);
        }

    }
}
