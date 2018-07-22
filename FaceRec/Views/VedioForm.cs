using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRec.Views
{
    public class VideoForm : CameraForm
    {
        public string FileName { get; set; }

        public VideoForm() 
        {
            this.Text = "视频识别";
        }

        protected override VideoCapture GetVideoCapture()
        {
            return VideoCapture.FromFile(this.FileName);
        }
    }
}
