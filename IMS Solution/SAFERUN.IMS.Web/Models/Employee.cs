using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class Employee:Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
         public string Name { get; set; }
        [Required]
        [Index("IX_Employee",IsUnique=true)]
         public string WorkNo { get; set; }
         public string Title { get; set; }
         public DateTime BirthDate { get; set; }
         public string MaritalStatus { get; set; }
         public string Gender { get; set; }
         public DateTime HireDate { get; set; }


         public int? ManagerID { get; set; }

         [ForeignKey("ManagerID")]
         public Employee Manager { get; set; }




         [ScaffoldColumn(false)]
         [Display(Name = "新增用户", Description = "新增用户")]
         [StringLength(20)]
         public string CreatedUserId { get; set; }
         [ScaffoldColumn(false)]
         [Display(Name = "新增时间", Description = "新增时间")]
         public DateTime? CreatedDateTime { get; set; }
         [ScaffoldColumn(false)]
         [Display(Name = "最后修改用户", Description = "最后修改用户")]
         [StringLength(20)]
         public string LastEditUserId { get; set; }
         [ScaffoldColumn(false)]
         [Display(Name = "最后修改时间", Description = "最后修改时间")]
         public DateTime? LastEditDateTime { get; set; }

    }
}