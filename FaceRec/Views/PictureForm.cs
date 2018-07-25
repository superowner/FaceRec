using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FaceRec.Models;
using DlibDotNet.Tools;
using OpenCvSharp;
using FaceRec.Core;
using DlibDotNet;

namespace FaceRec.Views
{
    public partial class PictureForm : Form
    {
        public string FileName { get; set; }

        public PictureForm()
        {
            InitializeComponent();
        }

        private void PictureForm_Load(object sender, EventArgs e)
        {
            var recognitor = ProgramContext.Current.Recognitor;
            var mainForm = (MainForm)this.MdiParent;
            using (var image = Cv2.ImRead(this.FileName))
            {
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
                this.pictureBox.Image = bitmap;
            }
        }
    }
}
