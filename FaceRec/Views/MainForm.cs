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
using DlibDotNet.Tools;

namespace FaceRec.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var systemOptionForm = new OptionsForm();
            systemOptionForm.ShowDialog();
        }

        private string GetFileName(string recognitionMode = "图片")
        {
            var result = this.openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return this.openFileDialog.FileName;
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
                var pictureForm = new PictureForm();
                pictureForm.MdiParent = this;
                pictureForm.FileName = fileName;
                pictureForm.Show();
                pictureForm.WindowState = FormWindowState.Maximized;
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
                var videoForm = new VideoForm();
                videoForm.MdiParent = this;
                videoForm.FileName = fileName;
                videoForm.Show();
                videoForm.WindowState = FormWindowState.Maximized;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                this.UpdateStatusAsync("正在初始化...");
                try
                {
                    ProgramContext.Initialize();
                }
                catch (Exception error)
                {
                    this.ShowMessagesAsync(error.Message, "初始化异常");
                }
                this.UpdateStatusAsync("初始化完成");
                this.UpdateRecognitionMenuItemsAsync(true);
            }).Start();
        }

        private void ShowMessagesAsync(string text, string caption)
        {
            MethodInvoker updateImage = () =>
            {
                if (!this.IsDisposed)
                {
                    MessageBox.Show(text, "初始化异常");
                }
            };
            if (!this.IsDisposed)
            {
                this.BeginInvoke(updateImage);
            }
        }

        public void UpdateStatusAsync(string text)
        {
            MethodInvoker updateImage = () =>
            {
                if (!this.IsDisposed)
                {
                    this.toolStripStatusLabel.Text = text;
                }
            };
            if (!this.IsDisposed)
            {
                this.BeginInvoke(updateImage);
            }
        }

        private void UpdateRecognitionMenuItemsAsync(bool enable)
        {
            MethodInvoker updateImage = () =>
            {
                if (!this.IsDisposed)
                {
                    this.cameraRecognitionToolStripMenuItem.Enabled = enable;
                    this.videoRecognitionToolStripMenuItem.Enabled = enable;
                    this.pictureRecognitionToolStripMenuItem.Enabled = enable;
                    this.userToolStripMenuItem.Enabled = enable;
                }
            };
            if (!this.IsDisposed)
            {
                this.BeginInvoke(updateImage);
            }
        }

        private void cameraRecognitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cameraForm = new CameraForm();
            cameraForm.MdiParent = this;
            cameraForm.Show();
            cameraForm.WindowState = FormWindowState.Maximized;
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var userForm = new UserForm();
            userForm.ShowDialog();
        }
    }
}
