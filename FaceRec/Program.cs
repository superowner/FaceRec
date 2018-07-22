using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

//using FaceRec.Core;
using FaceRec.Models;
using FaceRec.Views;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using DlibDotNet;
using System.Drawing;
using System.Data.SqlClient;
using DlibDotNet.Tools;

namespace FaceRec
{
    public class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
