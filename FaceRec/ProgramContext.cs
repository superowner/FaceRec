
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FaceRec.Core;
using DlibDotNet;
using DlibDotNet.Tools;
using FaceRec.Models;
using System.IO.Compression;
using System.IO;

namespace FaceRec
{
    public class ProgramContext
    {
        public static ProgramContext Current = new ProgramContext();

        public AppConfig Config;
        public UserView[] KnownUsers;
        public FaceRecognition Recognitor;
        public const string RecognitionModelFile = "data\\models\\dlib_face_recognition_resnet_model_v1.dat";
        public const string CnnDetectorModelFile = "data\\models\\mmod_human_face_detector.dat";
        public const string LandmarksModelFile = "data\\models\\shape_predictor_68_face_landmarks.dat";

        public static void Initialize()
        {
            try
            {
                Current.Config = AppConfig.Load();

                using (var store = new Store())
                {
                    store.Database.Initialize(true);
                    store.EnsureCreateTables();

                    InitializeKnowUsers(store);
                }

                Current.Recognitor = new FaceRecognition(CnnDetectorModelFile, RecognitionModelFile, LandmarksModelFile);
                var recognitor = Current.Recognitor;
                //var knowFace = Dlib.LoadJpeg<RgbPixel>(@"D:\projects\FaceRec\FaceRec\2.jpg");
                //var knowFaceEncodings = recognitor.FaceEncodings(knowFace, recognitor.FaceLocations(knowFace));
                //var a = knowFaceEncodings[0].ToBytes();
                //var b = a.FromBytes();

                //var inputStream = new MemoryStream(a);
                //var outputStream = new MemoryStream();
                //using (GZipStream compressionStream = new GZipStream(outputStream, CompressionMode.Compress))
                //{
                //    inputStream.CopyTo(compressionStream);
                //}
                //var c = outputStream.ToArray();

                //Current.KnownUsers = knowFaceEncodings;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void InitializeKnowUsers(Store store)
        {
            var userViews = store.UserViews.ToArray();
            foreach (var userView in userViews)
            {
                var inputStream = new MemoryStream(userView.Encoding);
                var outputStream = new MemoryStream();
                using (GZipStream decompressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(outputStream);
                }
                userView.Encoding = outputStream.ToArray();
            }
            Current.KnownUsers = userViews;
        }
    }
}
