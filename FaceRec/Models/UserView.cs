using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRec.Models
{
    [Table("vwUsers")]
    public class UserView : User
    {
        public string GroupName { get; set; }

    }
}
