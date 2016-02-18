using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class ProcessStep:Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index("IX_ProcessStep",IsUnique=true,Order=1)]
        public string Name { get; set; }
        [Index("IX_ProcessStep", IsUnique = true, Order = 2)]
        public int Order { get; set; }
        [Index("IX_ProcessStep", IsUnique = true, Order = 3)]
        public string StepName { get; set; }
        public decimal ElapsedTime { get; set; }
        public string Equipment { get; set; }

        public int Status { get; set; }
        public string Description { get; set; }



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