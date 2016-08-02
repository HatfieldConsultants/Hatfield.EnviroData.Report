using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public class Cell : ICell
    {
        public Cell(Type type, object value)
        {
            Type = type;
            Value = value;
        }

        public Type Type
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }
    }
}
