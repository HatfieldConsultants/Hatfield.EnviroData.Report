using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public class SimpleReportTable : IReportTable
    {
        private object[][] _rawData;
        private IEnumerable<IReportHeader> _rowHeaders;
        private IEnumerable<IReportHeader> _columnHeaders;
        private ICell[][] _cells;

        public SimpleReportTable(IEnumerable<IReportHeader> rowHeaders, IEnumerable<IReportHeader> columnHeaders, ICell[][] cells)
        {
            _rowHeaders = rowHeaders;
            _columnHeaders = columnHeaders;
            _cells = cells;
        }

        public object[][] RawData
        {
            get
            {
                if(_rawData == null)
                {
                    _rawData = CalculateRawData();
                }
                
                return _rawData;
            }
            
        }

        public IEnumerable<IReportHeader> RowHeaders
        {
            get 
            {
                return _rowHeaders;
            }
        }

        public IEnumerable<IReportHeader> ColumnHeaders
        {
            get 
            {
                return _columnHeaders;
            }
        }

        public ICell[][] Cells
        {
            get 
            {
                return _cells;
            }
        }

        public string ToHtml()
        {
            var sb = new StringBuilder();
            sb.Append("<table>");

            foreach (var row in RawData)
            {
                sb.Append("<tr>");

                var cellsHtml = String.Join("", row.Select(cell => String.Format("<td>{0}</td>", cell)));
                sb.Append(cellsHtml);

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }

        public System.IO.Stream ToStream(IReportWriter reportWriter)
        {
            throw new NotImplementedException();
        }

        public Task<System.IO.Stream> ToStreamAsyn(IReportWriter reportWriter)
        {
            throw new NotImplementedException();
        }

        private object[][] CalculateRawData()
        {
            //column width and height
            var columnWidth = ReportTableHelper.GetMaxWidthOfHeaders(_columnHeaders);
            var columnHeight = ReportTableHelper.GetMaxDepthOfHeaders(_columnHeaders);
            //row width and height
            //matrix rotate 90 degree
            var rowWidth = ReportTableHelper.GetMaxDepthOfHeaders(_rowHeaders);
            var rowHeight = ReportTableHelper.GetMaxWidthOfHeaders(_rowHeaders);

            var totalWidth = columnWidth + rowWidth + 1;
            var totalHeight = columnHeight + rowHeight + 1;

            var rawData = new object[totalHeight][];
            for (var i = 0; i < totalHeight; i++)
            {
                rawData[i] = new object[totalWidth];
            }

            //set headers
            SetColumnHeaders(rawData, rowWidth, _columnHeaders);
            SetRowHeaders(rawData, columnHeight, _rowHeaders);

            return rawData;

            
        }

        /// <summary>
        /// Set the column header
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startColumnIndex"></param>
        /// <param name="columnHeaders"></param>
        private void SetColumnHeaders(object[][] data, int startColumnIndex, IEnumerable<IReportHeader> columnHeaders)
        {
            var rowIndex = 0;
            
            var currentHeader = columnHeaders.FirstOrDefault();

            //set the property name column
            while (currentHeader != null)
            {
                data[rowIndex][startColumnIndex] = currentHeader.PropertyName;
                currentHeader = currentHeader.SubHeaders.FirstOrDefault();
                rowIndex++;
            }
            
            startColumnIndex++;
            //set value from left to right
            foreach(var header in columnHeaders)
            {
                //rowIndex = 0;//reset the row index
                var tempStartColumnIndex = startColumnIndex;
                var headerValuesOfEachLevel = ReportTableHelper.GetValueOfLevels(header);
                var widthOfHeader = headerValuesOfEachLevel.Last().Value.Count;

                foreach(var level in headerValuesOfEachLevel)
                {                    
                    tempStartColumnIndex = startColumnIndex;//reset the start column index
                    foreach(var optionValue in level.Value)
                    {
                        var cellLength = widthOfHeader / level.Value.Count;
                        while(cellLength > 0)
                        {
                            data[level.Key][tempStartColumnIndex++] = optionValue.CellValue.Value;//set value
                            cellLength--;
                        }
                    }
                }

                startColumnIndex = startColumnIndex + widthOfHeader;//move to the next header
            }
            


        }

        /// <summary>
        /// Set the row header
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="rowHeaders"></param>
        private void SetRowHeaders(object[][] data, int startRowIndex, IEnumerable<IReportHeader> rowHeaders)
        {
            var columnIndex = 0;
            
            //set the property name row
            var currentHeader = rowHeaders.FirstOrDefault();
            while (currentHeader != null)
            {
                data[startRowIndex][columnIndex] = currentHeader.PropertyName;
                currentHeader = currentHeader.SubHeaders.FirstOrDefault();
                columnIndex++;
            }

            startRowIndex++;

            //set value from top to bottom
            foreach (var header in rowHeaders)
            {                
                var tempStartRowIndex = startRowIndex;
                var headerValuesOfEachLevel = ReportTableHelper.GetValueOfLevels(header);
                var widthOfHeader = headerValuesOfEachLevel.Last().Value.Count;

                foreach (var level in headerValuesOfEachLevel)
                {
                    tempStartRowIndex = startRowIndex;//reset the start column index
                    foreach (var optionValue in level.Value)
                    {
                        var cellLength = widthOfHeader / level.Value.Count;
                        while (cellLength > 0)
                        {
                            data[tempStartRowIndex++][level.Key] = optionValue.CellValue.Value;//set value
                            cellLength--;
                        }
                    }
                }

                startRowIndex = startRowIndex + widthOfHeader;//move to the next header
            }
        }

        
    }
}
