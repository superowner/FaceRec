using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DlibDotNet;
using FaceRec.Core;

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
        public Matrix<float> FaceEncoding
        {
            get
            {
                return this.Encoding.FromBytes();
            }
        }

        public byte[] Encoding { get; set; }

        //[ForeignKey("UserId")]
        //public ICollection<FaceEncoding> FaceEncodings { get; set; }

        public string GroupId { get; set; }

        //[ForeignKey("GroupId")]
        //public UserGroup Group { get; set; }


        public string Comment { get; set; }
    }
}
