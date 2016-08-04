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
            var depth = 0;

            if(headers != null && headers.Any())
            {
                depth = GetMaxDepthOfHeaders(headers.FirstOrDefault().SubHeaders) + 1;

            }

            return depth;
        }

        public static Dictionary<int, List<IReportHeader>> GetValueOfLevels(IReportHeader header)
        {
            var values = new Dictionary<int, List<IReportHeader>>();
            var depth = ReportTableHelper.GetMaxDepthOfHeaders(new List<IReportHeader> { header });

            for (var i = 0; i < depth; i++)
            {
                var valueOfLevel = new List<IReportHeader>();
                GetValueOfParticularLevel(header, 0, i, valueOfLevel);
                values.Add(i, valueOfLevel);
            }

            return values;
            
        }

        private static void GetValueOfParticularLevel(IReportHeader header, int currentLevel, int requestLevel, List<IReportHeader> result)
        { 
            var levelIndex = currentLevel;
            if(levelIndex == requestLevel)
            {
                result.Add(header);
            }

            levelIndex++;

            foreach(var subHeader in header.SubHeaders)
            {
                GetValueOfParticularLevel(subHeader, levelIndex, requestLevel, result);
            }
        }

        public static IEnumerable<IReportHeader> GetPathByLeafIndex(IEnumerable<IReportHeader> headers, int targetIndex)
        {            
            var counter = 0;
            IReportHeader headerTreeThatTheIndexIn = null;

            //locate the header tree
            //to avoid start from very first node all the time
            foreach(var header in headers)
            {
                var widthOfTree = ReportTableHelper.GetMaxWidthOfHeaders(new List<IReportHeader> { header });

                if (targetIndex < (counter + widthOfTree))
                {
                    headerTreeThatTheIndexIn = header;
                    break;

                }
                else
                {
                    counter = counter + widthOfTree;
                }
            }

            var result = new Stack<IReportHeader>();

            GetPathByLeafIndex(result, headerTreeThatTheIndexIn, targetIndex, counter);
            //reverse the stack
            return result.Reverse();
        }

        private static void GetPathByLeafIndex(Stack<IReportHeader> pathStack, IReportHeader header, int targetIndex, int startIndex)
        {
            throw new NotImplementedException();
            pathStack.Push(header);
            if (header.SubHeaders == null || !header.SubHeaders.Any())
            {
                if (startIndex == targetIndex)
                {
                    return;
                }
                else
                {
                    startIndex++;
                    pathStack.Pop();
                }

            }

            
            foreach (var subHeader in header.SubHeaders)
            {               

                GetPathByLeafIndex(pathStack, subHeader, targetIndex, startIndex);
            }
            
            
        }

    }
}
