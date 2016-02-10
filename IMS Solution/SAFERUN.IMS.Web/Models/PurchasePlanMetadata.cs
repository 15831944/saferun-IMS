using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(PurchasePlanMetadata))]
    public partial class PurchasePlan
    {
    }

    public partial class PurchasePlanMetadata
    {
        [Display(Name = "订单")]
        public Order Order { get; set; }

        [Display(Name = "采购料件")]
        public SKU SKU { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "订单号")]
        [MaxLength(20)]
        public string OrderKey { get; set; }

        [Display(Name = "采购料件")]
        public int SKUId { get; set; }

        [Display(Name = "图纸名称")]
        [MaxLength(30)]
        public string DesignName { get; set; }

        [Display(Name = "组件号")]
        [MaxLength(30)]
        public string ComponentSKU { get; set; }

        [Display(Name = "替代料")]
        [MaxLength(30)]
        public string ALTSku { get; set; }

        [Display(Name = "图纸编号")]
        [MaxLength(30)]
        public string GraphSKU { get; set; }

        [Display(Name = "库存编号")]
        [MaxLength(30)]
        public string StockSKU { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "单位用量")]
        public decimal ConsumeQty { get; set; }

        [Display(Name = "需求数量")]
        public decimal RequirementQty { get; set; }

        [Display(Name = "不良率")]
        public decimal RejectRatio { get; set; }

        [Display(Name = "装配位置")]
        [MaxLength(50)]
        public string Deploy { get; set; }

        [Display(Name = "表面处理")]
        [MaxLength(30)]
        public string Locator { get; set; }

        [Display(Name = "品牌")]
        [MaxLength(20)]
        public string Brand { get; set; }

        [Display(Name = "供应商")]
        [MaxLength(20)]
        public string Supplier { get; set; }

        [Display(Name = "计划完成日期")]
        public DateTime OrderPlanDate { get; set; }

        [Display(Name = "计划到货日期")]
        public DateTime PlanDeliveryDate { get; set; }

        [Display(Name = "设计到货日期")]
        public DateTime ActualDeliveryDate { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100)]
        public string Remark { get; set; }

        [Display(Name = "订单")]
        public int OrderId { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class PurchasePlanChangeViewModel
    {
        public IEnumerable<PurchasePlan> inserted { get; set; }
        public IEnumerable<PurchasePlan> deleted { get; set; }
        public IEnumerable<PurchasePlan> updated { get; set; }
    }

}
