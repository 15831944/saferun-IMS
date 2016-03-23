using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(WorkDetailMetadata))]
    public partial class WorkDetail
    {
    }

    public partial class WorkDetailMetadata
    {
        [Display(Name = "物料编码")]
        public SKU ComponentSKU { get; set; }

        [Display(Name = "部套号")]
        public SKU ParentSKU { get; set; }

        [Display(Name = "生产任务单")]
        public Work Work { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "生产任务单号")]
        [MaxLength(30)]
        public string WorkNo { get; set; }

        [Display(Name = "生产任务单")]
        public int WorkId { get; set; }

        [Display(Name = "部套号")]
        public int ParentSKUId { get; set; }

        [Display(Name = "物料编码")]
        public int ComponentSKUId { get; set; }

        [Display(Name = "图纸编号")]
        [MaxLength(30)]
        public string GraphSKU { get; set; }

        [Display(Name = "图纸版本")]
        [MaxLength(30)]
        public string GraphVer { get; set; }

        [Display(Name = "BOM基本单位用量")]
        public decimal ConsumeQty { get; set; }

        [Display(Name = "库存量")]
        public decimal StockQty { get; set; }

        [Display(Name = "订单需求量")]
        public decimal RequirementQty { get; set; }
         [Display(Name = "生产数量")]
        public decimal? ProductionQty { get; set; }
         [Display(Name = "已完成数量")]
        public decimal? FinishedQty { get; set; }

        [Display(Name = "品牌")]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Display(Name = "表面处理")]
        [MaxLength(50)]
        public string Process { get; set; }

        [Display(Name = "物料负责单位")]
        [MaxLength(50)]
        public string Responsibility { get; set; }

        [Display(Name = "计划订单生产日期")]
        public DateTime AltOrderProdctionDate { get; set; }

        [Display(Name = "计划毛坯件生产日期")]
        public DateTime AltProdctionDate1 { get; set; }

        [Display(Name = "毛坯件确认完成日期")]
        public DateTime ActualProdctionDate1 { get; set; }

        [Display(Name = "计划精加工生产日期")]
        public DateTime AltProdctionDate2 { get; set; }

        [Display(Name = "精加工确认完成日期")]
        public DateTime ActualProdctionDate2 { get; set; }

        [Display(Name = "表面处理计划日期")]
        public DateTime AltProdctionDate3 { get; set; }

        [Display(Name = "表面处理确认完成日期")]
        public DateTime ActualProdctionDate3 { get; set; }

        [Display(Name = "装配计划完成日期")]
        public DateTime AltProdctionDate4 { get; set; }

        [Display(Name = "装配确认完成日期")]
        public DateTime ActualProdctionDate4 { get; set; }

        [Display(Name = "备用计划日期")]
        public DateTime AltProdctionDate5 { get; set; }

        [Display(Name = "备用确认完成日期")]
        public DateTime ActualProdctionDate5 { get; set; }
        [Display(Name = "状态")]
        public int Status { get; set; }
        [Display(Name = "确认人")]
        [MaxLength(20)]
        public string ConfirmUser { get; set; }

        [Display(Name = "备注")]
        [MaxLength(200)]
        public string Remark1 { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100)]
        public string Remark2 { get; set; }

        [Display(Name = "BomComponentId")]
        public int BomComponentId { get; set; }

        [Display(Name = "ParentBomComponentId")]
        public int ParentBomComponentId { get; set; }

        [Display(Name = "订单号")]
        [MaxLength(30)]
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




	public class WorkDetailChangeViewModel
    {
        public IEnumerable<WorkDetail> inserted { get; set; }
        public IEnumerable<WorkDetail> deleted { get; set; }
        public IEnumerable<WorkDetail> updated { get; set; }
    }

}
