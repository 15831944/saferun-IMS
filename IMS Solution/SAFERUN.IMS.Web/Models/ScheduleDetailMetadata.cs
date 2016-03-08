using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(ScheduleDetailMetadata))]
    public partial class ScheduleDetail
    {
    }

    public partial class ScheduleDetailMetadata
    {
        [Display(Name = "零件编号")]
        public SKU ComponentSKU { get; set; }

        [Display(Name = "部套编号")]
        public SKU ParentSKU { get; set; }

        [Display(Name = "生产派车")]
        public ProductionSchedule ProductionSchedule { get; set; }

        [Display(Name = "班别")]
        public Shift Shift { get; set; }

        [Display(Name = "机台")]
        public Station Station { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "生产排程版本号")]
        [MaxLength(30)]
        public string ScheduleNo { get; set; }

        [Display(Name = "生产任务单号")]
        [MaxLength(30)]
        public string WorkNo { get; set; }

        [Display(Name = "部套编号")]
        public int ParentSKUId { get; set; }

        [Display(Name = "零件编号")]
        public int ComponentSKUId { get; set; }

        [Display(Name = "图纸编号")]
        [MaxLength(30)]
        public string GraphSKU { get; set; }

        [Display(Name = "图纸版本号")]
        [MaxLength(20)]
        public string GraphVer { get; set; }
        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "单位用量")]
        public decimal ConsumeQty { get; set; }

        [Display(Name = "库存量")]
        public decimal StockQty { get; set; }

        [Display(Name = "订单需求量")]
        public decimal RequirementQty { get; set; }

        [Display(Name = "排程生产量")]
        public decimal ScheduleProductionQty { get; set; }

        [Display(Name = "实际生产量")]
        public decimal ActualProductionQty { get; set; }

        [Display(Name = "机台")]
        public int StationId { get; set; }

        [Display(Name = "班别")]
        public int ShiftId { get; set; }

        [Display(Name = "操作员")]
        [MaxLength(20)]
        public string Operator { get; set; }

        [Display(Name = "毛坯件计划完工日期")]
        public DateTime AltProdctionDate1 { get; set; }

        [Display(Name = "毛坯件确认完工日期")]
        public DateTime ActualProdctionDate1 { get; set; }

        [Display(Name = "计划完成日期")]
        public DateTime AltProdctionDate2 { get; set; }

        [Display(Name = "实际完成日期")]
        public DateTime ActualProdctionDate2 { get; set; }

        [Display(Name = "备用日期")]
        public DateTime AltProdctionDate3 { get; set; }

        [Display(Name = "备用日期")]
        public DateTime ActualProdctionDate3 { get; set; }

        [Display(Name = "预估工艺工时")]
        public decimal AltConsumeTime { get; set; }

        [Display(Name = "实际工时")]
        public decimal ActualConsumeTime { get; set; }

        [Display(Name = "备注1")]
        [MaxLength(100)]
        public string Remark1 { get; set; }

        [Display(Name = "备注2")]
        [MaxLength(100)]
        public string Remark2 { get; set; }

        [Display(Name = "ProductionScheduleId")]
        public int ProductionScheduleId { get; set; }

        [Display(Name = "BomComponentId")]
        public int BomComponentId { get; set; }

        [Display(Name = "ParentBomComponentId")]
        public int ParentBomComponentId { get; set; }

        [Display(Name = "OrderKey")]
        public string OrderKey { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class ScheduleDetailChangeViewModel
    {
        public IEnumerable<ScheduleDetail> inserted { get; set; }
        public IEnumerable<ScheduleDetail> deleted { get; set; }
        public IEnumerable<ScheduleDetail> updated { get; set; }
    }

}
