using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(AssemblyPlanMetadata))]
    public partial class AssemblyPlan
    {
    }

    public partial class AssemblyPlanMetadata
    {
        [Display(Name = "订单")]
        public Order Order { get; set; }

        [Display(Name = "物料编码")]
        public SKU SKU { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "订单编号")]
        [MaxLength(20)]
        public string OrderKey { get; set; }

        [Display(Name = "物料编码")]
        public int SKUId { get; set; }

        [Display(Name = "部套名称")]
        [MaxLength(50)]
        public string DesignName { get; set; }

        [Display(Name = "物料编码")]
        [MaxLength(30)]
        public string ComponentSKU { get; set; }

        [Display(Name = "成品编码")]
        [MaxLength(30)]
        public string FinishedSKU { get; set; }

        [Display(Name = "订单日期")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "需求日期")]
        public DateTime RequirementDate { get; set; }

        [Display(Name = "图纸分解时间")]
        public DateTime ResolveDate1 { get; set; }

        [Display(Name = "图纸下发时间")]
        public DateTime ActualReleaseDate1 { get; set; }

        [Display(Name = "要求价格交期")]
        public DateTime SetPlanDate2 { get; set; }

        [Display(Name = "入库时间")]
        public DateTime PutawayDate2 { get; set; }

        [Display(Name = "要求价格交期")]
        public DateTime SetPlanDate3 { get; set; }

        [Display(Name = "入库时间")]
        public DateTime PutawayDate3 { get; set; }

        [Display(Name = "制定计划时间")]
        public DateTime SetPlanDate4 { get; set; }

        [Display(Name = "入库时间")]
        public DateTime PutawayDate4 { get; set; }

        [Display(Name = "制定计划时间")]
        public DateTime SetPlanDate5 { get; set; }

        [Display(Name = "原料到货时间")]
        public DateTime DeliveryDate5 { get; set; }

        [Display(Name = "入库时间")]
        public DateTime PutawayDate5 { get; set; }

        [Display(Name = "制定计划时间")]
        public DateTime SetPlanDate6 { get; set; }

        [Display(Name = "实际开始时间")]
        public DateTime ActualStartDate6 { get; set; }

        [Display(Name = "实际结束时间")]
        public DateTime ActualCompletedDate6 { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100)]
        public string Remark { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "订单号")]
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




	public class AssemblyPlanChangeViewModel
    {
        public IEnumerable<AssemblyPlan> inserted { get; set; }
        public IEnumerable<AssemblyPlan> deleted { get; set; }
        public IEnumerable<AssemblyPlan> updated { get; set; }
    }

}
