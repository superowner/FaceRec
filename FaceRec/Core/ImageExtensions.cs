using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRec.Core
{
    public static class ImageExtensions
    {
        public static byte[] ToBytes(this Image image, ImageFormat format = null)
        {
            if (image == null)
            {
                return null;
            }

            using (var ms = new MemoryStream())
            {
                image.Save(ms, format == null ? image.RawFormat : format);
                return ms.ToArray();
            }
        }
    }
}
