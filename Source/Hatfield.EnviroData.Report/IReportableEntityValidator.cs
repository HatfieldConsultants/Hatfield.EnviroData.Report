using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public interface IReportableEntityValidator
    {
        bool IsSupported(object data);
        bool IsSupported(IEnumerable<object> data);
    }
}
