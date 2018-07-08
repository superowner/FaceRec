using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using FaceRec.Core;
using FaceRec.Models;
using FaceRec.Views;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using DlibDotNet;
using System.Drawing;
using System.Data.SqlClient;

namespace FaceRec
{
    public class Program
    {
        public delegate void DetectSuccessEventHandler(object sender, DetectSuccessEventArgs e);

        public static Program Current = new Program();

        public AppConfig Config;
        public bool isRunning;
        public event DetectSuccessEventHandler DetectSuccess;

        private void fireDetectSuccess(DetectSuccessEventArgs e)
        {
            if (this.DetectSuccess != null)
                this.DetectSuccess(this, e);
        }

        public static ValueTuple<Bitmap, System.Drawing.Rectangle[], double> GetFaceLocationsFromMat(Mat image, bool fireEvent = true)
        {
            var start = DateTime.Now;
            double fx = 0;
            double fy = 0;
            fx = Current.Config.ImageSampleSize.Width / (double)image.Width;
            fy = Current.Config.ImageSampleSize.Height / (double)image.Height;

            var rectangles = FaceRecognition.GetFaceLocations(image, fx, fy);
            var bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);

            if (rectangles.Length > 0)
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    using (Pen pen = new Pen(Color.Red))
                    {
                        g.DrawRectangles(pen, rectangles);
                    }
                }
            }

            var duration = (DateTime.Now - start).TotalSeconds;
            if (fireEvent)
            {
                Current.fireDetectSuccess(new DetectSuccessEventArgs(bitmap, rectangles, duration));
            }

            return (bitmap, rectangles, duration);
        }

        public static void GetFaceLocationsFromVideo(VideoCapture videoCapture, bool fireEvent = true)
        {
            if (!videoCapture.IsOpened())
            {
                return;
            }

            using (Mat image = new Mat()) // Frame image buffer
            {
                while (Current.isRunning)
                {
                    Application.DoEvents();
                    videoCapture.Read(image); // same as cvQueryFrame
                    GetFaceLocationsFromMat(image, fireEvent);
                }
            }
        }

        private static bool Initialize()
        {
            try
            {
                using (var fs = new FileStream("config.json", System.IO.FileMode.Open))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        var content = sr.ReadToEnd();

                        Current.Config = JsonConvert.DeserializeObject<AppConfig>(content, new SampleSizeJsonConverter());
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "加载配置文件异常");

                return false;
            }
        }
        public static void Exit()
        {
            Current.isRunning = false;
            Application.Exit();
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!Initialize())
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
