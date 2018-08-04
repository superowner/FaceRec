using DlibDotNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRec.Models
{
    public struct AppConfig
    {
        public const string ConfigFile = "config.json";

        public bool DrawRectangle { get; set; }

        public uint UpSampleTimes { get; set; }

        public float Tolerance { get; set; }

        public bool EnableGPUAcceleration { get; set; }

        public bool EnableMovementDetection { get; set; }

        public bool EnableCloudDetection { get; set; }

        public bool EnableRealTimeRecoginition { get; set; }

        public static AppConfig Load()
        {
            try
            {
                using (var fs = new FileStream(ConfigFile, System.IO.FileMode.Open))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        var content = sr.ReadToEnd();

                        return JsonConvert.DeserializeObject<AppConfig>(content);

                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void Save()
        {
            try
            {
                using (var fs = new FileStream(ConfigFile, System.IO.FileMode.Create))
                {
                    var content = JsonConvert.SerializeObject(this);
                    using (var sw = new StreamWriter(fs))
                    {
                        sw.Write(content);
                        sw.Flush();
                        sw.Close();
                    }
                    fs.Close();
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void Copy(AppConfig config)
        {
            this.DrawRectangle = config.DrawRectangle;
            this.EnableCloudDetection = config.EnableCloudDetection;
            this.EnableGPUAcceleration = config.EnableGPUAcceleration;
            this.EnableMovementDetection = config.EnableMovementDetection;
            this.EnableRealTimeRecoginition = config.EnableRealTimeRecoginition;
            this.Tolerance = config.Tolerance;
            this.UpSampleTimes = config.UpSampleTimes;
        }
    }
}
