using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Hatfield.EnviroData.Report.Excel
{
    public class ExcelWriter : IWriter<byte[]>
    {
        public byte[] Write(IReportTable data)
        {
            var workBook = new XSSFWorkbook();
            ISheet sheet = workBook.CreateSheet("Sheet1");

            var rawData = data.RawData;
            if(rawData != null && rawData.Any())
            {
                for (var i = 0; i < rawData.Length; i++)
                {
                    var row = sheet.CreateRow(i);

                    for (var j = 0; j < rawData[i].Length; j++)
                    {
                        if (rawData[i][j] != null)
                        {
                            row.CreateCell(j).SetCellValue(rawData[i][j].ToString());
                        }
                        
                    }
                }
            }
            
            var memoryStream = new MemoryStream();
            workBook.Write(memoryStream);




            return memoryStream.ToArray();
        }
    }
}
