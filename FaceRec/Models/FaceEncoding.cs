using DlibDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRec.Models
{
    [Table("tblFaceEncodings")]
    public class FaceEncoding
    {
        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public double Value { get; set; }
    }
}
