﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Hatfield.EnviroData.Report
{
    /// <summary>
    /// Generator to genreate simple report
    /// </summary>
    public class SimpleReportGenerator : ReportGeneratorBase
    {
        public SimpleReportGenerator(IReportableEntityValidator validator)
            : base(validator)
        { 
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tableDefinition"></param>
        /// <returns></returns>
        public override IReportTable Generate(IEnumerable<object> data, Definition tableDefinition)
        {
            ValidateData(data);

            var columnHeader = CalculateHeaders(data, tableDefinition.Cols);
            var rowHeader = CalculateHeaders(data, tableDefinition.Rows);

            //use this flatten data to get the values for each cell
            var flattenData = FlattenData(data);
            var cells = CalculateCells(flattenData, tableDefinition, rowHeader, columnHeader);

            return new SimpleReportTable(rowHeader, columnHeader, cells);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="names"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportHeader"></param>
        /// <param name="propertyNames"></param>
        /// <param name="data"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataOfProperty"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private Dictionary<object, IEnumerable<object>> GroupDataByPropertyName(IEnumerable<object> dataOfProperty, string propertyName)
        {            
            var groups = dataOfProperty.GroupBy(x => GetValueByProperty(x, propertyName));
            var results = groups.ToDictionary(gdc => gdc.Key, gdc => gdc.AsEnumerable());
            return results;
        }

        /// <summary>
        /// Get value for the property by reflection
        /// </summary>
        /// <param name="data">data of object</param>
        /// <param name="propertyName">property name</param>
        /// <returns>value of property</returns>
        private object GetValueByProperty(object data, string propertyName)
        {
            return data.GetType().GetProperty(propertyName).GetValue(data);
        }

        
        /// <summary>
        /// Calculate value for the data cell
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tableDefinition"></param>
        /// <param name="rowHeaders"></param>
        /// <param name="columnHeaders"></param>
        /// <returns></returns>
        private ICell[][] CalculateCells(IEnumerable<PropertyData> data, Definition tableDefinition, 
                                        IEnumerable<IReportHeader> rowHeaders, IEnumerable<IReportHeader> columnHeaders)
        {
            var maxWidth = ReportTableHelper.GetMaxWidthOfHeaders(columnHeaders);
            var maxHeight = ReportTableHelper.GetMaxWidthOfHeaders(rowHeaders);

            var aggregator = AggregatorFactory.Build(tableDefinition.AggregatorName);
            //initial the cells matrix
            var cells = new Cell[maxHeight][];
            for (var i = 0; i < maxHeight; i++)
            {
                cells[i] = new Cell[maxWidth];

                for (var j = 0; j < maxWidth; j++)
                {
                    var matchingRules = DecideMatchRuleForCell(i, j, rowHeaders, columnHeaders);
                    var cellValue = CalculateValueForCell(matchingRules, data, tableDefinition.Vals, aggregator);
                    cells[i][j] = new Cell(cellValue.GetType(), cellValue);
                }
            }

            return cells;
        }        

        
        /// <summary>
        /// decide the match rule for the data cell from the index
        /// result would be like Age: 3, Gender: Male in the tuple
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="rowHeaders"></param>
        /// <param name="columnHeaders"></param>
        /// <returns></returns>
        private IEnumerable<Tuple<string, object>> DecideMatchRuleForCell(int rowIndex, int columnIndex, IEnumerable<IReportHeader> rowHeaders, IEnumerable<IReportHeader> columnHeaders)
        {
            throw new NotImplementedException();
        
        }
        
        /// <summary>
        /// calculate value for cell 
        /// </summary>
        /// <param name="matchingRules"></param>
        /// <param name="flattenData"></param>
        /// <param name="valuePropertyNames"></param>
        /// <param name="aggregator"></param>
        /// <returns></returns>
        private object CalculateValueForCell(IEnumerable<Tuple<string, object>> matchingRules, 
                                            IEnumerable<PropertyData> flattenData, 
                                            IEnumerable<string> valuePropertyNames, 
                                            IValueAggregator aggregator)
        {
            return aggregator.Calculate(valuePropertyNames, matchingRules, flattenData);
        }

    }
}
