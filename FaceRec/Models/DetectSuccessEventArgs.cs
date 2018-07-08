using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRec.Models
{
    public class DetectSuccessEventArgs : EventArgs
    {
        public DetectSuccessEventArgs(Bitmap image, Rectangle[] rectangles, double duration)
        {
            this.Image = image;
            this.Rectangles = rectangles;
            this.Duration = duration;
        }

        public Bitmap Image { get; set; }

        public Rectangle[] Rectangles { get; set; }

        public double Duration { get; set; }
    }
}
