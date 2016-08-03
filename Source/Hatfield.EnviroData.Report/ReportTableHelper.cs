using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public static class ReportTableHelper
    {
        public static int GetMaxWidthOfHeaders(IEnumerable<IReportHeader> headers)
        {
            if (headers == null || !headers.Any())
            {
                return 1;
            }

            var height = 0;

            foreach (var header in headers)
            {
                height += GetMaxWidthOfHeaders(header.SubHeaders);
            }


            return height;
        }

        public static int GetMaxDepthOfHeaders(IEnumerable<IReportHeader> headers)
        {
            var depth = 1;

            if(headers != null && headers.Any())
            {
                depth += GetMaxDepthOfHeaders(headers.FirstOrDefault().SubHeaders);

            }

            return depth;
        }

    }
}
