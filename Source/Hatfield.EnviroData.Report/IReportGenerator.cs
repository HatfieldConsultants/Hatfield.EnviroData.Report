using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public interface IReportGenerator
    {
        IReportTable Generate(IEnumerable<ReportableEntityBase> data, Definition tableDefinition);
    }
}
