using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public class ReportHeader : IReportHeader
    {
        private IList<IReportHeader> _subHeaders;

        public ReportHeader(string name, ICell cellValue)
        {
            PropertyName = name;
            CellValue = cellValue;
            _subHeaders = new List<IReportHeader>();
        }

        public ReportHeader(string name, ICell cellValue, IEnumerable<IReportHeader> subHeaders)
        {
            PropertyName = name;
            CellValue = cellValue;
            _subHeaders = subHeaders.ToList();
        }

        public string PropertyName
        {
            get;
            set;
        }

        public ICell CellValue
        {
            get;
            set;
        }

        public IEnumerable<IReportHeader> SubHeaders
        {
            get
            {
                return _subHeaders;
            }
            set
            {
                _subHeaders = value.ToList();
            }
        }

        public IReportHeader ParentHeader { get; set; }

        public void AddSubHeader(IReportHeader header)
        {
            _subHeaders.Add(header);
            header.ParentHeader = this;
        }


    }
}
