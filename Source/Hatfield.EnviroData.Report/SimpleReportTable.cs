using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatfield.EnviroData.Report
{
    public class SimpleReportTable : IReportTable
    {
        private object[][] rawData = null;

        public object[][] RawData
        {
            get { return rawData; }
        }

        public string ToHtml()
        {
            throw new NotImplementedException();
        }

        public System.IO.Stream ToStream(IReportWriter reportWriter)
        {
            throw new NotImplementedException();
        }

        public Task<string> ToHtmlAsyn()
        {
            throw new NotImplementedException();
        }

        public Task<System.IO.Stream> ToStreamAsyn(IReportWriter reportWriter)
        {
            throw new NotImplementedException();
        }
    }
}
