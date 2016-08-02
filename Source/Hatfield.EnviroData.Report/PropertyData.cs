using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public class PropertyData
    {
        public PropertyData(string name, Type type)
        {
            Name = name;
            Type = type;
            _data = new List<object>();
        }

        public PropertyData(string name, Type type, IEnumerable<object> data)
        {
            Name = name;
            Type = type;
            _data = data.ToList();
        }

        private List<object> _data;

        public string Name { get; set; }
        public Type Type { get; set; }

        public IEnumerable<object> Data 
        {
            get
            {
                return _data;
            }
        }

        public void AddData(object item)
        {
            _data.Add(item);
        }


    }
}
