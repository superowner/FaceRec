using DlibDotNet;
using DlibDotNet.Tools;
using FaceRec.Core;
using FaceRec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var faceEncodings = store.FaceEncodings.ToArray();
            foreach (var userView in userViews)
            {
                var userFaceEncodings = faceEncodings.Where(fe => fe.UserId == userView.Id).ToArray();
                userView.Encoding = new Matrix<double>(128, 150);
                foreach (var faceEncoding in userFaceEncodings)
                {
                    userView.Encoding[faceEncoding.Row, faceEncoding.Column] = faceEncoding.Value;
                }
            }
            Current.KnownUsers = userViews;
        }
    }
}
