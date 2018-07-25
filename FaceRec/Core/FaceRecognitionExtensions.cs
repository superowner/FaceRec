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

        //public static ValueTuple<Bitmap, int, int, double> Recognize(this FaceRecognition recognitor, Mat image)
        //{
        //    var config = ProgramContext.Current.Config;
        //    var start = DateTime.Now;
        //    double fx = 0;
        //    double fy = 0;
        //    fx = config.ImageSampleSize.Width / (double)image.Width;
        //    fy = config.ImageSampleSize.Height / (double)image.Height;
        //    using (var newImage = Resize(image, fx, fy))
        //    {
        //        var bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(newImage);
        //        using (var img = newImage.ToArray2D<RgbPixel>())
        //        {
        //            var detectRectangles = InnerDetect(recognitor, img, bitmap);
        //            var recoginizeRectangles = InnerRecognize(recognitor, img, detectRectangles);

        //            var duration = (DateTime.Now - start).TotalSeconds;
        //            return (bitmap, detectRectangles.Length, recoginizeRectangles.Length, duration);
        //        }
        //    }
        //}

        public static ValueTuple<Bitmap, int, int, double> Recognize(this FaceRecognition recognitor, Mat image)
        {
            var start = DateTime.Now;
            var (bitmap, img, detectRectangles) = recognitor.Detect(image);
            var recoginizeRectangles = InnerRecognize(recognitor, img, bitmap, detectRectangles);
            img.Dispose();
            var duration = (DateTime.Now - start).TotalSeconds;
            return (bitmap, detectRectangles.Length, recoginizeRectangles.Length, duration);
        }

        private static ValueTuple<Bitmap, Array2D<RgbPixel>, FaceLandmarkDetail[]> GetFaceLandmarkDetails(this FaceRecognition recognitor, Mat image)
        {
            var (bitmap, img, detectRectangles) = recognitor.Detect(image);
            var faceLandmarks = recognitor.FaceLandmarks(img, detectRectangles);
            var details = new List<FaceLandmarkDetail>();
            foreach (var faceLandmark in faceLandmarks)
            {
                details.Add(FaceLandmarkDetail.From(faceLandmark));
            }

            return (bitmap, img, details.ToArray());
        }

        private static ValueTuple<Bitmap, Array2D<RgbPixel>, DlibDotNet.Rectangle[]> Detect(this FaceRecognition recognitor, Mat image)
        {
            var config = ProgramContext.Current.Config;
            var start = DateTime.Now;
            double fx = 0;
            double fy = 0;
            fx = config.ImageSampleSize.Width / (double)image.Width;
            fy = config.ImageSampleSize.Height / (double)image.Height;
            using (var newImage = image.Resize(fx, fy))
            {
                var bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(newImage);
                var img = newImage.ToArray2D<RgbPixel>();
                var rectangles = InnerDetect(recognitor, img, bitmap);

                return (bitmap, img, rectangles);
            }
        }

        public static Mat Resize(this Mat image, double fx = 0, double fy = 0)
        {
            if (fx != 0 && fy != 0)
            {
                var newImage = new Mat();
                Cv2.Resize(image, newImage, new OpenCvSharp.Size(0, 0), fx, fy);
                return newImage;
            }
            else
            {
                return image;
            }
        }

        private static DlibDotNet.Rectangle[] InnerDetect(FaceRecognition recognitor, Array2D<RgbPixel> img, Bitmap bitmap, double fx = 0, double fy = 0)
        {
            var config = ProgramContext.Current.Config;
            var rectangles = recognitor.FaceLocations(img, config.UpSampleTimes, config.EnableGPUAcceleration ? "cnn" : "hog");
            if (rectangles.Length > 0 && config.DrawRectangle)
            {
                var rects = rectangles.ToSystemRectangles(fx, fy);
                using (var g = Graphics.FromImage(bitmap))
                {
                    using (Pen pen = new Pen(Color.Red))
                    {
                        g.DrawRectangles(pen, rects);
                    }
                }
            }

            return rectangles;
        }

        private static DlibDotNet.Rectangle[] InnerRecognize(FaceRecognition recognitor, Array2D<RgbPixel> img, Bitmap bitmap, DlibDotNet.Rectangle[] rectangles)
        {
            var config = ProgramContext.Current.Config;
            var knownUsers = ProgramContext.Current.KnownUsers;
            var rects = new List<DlibDotNet.Rectangle>();
            if (rectangles.Length > 0)
            {
                if (config.EnableRealTimeRecoginition)
                {
                    var faceEncodings = recognitor.FaceEncodings(img, rectangles);
                    for (int i = 0; i < faceEncodings.Length; i++)
                    {
                        for (int j = 0; j < knownUsers.Length; j++)
                        {
                            var user = knownUsers[j];
                            var isKnown = recognitor.FaceCompare(faceEncodings[i], user.FaceEncoding);
                            if (isKnown)
                            {
                                rects.Add(rectangles[i]);
                                using (var g = Graphics.FromImage(bitmap))
                                {
                                    using (Pen pen = new Pen(Color.Red))
                                    {
                                        var labelRectangle = new System.Drawing.Rectangle(rectangles[i].Left, rectangles[i].Bottom, (int)rectangles[i].Width, 25);
                                        g.DrawRectangle(pen, labelRectangle);

                                        using (var brush = new SolidBrush(Color.Red))
                                        {
                                            g.DrawString(string.Format("{0}/{1}", user.Name, user.GroupName), new Font("黑体", 14), brush, new PointF(labelRectangle.Left + 5, labelRectangle.Top + 5));
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

            return rects.ToArray();
        }
    }
}
