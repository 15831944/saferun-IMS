using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class ProductionTask:Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string OrderKey { get; set; }
        public int SKUId { get; set; }
        [ForeignKey("SKUId")]
        public virtual SKU SKU { get; set; }
        public string DesignName { get; set; }
        public string ComponentSKU { get; set; }
        public string ALTSku { get; set; }
        public string GraphSKU { get; set; }
        public string ProcessName { get; set; }
        public int ProcessOrder { get; set; }
        public string ProcessSetp { get; set; }
        public decimal AltElapsedTime { get; set; }
        public string ProductionLine { get; set; }
        public string Equipment { get; set; }
        public DateTime OrderPlanDate { get; set; }
        public string Owner { get; set; }
        public DateTime? PlanStartDateTime { get; set; }
        public DateTime? PlanCompletedDateTime { get; set; }
        public DateTime? ActualStartDateTime { get; set; }
        public DateTime? ActualCompletedDateTime { get; set; }
        public decimal? ActualElapsedTime { get; set; }

        public int Status { get; set; }
        public string Issue { get; set; }
        public string Remark { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ScaffoldColumn(false)]
        public int BomComponentId { get; set; }
        [ScaffoldColumn(false)]
        public int? ParentBomComponentId { get; set; }




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