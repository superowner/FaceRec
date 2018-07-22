using DlibDotNet;
using FaceRec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRec.Core
{
    public static class MatrixExtensions
    {
        public const int Rows = 128;
        public const int Columns = 150;

        //public static T[] ToBytes<T>(this Matrix<T> matrix)
        //    where T : struct
        //{
        //    var output = new T[matrix.Size];

        //    for (int i = 0; i < matrix.Rows; i++)
        //    {
        //        for (int j = 0; j < matrix.Columns; j++)
        //        {
        //            output[i * matrix.Rows + j] = matrix[i, j];
        //        }
        //    }

        //    return output;
        //}

        //public static Matrix<double> ToMatrix(this FaceEncoding[] faceEncodings)
        //{
        //    var matrix = new Matrix<double>(Rows, Columns);
        //    for (int i = 0; i < Rows; i++)
        //    {
        //        for (int j = 0; j < Columns; j++)
        //        {
        //            matrix[i,j]
        //        }
        //    }
        //    foreach (var faceEncoding in userFaceEncodings)
        //    {
        //        user.Encoding[faceEncoding.Row, faceEncoding.Row] = faceEncoding.Value;
        //    }
        //}
    }
}
