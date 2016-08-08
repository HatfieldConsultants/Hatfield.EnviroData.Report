using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public interface IReportGenerator
    {
        IReportTable Render(IEnumerable<object> data, Definition tableDefinition);
    }
}
