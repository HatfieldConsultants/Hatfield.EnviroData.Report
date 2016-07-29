using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public interface IReportTable
    {
        object[][] RawData { get; set; }
        string ToHtml();
        Stream ToStream(IReportWriter reportWriter);
 
        Task<Stream> ToStreamAsyn(IReportWriter reportWriter);
    }
}
