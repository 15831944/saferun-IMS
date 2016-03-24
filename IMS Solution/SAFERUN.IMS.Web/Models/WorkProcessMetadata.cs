using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(WorkProcessMetadata))]
    public partial class WorkProcess
    {
    }

    public partial class WorkProcessMetadata
    {
        [Display(Name = "客户")]
        public Customer Customer { get; set; }

        [Display(Name = "订单")]
        public Order Order { get; set; }

        [Display(Name = "生产工序")]
        public ProductionProcess ProductionProcess { get; set; }

        [Display(Name = "零件")]
        public SKU SKU { get; set; }

        [Display(Name = "生产任务单")]
        public Work Work { get; set; }

        [Display(Name = "生产任务单明细")]
        public WorkDetail WorkDetail { get; set; }

        [Display(Name = "零件工序流程卡明细")]
        public WorkProcessDetail WorkProcessDetails { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "生产任务单号")]
        [MaxLength(30)]
        public string WorkNo { get; set; }

        [Display(Name = "生产任务单")]
        public int WorkId { get; set; }

        [Display(Name = "订单")]
        public int OrderId { get; set; }

        [Display(Name = "订单号")]
        [MaxLength(30)]
        public string OrderKey { get; set; }

        [Display(Name = "项目名称")]
        [MaxLength(50)]
        public string ProjectName { get; set; }

        [Display(Name = "零件号")]
        public int SKUId { get; set; }

        [Display(Name = "图纸编号")]
        [MaxLength(30)]
        public string GraphSKU { get; set; }
        [Display(Name = "需求数量")]
        public decimal RequirementQty { get; set; }
        [Display(Name = "生产数量")]
        public decimal ProductionQty { get; set; }

        [Display(Name = "已完成数量")]
        public decimal FinishedQty { get; set; }

        [Display(Name = "生产任务数")]
        public int WorkItems { get; set; }

        [Display(Name = "生产工序")]
        public int ProductionProcessId { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "操作员")]
        [MaxLength(30)]
        public string Operator { get; set; }

        [Display(Name = "工单日期")]
        public DateTime WorkDate { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100)]
        public string Remark { get; set; }

        [Display(Name = "任务单编号")]
        public int WorkDetailId { get; set; }

        [Display(Name = "客户")]
        public int CustomerId { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class WorkProcessChangeViewModel
    {
        public IEnumerable<WorkProcess> inserted { get; set; }
        public IEnumerable<WorkProcess> deleted { get; set; }
        public IEnumerable<WorkProcess> updated { get; set; }
    }

}
