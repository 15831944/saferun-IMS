using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class MadeType:Entity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}