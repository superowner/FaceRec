using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRec.Extensions
{
    public static class RectangleExtensions
    {
        public static Rectangle ToSystemRectangle(this DlibDotNet.Rectangle rectangle, double fx = 0, double fy = 0)
        {
            return new Rectangle(
                fx == 0 ? rectangle.Left : (int)(rectangle.Left / fx),
                fy == 0 ? rectangle.Top : (int)(rectangle.Top / fy),
                (int)(fx == 0 ? rectangle.Width : rectangle.Width / fx),
                (int)(fy == 0 ? rectangle.Height : rectangle.Height / fy)
                );
        }
    }
}
