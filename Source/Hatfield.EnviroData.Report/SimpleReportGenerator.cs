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

            var columnHeader = CalculateHeaders(data, tableDefinition.Cols);
            var rowHeader = CalculateHeaders(data, tableDefinition.Rows);
            var cells = CalculateCells(data, tableDefinition, rowHeader, columnHeader);

            return new SimpleReportTable(rowHeader, columnHeader, cells);
        }
        
        private IEnumerable<IReportHeader> CalculateHeaders(IEnumerable<object> data, IEnumerable<string> names)
        {
            var headers = new List<IReportHeader>();
            var dataGroups = GroupDataByPropertyName(data, names.First());

            foreach (var dataGroup in dataGroups)
            {
                var header = new ReportHeader(names.First(), new Cell(dataGroup.Key.GetType(), dataGroup.Key));

                AddSubHeaders(header, names.Skip(1), dataGroup.Value);

                headers.Add(header);
            }

            return headers;

        }

        private void AddSubHeaders(IReportHeader reportHeader, IEnumerable<string> propertyNames, IEnumerable<object> data)
        {
            var statck = new Stack<string>(propertyNames.ToArray());
            
            while(statck.Count > 0)
            {
                var currentPropertyName = statck.Pop();
                var dataGroups = GroupDataByPropertyName(data, currentPropertyName);

                foreach (var dataGroup in dataGroups)
                {
                    var header = new ReportHeader(currentPropertyName, new Cell(dataGroup.Key.GetType(), dataGroup.Key));
                    reportHeader.AddSubHeader(header);

                    AddSubHeaders(header, statck, dataGroup.Value);                    
                } 
            }
            
        }

        private Dictionary<object, IEnumerable<object>> GroupDataByPropertyName(IEnumerable<object> dataOfProperty, string propertyName)
        {            
            var groups = dataOfProperty.GroupBy(x => GetValueByProperty(x, propertyName));
            var results = groups.ToDictionary(gdc => gdc.Key, gdc => gdc.AsEnumerable());
            return results;
        }

        private object GetValueByProperty(object data, string propertyName)
        {
            return data.GetType().GetProperty(propertyName).GetValue(data);
        }

        

        private ICell[][] CalculateCells(IEnumerable<object> data, Definition tableDefinition, 
                                        IEnumerable<IReportHeader> rowHeaders, IEnumerable<IReportHeader> columnHeaders)
        {
            var maxWidth = GetMaxDepthOfHeaders(columnHeaders);
            var maxHeight = GetMaxDepthOfHeaders(rowHeaders);

            //initial the cells matrix
            var cells = new Cell[maxHeight][];
            for (var i = 0; i < maxHeight; i++)
            {
                cells[i] = new Cell[maxWidth];

                for (var j = 0; j < maxWidth; j++)
                {
                    cells[i][j] = new Cell(typeof(string), "N/A");
                }
            }

            return cells;
        }

        private int GetMaxDepthOfHeaders(IEnumerable<IReportHeader> headers)
        {
            if (headers == null || !headers.Any())
            {
                return 0;
            }

            var height = headers.Count();

            foreach(var header in headers)
            {
                height += GetMaxDepthOfHeaders(header.SubHeaders);
            }
            

            return height;
        }
        

    }
}
