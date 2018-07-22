using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using FaceRec.Models;
using DlibDotNet.Tools;
using OpenCvSharp;
using FaceRec.Core;
using DlibDotNet;

namespace FaceRec.Views
{
    public partial class CameraForm : Form
    {
        private bool isRunning;
        private Thread runningThread;

        public CameraForm()
        {
            InitializeComponent();
        }

        protected virtual VideoCapture GetVideoCapture()
        {
            return VideoCapture.FromCamera(0);
        }

        private void UpdatePicutureBoxAsync(Bitmap bitmap)
        {
            MethodInvoker updateImage = () =>
            {
                if (!this.IsDisposed)
                {
                    this.pictureBox.Image = bitmap;
                }
            };

            if (!this.IsDisposed)
            {
                this.BeginInvoke(updateImage);
            }
        }

        private void CameraForm_Load(object sender, EventArgs e)
        {
            this.isRunning = true;            
            var recognitor = ProgramContext.Current.Recognitor;
            var mainForm = (MainForm)this.MdiParent;

            this.runningThread = new Thread(() =>
            {                
                mainForm.UpdateStatusAsync("正在检测...");
                using (var videoCapture = this.GetVideoCapture())
                {
                    if (!videoCapture.IsOpened())
                    {                        
                        mainForm.UpdateStatusAsync(videoCapture.CaptureType == CaptureType.Camera ? "无法打开摄像头" : "无法打开视频文件");
                        return;
                    }

                    using (Mat image = new Mat()) // Frame image buffer
                    {
                        while (this.isRunning)
                        {
                            var start = DateTime.Now;
                            Application.DoEvents();
                            videoCapture.Read(image); // same as cvQueryFrame                            
                            var (bitmap, detectionCount, recognitionCount, duration) = recognitor.Recognize(image);
                            string text;
                            if (ProgramContext.Current.Config.EnableRealTimeRecoginition)
                            {
                                text = string.Format("检测到{0}个,识别成功{1}个，耗时{2:0.00}秒", detectionCount, recognitionCount, duration);
                            }
                            else
                            {
                                text = string.Format("检测到{0}个，耗时{1:0.00}秒", detectionCount, duration);
                            }
                            mainForm.UpdateStatusAsync(text);
                            this.UpdatePicutureBoxAsync(bitmap);
                        }
                    }
                }
            });
            this.runningThread.Start();
        }

        private void CameraForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.isRunning = false;
            while (this.runningThread.IsAlive)
            {
                Thread.Sleep(50);
            }
        }
    }
}
