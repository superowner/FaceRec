using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DlibDotNet;
using FaceRec.Core;

namespace FaceRec.Models
{
    [Table("vwUsers")]
    public class UserView
    {
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

        public string GroupName { get; set; }

    }
}
