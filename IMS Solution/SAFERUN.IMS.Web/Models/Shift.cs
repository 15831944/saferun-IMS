using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class Shift:Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index("IX_Shift")]
        public string Name { get; set; }
        public string OnTime { get; set; }
        public string OffTime { get; set; }
        public string Remark { get; set; }

    }
}