using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DlibDotNet;
using FaceRec.Models;
using FaceRec.Extensions;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace FaceRec.Core
{
    public static class FaceRecognition
    {
        static FrontalFaceDetector faceDetector = FrontalFaceDetector.GetFrontalFaceDetector();
        static ShapePredictor pose_predictor = new ShapePredictor("data/models/shape_predictor_68_face_landmarks.dat");

        //public static Array2D<RgbPixel> LoadImage(string filePath)
        //{
        //    return Dlib.LoadImage<RgbPixel>(filePath);
        //}

        private static System.Drawing.Rectangle[] GetRawFaceLocations(Mat image, double fx = 0, double fy = 0)
        {
            var writeableBitmap = WriteableBitmapConverter.ToWriteableBitmap(image);
            var imgRgbArray = DlibDotNet.Extensions.WriteableBitmapExtensions.ToArray2D<RgbPixel>(writeableBitmap);
            var rectangles = FaceRecognition.GetFaceLocations(imgRgbArray);

            return (from rectangle in rectangles
                    select rectangle.ToSystemRectangle(fx, fy)
            ).ToArray();
        }

        public static System.Drawing.Rectangle[] GetFaceLocations(Mat image, double fx = 0, double fy = 0)
        {
            if (fx != 0 && fy != 0)
            {
                using (var smallImage = new Mat())
                {
                    Cv2.Resize(image, smallImage, new Size(0, 0), fx, fy);

                    return GetRawFaceLocations(smallImage, fx, fy);
                }
            }
            else
            {
                return GetRawFaceLocations(image, fx, fy);
            }
        }

        //face_locations(img, number_of_times_to_upsample=1, model="hog"):
        public static Rectangle[] GetFaceLocations(Array2DBase img, double threshold = 0d)
        {
            //Dlib.PyramidUp(img);
            return faceDetector.Detect(img, threshold);
        }


        //face_image, face_locations= None, model= "large"
        public static IEnumerable<KeyValuePair<FullObjectDetection, FaceLandmark>> GetFaceLandmarks(Array2DBase faceImage, Rectangle[] faceLocations = null)
        {
            if (faceLocations == null)
            {
                faceLocations = GetFaceLocations(faceImage);
            }

            var faceLandmarks = new Dictionary<FullObjectDetection, FaceLandmark>();
            foreach (var faceLocation in faceLocations)
            {
                using (var dect = pose_predictor.Detect(faceImage, faceLocation))
                {
                    var points = new DlibDotNet.Point[dect.Parts];
                    for (uint index = 0; index < dect.Parts; index++)
                    {
                        points[index] = dect.GetPart(index);
                    }
                    faceLandmarks.Add(dect, new FaceLandmark(points));
                }
            }
            return faceLandmarks.ToArray();
        }


        //public static ChipDetails[] GetFaceChipDetails(IEnumerable<FullObjectDetection> dets, uint size = 200, double padding = 0.2d)
        //{
        //    return Dlib.GetFaceChipDetails(dets, size, padding);
        //}
    }
}

