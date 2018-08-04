using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace FaceRec.Core
{
    public static class PathExtensions
    {
        private static readonly Regex containsChineseCharsRegex = new Regex("[\u4e00-\u9fa5]");

        public static string EnsureNonChineseCharsFileName(this string fileName)
        {
            if (containsChineseCharsRegex.IsMatch(fileName))
            {
                var tempFileName = Path.GetTempFileName();
                File.Copy(fileName, tempFileName, true);
                return tempFileName;
            }

            return fileName;
        }
    }
}
