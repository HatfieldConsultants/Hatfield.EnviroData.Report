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
        object[][] RawData { get; }
        string ToHtml();
        Stream ToStream(IReportWriter reportWriter);

        Task<string> ToHtmlAsyn();        
        Task<Stream> ToStreamAsyn(IReportWriter reportWriter);
    }
}
