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
        public const int BytesOfFloat = 4;

        public static byte[] ToBytes(this Matrix<float> matrix)
        {

            var output = new byte[matrix.Size * BytesOfFloat];

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    var bytes = BitConverter.GetBytes(matrix[i, j]);
                    Array.Copy(bytes, 0, output, (i * matrix.Rows + j) * BytesOfFloat, BytesOfFloat);
                }
            }

            return output;
        }

        public static Matrix<float> FromBytes(this byte[] bytes)
        {
            var matrix = new Matrix<float>(Rows, Columns);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    matrix[i, j] = BitConverter.ToSingle(bytes, (i * matrix.Rows + j) * BytesOfFloat);
                }
            }

            return matrix;
        }
    }
}
