using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class WorkProcess:Entity
    {
        public WorkProcess()
        {
            WorkProcessDetails = new HashSet<WorkProcessDetail>();
        }
        [Key]
        public int Id { get; set; }
        public string WorkNo { get; set; }
        public int WorkId { get; set; }
        [ForeignKey("WorkId")]
        public virtual Work Work { get; set; }
        public int OrderId { get; set; }
        public string OrderKey { get; set; }
         [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
         public string ProjectName { get; set; }
         public int SKUId { get; set; }
         [ForeignKey("SKUId")]
         public virtual SKU SKU { get; set; }
         public string GraphSKU { get; set; }
         public decimal RequirementQty { get; set; }
         public decimal ProductionQty { get; set; }
         public decimal FinishedQty { get; set; }
         public int WorkItems { get; set; }
         public int? ProductionProcessId { get; set; }
         [ForeignKey("ProductionProcessId")]
         public virtual ProductionProcess ProductionProcess { get; set; }
         public int Status { get; set; }
         public string Operator { get; set; }
         public DateTime WorkDate { get; set; }
         public string Remark { get; set; }

         public int? WorkDetailId { get; set; }
         [ForeignKey("WorkDetailId")]
         public virtual WorkDetail WorkDetail { get; set; }
         public int CustomerId { get; set; }
         [ForeignKey("CustomerId")]
         public virtual Customer Customer { get; set; }


         public virtual ICollection<WorkProcessDetail> WorkProcessDetails { get; set; }

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