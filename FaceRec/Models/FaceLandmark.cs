using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DlibDotNet;

namespace FaceRec.Models
{
    public class FaceLandmark
    {
        /*
         * "chin": points[0:17],
            "left_eyebrow": points[17:22],
            "right_eyebrow": points[22:27],
            "nose_bridge": points[27:31],
            "nose_tip": points[31:36],
            "left_eye": points[36:42],
            "right_eye": points[42:48],
            "top_lip": points[48:55] + [points[64]] + [points[63]] + [points[62]] + [points[61]] + [points[60]],
            "bottom_lip": points[54:60] + [points[48]] + [points[60]] + [points[67]] + [points[66]] + [points[65]] + [points[64]]

         * */

        public FaceLandmark(Point[] points)
        {
            if (points.Length != 68)
            {
                return;
            }

            this.Chip = points.Take(16).ToArray();
            this.LeftEyebrow = points.Skip(16).Take(5).ToArray();
            this.RightEyebrow = points.Skip(21).Take(5).ToArray();
            this.NoseBridge = points.Skip(26).Take(4).ToArray();
            this.NoseTip = points.Skip(30).Take(5).ToArray();
            this.LeftEye = points.Skip(35).Take(6).ToArray();
            this.RightEye = points.Skip(41).Take(6).ToArray();
            this.TopLip = points.Skip(47).Take(7)
                .Concat(new Point[] { points[64], points[63], points[62], points[61], points[60] }).ToArray();
            this.BottomLip = points.Skip(53).Take(6)
                .Concat(new Point[] { points[48], points[60], points[67], points[66], points[65], points[64] }).ToArray();
        }

        public Point[] Chip { get; private set; }

        public Point[] LeftEyebrow { get; private set; }

        public Point[] RightEyebrow { get; private set; }

        public Point[] NoseBridge { get; private set; }

        public Point[] NoseTip { get; private set; }

        public Point[] LeftEye { get; private set; }

        public Point[] RightEye { get; private set; }

        public Point[] TopLip { get; private set; }

        public Point[] BottomLip { get; private set; }
    }
}
