using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DlibDotNet;
using DlibDotNet.Tools;
using FaceRec.Models;
using OpenCvSharp;

namespace FaceRec.Core
{
    public static class FaceRecognitionExtensions
    {
        public static Array2D<T> ToArray2D<T>(this Mat image)
            where T : struct
        {
            var bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
            return DlibDotNet.Extensions.BitmapExtensions.ToArray2D<T>(bitmap);
        }

        public static ValueTuple<Bitmap, int, int, double> Recognize(this FaceRecognition recognitor, Array2D<RgbPixel> img)
        {
            var start = DateTime.Now;
            var config = ProgramContext.Current.Config;
            var bitmap = DlibDotNet.Extensions.BitmapExtensions.ToBitmap(img);
            var recoginizeRectangles = new List<DlibDotNet.Rectangle>();
            var detectRectangles = recognitor.FaceLocations(img, config.UpSampleTimes, config.EnableGPUAcceleration ? "cnn" : "hog");
            if (detectRectangles.Length > 0 && config.DrawRectangle)
            {
                var rects = detectRectangles.ToSystemRectangles();
                using (var g = Graphics.FromImage(bitmap))
                {
                    using (Pen pen = new Pen(Color.Red))
                    {
                        g.DrawRectangles(pen, rects);
                    }
                }

                if (config.EnableRealTimeRecoginition)
                {
                    var knownUsers = ProgramContext.Current.KnownUsers;
                    var faceEncodings = recognitor.FaceEncodings(img, detectRectangles);
                    for (int i = 0; i < faceEncodings.Length; i++)
                    {
                        for (int j = 0; j < knownUsers.Length; j++)
                        {
                            var userView = knownUsers[j];
                            var isKnown = recognitor.FaceCompare(faceEncodings[i], userView.FaceEncoding, 0.5f);
                            if (isKnown)
                            {
                                recoginizeRectangles.Add(detectRectangles[i]);
                                using (var g = Graphics.FromImage(bitmap))
                                {
                                    using (Pen pen = new Pen(Color.Red))
                                    {
                                        var labelRectangle = new System.Drawing.Rectangle(detectRectangles[i].Left, detectRectangles[i].Bottom, (int)detectRectangles[i].Width, 25);
                                        g.DrawRectangle(pen, labelRectangle);

                                        using (var brush = new SolidBrush(Color.Red))
                                        {
                                            g.DrawString(string.Format("{0}/{1}", userView.Name, userView.GroupName), new Font("黑体", 14), brush, new PointF(labelRectangle.Left + 5, labelRectangle.Top + 5));
                                        }
                                    }
                                }

                                break;
                            }
                        }
                        faceEncodings[i].Dispose();
                    }
                }
                else
                {

                }
            }

            var duration = (DateTime.Now - start).TotalSeconds;
            return (bitmap, detectRectangles.Length, recoginizeRectangles.Count, duration);
        }
    }
}
