using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(RepairJobMetadata))]
    public partial class RepairJob
    {
    }

    public partial class RepairJobMetadata
    {
        [Display(Name = "订单编号")]
        public Order Order { get; set; }

        [Display(Name = "物料编号")]
        public SKU SKU { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "订单编号")]
        [MaxLength(30)]
        public string OrderKey { get; set; }

        [Display(Name = "项目名称")]
        [MaxLength(50)]
        public string ProjectName { get; set; }

        [Display(Name = "物料编号")]
        public int SKUId { get; set; }

        [Display(Name = "图纸编号")]
        [MaxLength(30)]
        public string GraphSKU { get; set; }

        [Display(Name = "设计名称")]
        [MaxLength(50)]
        public string DesignName { get; set; }

        [Display(Name = "数量")]
        public decimal RepairQty { get; set; }

        [Display(Name = "返修问题")]
        [MaxLength(200)]
        public string Issue { get; set; }

        [Display(Name = "处理工序")]
        [MaxLength(50)]
        public string ProcessName { get; set; }

        [Display(Name = "责任部门")]
        [MaxLength(20)]
        public string ResponsibleDepartment { get; set; }

        [Display(Name = "返修人员")]
        [MaxLength(20)]
        public string Owner { get; set; }

        [Display(Name = "开始日期")]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "结束日期")]
        public DateTime CompletedDateTime { get; set; }

        [Display(Name = "用时")]
        public decimal ElapsedTime { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "备注")]
        [MaxLength(200)]
        public string Remark { get; set; }

        [Display(Name = "订单编号")]
        public int OrderId { get; set; }

        [Display(Name = "BomComponentId")]
        public int BomComponentId { get; set; }

        [Display(Name = "ParentBomComponentId")]
        public int ParentBomComponentId { get; set; }

    }




	public class RepairJobChangeViewModel
    {
        public IEnumerable<RepairJob> inserted { get; set; }
        public IEnumerable<RepairJob> deleted { get; set; }
        public IEnumerable<RepairJob> updated { get; set; }
    }

}
