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
            var rowStartIndex = 0;
            var currentColumnHeader = this.ColumnHeaders.First();
            while (currentColumnHeader.SubHeaders != null && currentColumnHeader.SubHeaders.Any())
            {
                rowStartIndex++;
                currentColumnHeader = currentColumnHeader.SubHeaders.First();
            }

            throw new NotImplementedException();
        }


        
    }
}
