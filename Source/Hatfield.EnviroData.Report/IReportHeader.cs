using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public interface IReportHeader
    {
        string PropertyName { get; set; }
        ICell CellValue { get; set; }

        IEnumerable<IReportHeader> SubHeaders { get; set; }
        void AddSubHeader(IReportHeader header);        
        
    }
}
