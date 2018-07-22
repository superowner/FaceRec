using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRec.Core
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

        public static Rectangle[] ToSystemRectangles(this DlibDotNet.Rectangle[] rectangles, double fx = 0, double fy = 0)
        {
            if (rectangles != null)
            {
                var results = new Rectangle[rectangles.Length];
                for (int i = 0; i < rectangles.Length; i++)
                {
                    results[i] = ToSystemRectangle(rectangles[i], fx, fy);
                }

                return results;
            }

            return new Rectangle[] { };
        }
    }
}
