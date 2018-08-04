using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;

namespace FaceRec.Core
{
    public class Camera
    {
        public static string[] GetDevices()
        {
            var devices = new List<string>();
            var name = String.Empty.PadRight(100);
            var version = String.Empty.PadRight(100);
            var endOfDeviceList = false;
            short index = 0;

            do
            {
                // Get Driver name and version
                endOfDeviceList = capGetDriverDescriptionA(index, ref name, 100, ref version, 100);
                if (endOfDeviceList)
                {
                    devices.Add(name.Trim('\0'));
                }
                index += 1;
            }
            while (!(endOfDeviceList == false));

            return devices.ToArray();
        }

        [DllImport("avicap32.dll")]
        protected static extern bool capGetDriverDescriptionA(
            short wDriverIndex,
            [MarshalAs(UnmanagedType.VBByRefStr)]ref String lpszName,
            int cbName,
            [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer,
            int cbVer);
    }
}
