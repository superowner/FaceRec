using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

using OpenCvSharp;
using OpenCvSharp.Extensions;
using DlibDotNet;
using FaceRec.Models;

namespace FaceRec.Views
{
    public partial class MainForm : Form
    {
        //private bool isRunning;

        public MainForm()
        {
            InitializeComponent();

            Program.Current.DetectSuccess += Current_DetectSuccess;
        }

        private void Current_DetectSuccess(object sender, DetectSuccessEventArgs e)
        {           
            this.UpdateStatusAsync($"检测{e.Rectangles.Length}个,耗时{Math.Round(e.Duration, 3)}");
            this.UpdatePicutureBoxAsync(e.Image);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Exit();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpdateUIOnStop();
        }

        private void UpdateStatusAsync(string text)
        {
            MethodInvoker updateStatus = () =>
            {
                if (!this.IsDisposed)
                {
                    this.speedToolStripStatusLabel.Text = text;
                }
            };
            if (!this.IsDisposed)
            {
                this.BeginInvoke(updateStatus);
            }
            Application.DoEvents();
        }

        private void UpdatePicutureBoxAsync(Bitmap bitmap)
        {
            MethodInvoker updateImage = () =>
            {
                if (!this.IsDisposed)
                {
                    this.mainPictureBox.Image = bitmap;
                }
            };
            if (!this.IsDisposed)
            {
                this.BeginInvoke(updateImage);
            }
            Application.DoEvents();
        }

        private void UpdateUIOnStart()
        {
            MethodInvoker updateImage = () =>
            {
                if (!this.IsDisposed)
                {
                    this.startToolStripMenuItem.Enabled = false;
                    this.stopToolStripMenuItem.Enabled = true;
                    Program.Current.isRunning = true;
                    this.speedToolStripStatusLabel.Text = "检测中...";
                }
            };
            if (!this.IsDisposed)
            {
                this.BeginInvoke(updateImage);
            }
            Application.DoEvents();
        }

        private void UpdateUIOnStop()
        {
            MethodInvoker updateImage = () =>
            {
                if (!this.IsDisposed)
                {
                    this.startToolStripMenuItem.Enabled = true;
                    this.stopToolStripMenuItem.Enabled = false;
                    Program.Current.isRunning = false;
                    this.speedToolStripStatusLabel.Text = "";
                    this.mainPictureBox.Image = null;
                }
            };
            if (!this.IsDisposed)
            {
                this.BeginInvoke(updateImage);
            }
            Application.DoEvents();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action start = () =>
            {
                this.UpdateUIOnStart();
                using (var videoCapture = VideoCapture.FromCamera(0))
                {
                    Program.GetFaceLocationsFromVideo(videoCapture);
                }
            };

            if (Program.Current.isRunning)
            {
                var result = MessageBox.Show("使用摄像头识别将停止进行中的其他识别", "提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.UpdateUIOnStop();
                    Thread.Sleep(100);
                    Application.DoEvents();

                    start();
                }
            }
            else
            {
                start();
            }
        }

        private void systemOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var systemOptionForm = new SystemOptionsForm();
            systemOptionForm.ShowDialog();
        }

        private string GetFileName(string recognitionMode = "图片")
        {
            Func<string> getFileName = () =>
            {
                var openFileResult = this.openFileDialog.ShowDialog();
                if (openFileResult == DialogResult.OK)
                {
                    return this.openFileDialog.FileName;
                }

                return string.Empty;
            };

            if (Program.Current.isRunning)
            {
                var result = MessageBox.Show($"使用{recognitionMode}识别将停止进行中的其他识别", "提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.UpdateUIOnStop();
                    Thread.Sleep(100);
                    Application.DoEvents();

                    return getFileName();
                }
            }
            else
            {
                return getFileName();
            }

            return string.Empty;
        }

        private void pictureRecognitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog.DefaultExt = "jpg";
            this.openFileDialog.Filter = "所有图片|*.jpg;*.jpeg;*.png;*.bmp";
            this.openFileDialog.Title = "选择图片";
            var fileName = this.GetFileName();
            if (fileName != string.Empty)
            {
                using (var image = Cv2.ImRead(fileName))
                {
                    this.mainPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                    this.mainPictureBox.Dock = DockStyle.None;
                    //this.UpdateUIOnStart();
                    this.UpdateStatusAsync("检测中...");
                    Program.GetFaceLocationsFromMat(image);
                }
            }
        }

        private void videoRecognitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog.DefaultExt = "mp4";
            this.openFileDialog.Filter = "所有视频|*.avi;*.mp4;*.mpeg;*wmv";
            this.openFileDialog.Title = "选择视频";
            var fileName = this.GetFileName("视频");
            if (fileName != string.Empty)
            {
                using (var videoCapture = VideoCapture.FromFile(fileName))
                {
                    this.mainPictureBox.SizeMode = PictureBoxSizeMode.Normal;
                    this.mainPictureBox.Dock = DockStyle.Fill;
                    this.UpdateUIOnStart();
                    Program.GetFaceLocationsFromVideo(videoCapture);
                }
            }
        }
    }
}
