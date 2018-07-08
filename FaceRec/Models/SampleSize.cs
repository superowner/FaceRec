using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRec.Models
{
    public struct SampleSize
    {
        public string Name { get { return string.Format("{1}p({0}x{1})", this.Width, this.Height); } }

        public int Value { get { return this.Width * this.Height; } }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public SampleSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public static SampleSize[] AllSizes = new SampleSize[] {
            new SampleSize(640, 480),
            new SampleSize(960 , 640),
            new SampleSize(1280 , 720),
            new SampleSize(1600 , 900),
            new SampleSize(1920 , 1080)
        };

        public override bool Equals(object obj)
        {
            if(obj is SampleSize)
            {
                return this.Value == ((SampleSize)obj).Value;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Width.GetHashCode() ^ this.Height.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static implicit operator SampleSize(int value)
        {
            var size = AllSizes.FirstOrDefault((bs) => bs.Value == value);
            return size.Value != 0 ? size : AllSizes[0];
        }

        public static explicit operator int(SampleSize size)
        {
            return size.Value;
        }
    }
}
