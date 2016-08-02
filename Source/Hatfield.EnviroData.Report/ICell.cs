using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public interface ICell
    {
        Type Type { get; set; }
        object Value { get; set; }
    }
}
