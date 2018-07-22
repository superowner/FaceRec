using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DlibDotNet;

namespace FaceRec.Models
{
    [Table("tblUsers")]
    public class User
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public byte[] Face { get; set; }

        [NotMapped]
        public Matrix<double> Encoding { get; set; }


        //[ForeignKey("UserId")]
        //public ICollection<FaceEncoding> FaceEncodings { get; set; }

        //[NotMapped]
        //public Matrix<float> Encoding {
        //    get
        //    {
        //        var matrix = new Matrix<float>(128, 150);
        //        foreach (var faceEncoding in this.FaceEncodings)
        //        {
        //            matrix[faceEncoding.Row, faceEncoding.Row] = faceEncoding.Value;
        //        }

        //        return matrix;
        //    }
        //}

        public string GroupId { get; set; }

        //[ForeignKey("GroupId")]
        //public UserGroup Group { get; set; }


        public string Comment { get; set; }
    }
}
