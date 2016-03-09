using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class WorkProcessDetail:Entity
    {
        [Key]
        public int Id { get; set; }
        public int WorkProcessId { get; set; }
        [ForeignKey("WorkProcessId")]
        public virtual WorkProcess WorkProcess { get; set; }
        public int ProcessStepId { get; set; }
        [ForeignKey("ProcessStepId")]
        public virtual ProcessStep ProcessStep { get; set; }
        public string StepName { get; set; }

        public int? StationId { get; set; }
        [ForeignKey("StationId")]
        public virtual Station Station { get; set; }

        public decimal StandardElapsedTime { get; set; }

        public DateTime? StartingDateTime { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public decimal ElapsedTime { get; set; }
        public decimal WorkingTime { get; set; }
        public string Operator { get; set; }
        public string QCConfirm { get; set; }
        public DateTime? QCConfirmDateTime { get; set; }
        public string CompletedConfirm { get; set; }

        public int Status { get; set; }

        public string Remark { get; set; }

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