using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaceRec.Models
{
    [Table("tblUserGroups")]
    public class UserGroup
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
