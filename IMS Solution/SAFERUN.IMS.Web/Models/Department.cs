using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class Department:Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index("IX_Department", IsUnique = true)]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}