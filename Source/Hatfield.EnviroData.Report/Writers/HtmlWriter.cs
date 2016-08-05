using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report.Writers
{
    public class HtmlWriter : IWriter<string>
    {
        public string Write(IReportTable data)
        {
            var sb = new StringBuilder();
            sb.Append("<table>");

            foreach (var row in data.RawData)
            {
                sb.Append("<tr>");

                var cellsHtml = String.Join("", row.Select(cell => String.Format("<td>{0}</td>", cell)));
                sb.Append(cellsHtml);

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
    }
}
