using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRec.Models
{
    public struct AppConfig
    {       
        public SampleSize ImageSampleSize { get; set; }

        public double Tolerance { get; set; }

        public bool EnableGPUAccelerating { get; set; }

        public bool EnableMovementDetecting { get; set; }

        public bool EnableCloudDetecting { get; set; }
    }
}
