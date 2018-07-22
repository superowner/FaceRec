using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DlibDotNet;

namespace FaceRec.Models
{
    public class FaceLandmarkDetail
    {
        public FaceLandmarkDetail(Point[] points)
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

        public static FaceLandmarkDetail From(FullObjectDetection faceLandmark)
        {
            var points = new DlibDotNet.Point[faceLandmark.Parts];
            for (uint index = 0; index < faceLandmark.Parts; index++)
            {
                points[index] = faceLandmark.GetPart(index);
            }

            return new FaceLandmarkDetail(points);
        }
    }
}
