using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(DefectTrackingMetadata))]
    public partial class DefectTracking
    {
    }

    public partial class DefectTrackingMetadata
    {
        [Display(Name = "缺陷代码")]
        public DefectCode DefectCode { get; set; }

        [Display(Name = "缺陷类型")]
        public DefectType DefectType { get; set; }

        [Display(Name = "订单")]
        public Order Order { get; set; }

        [Display(Name = "SKU")]
        public SKU SKU { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "项目名称")]
        [MaxLength(50)]
        public string ProjectName { get; set; }

        [Display(Name = "订单编号")]
        [MaxLength(30)]
        public string OrderKey { get; set; }

        [Display(Name = "订单编号")]
        public int OrderId { get; set; }

        [Display(Name = "零件SKU")]
        public int SKUId { get; set; }

        [Display(Name = "零件SKU")]
        [MaxLength(30)]
        public string ComponentSKU { get; set; }

        [Display(Name = "供应商")]
        [MaxLength(50)]
        public string Supplier { get; set; }

        [Display(Name = "图纸编号")]
        [MaxLength(30)]
        public string GraphSKU { get; set; }

        [Required(ErrorMessage = "Please enter : 送检数量")]
        [Display(Name = "送检数量")]
        public decimal QCQty { get; set; }

        [Display(Name = "检验数量")]
        public decimal CheckedQty { get; set; }

        [Display(Name = "不良数量")]
        public decimal NGQty { get; set; }

        [Display(Name = "缺陷类型")]
        public int DefectTypeId { get; set; }

        [Display(Name = "缺陷代码")]
        public int DefectCodeId { get; set; }

        [Display(Name = "工艺")]
        [MaxLength(50)]
        public string Locator { get; set; }

        [Display(Name = "缺陷描述")]
        [MaxLength(100)]
        public string DefectDesc { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "处理结果")]
        [MaxLength(100)]
        public string Result { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100)]
        public string Remark { get; set; }

        [Display(Name = "QC人员")]
        [MaxLength(20)]
        public string QCUser { get; set; }

        [Required(ErrorMessage = "Please enter : 记录时间")]
        [Display(Name = "记录时间")]
        public DateTime TrackingDateTime { get; set; }

        [Display(Name = "QC完成时间")]
        public DateTime CheckedDateTime { get; set; }

        [Display(Name = "关闭时间")]
        public DateTime CloseDateTime { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class DefectTrackingChangeViewModel
    {
        public IEnumerable<DefectTracking> inserted { get; set; }
        public IEnumerable<DefectTracking> deleted { get; set; }
        public IEnumerable<DefectTracking> updated { get; set; }
    }

}
