using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public class SimpleReportTable : IReportTable
    {
        
        public object[][] RawData
        {
            get;
            set;
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
    }
}
