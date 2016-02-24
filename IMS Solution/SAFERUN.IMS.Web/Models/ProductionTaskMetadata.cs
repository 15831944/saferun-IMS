using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(ProductionTaskMetadata))]
    public partial class ProductionTask
    {
    }

    public partial class ProductionTaskMetadata
    {
        [Display(Name = "订单")]
        public Order Order { get; set; }

        [Display(Name = "物料编号")]
        public SKU SKU { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "订单号")]
        [MaxLength(20)]
        public string OrderKey { get; set; }

        [Display(Name = "物料编号")]
        public int SKUId { get; set; }

        [Display(Name = "物料名称")]
        [MaxLength(30)]
        public string DesignName { get; set; }

        [Display(Name = "物料编号")]
        [MaxLength(30)]
        public string ComponentSKU { get; set; }

        [Display(Name = "替代物料")]
        [MaxLength(30)]
        public string ALTSku { get; set; }

        [Display(Name = "图纸编号")]
        [MaxLength(30)]
        public string GraphSKU { get; set; }

        [Display(Name = "处理工艺")]
        [MaxLength(50)]
        public string ProcessName { get; set; }

        [Display(Name = "工艺次序")]
        public int ProcessOrder { get; set; }

        [Display(Name = "处理步骤")]
        [MaxLength(50)]
        public string ProcessSetp { get; set; }

        [Display(Name = "评估用时(H)")]
        public decimal AltElapsedTime { get; set; }

        [Display(Name = "加工生产线")]
        [MaxLength(50)]
        public string ProductionLine { get; set; }

        [Display(Name = "加工设备")]
        [MaxLength(50)]
        public string Equipment { get; set; }

        [Display(Name = "订单计划日期")]
        public DateTime OrderPlanDate { get; set; }

        [Display(Name = "负责人")]
        [MaxLength(30)]
        public string Owner { get; set; }

        [Display(Name = "计划开始日期")]
        public DateTime PlanStartDateTime { get; set; }

        [Display(Name = "计划结束日期")]
        public DateTime PlanCompletedDateTime { get; set; }

        [Display(Name = "实际开始日期")]
        public DateTime ActualStartDateTime { get; set; }

        [Display(Name = "实际完成日期")]
        public DateTime ActualCompletedDateTime { get; set; }

        [Display(Name = "实际用时")]
        public decimal ActualElapsedTime { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "异常")]
        [MaxLength(200)]
        public string Issue { get; set; }

        [Display(Name = "备注")]
        [MaxLength(200)]
        public string Remark { get; set; }

        [Display(Name = "订单编号")]
        public int OrderId { get; set; }

        [Display(Name = "BomComponentId")]
        public int BomComponentId { get; set; }

        [Display(Name = "ParentBomComponentId")]
        public int ParentBomComponentId { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class ProductionTaskChangeViewModel
    {
        public IEnumerable<ProductionTask> inserted { get; set; }
        public IEnumerable<ProductionTask> deleted { get; set; }
        public IEnumerable<ProductionTask> updated { get; set; }
    }

}
