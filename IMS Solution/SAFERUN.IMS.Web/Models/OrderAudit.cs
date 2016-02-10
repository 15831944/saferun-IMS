using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class OrderAuditPlan:Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string OrderKey { get; set; }
        [Required]
        public string AuditContent { get; set; }
        public string Department { get; set; }
        public string AuditResult { get; set; }
        public int Status { get; set; }
        public DateTime PlanAuditDate { get; set; }
        public DateTime? AuditDate { get; set; }
        public string AuditUser { get; set; }
        public string Remark { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }



        #region add

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
        #endregion

    
    }
}