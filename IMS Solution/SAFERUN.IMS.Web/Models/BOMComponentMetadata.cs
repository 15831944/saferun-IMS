using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(BOMComponentMetadata))]
    public partial class BOMComponent
    {
    }

    public partial class BOMComponentMetadata
    {
        [Display(Name = "子零件")]
        public BOMComponent Components { get; set; }

        [Display(Name = "父零件")]
        public BOMComponent ParentComponent { get; set; }

        [Display(Name = "SKU")]
        public SKU SKU { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 设计名称")]
        [Display(Name = "设计名称")]
        [MaxLength(30)]
        public string DesignName { get; set; }

        [Required(ErrorMessage = "Please enter : 零件SKU")]
        [Display(Name = "零件SKU")]
        [MaxLength(30)]
        public string ComponentSKU { get; set; }

        [Display(Name = "替代Sku")]
        [MaxLength(30)]
        public string ALTSku { get; set; }

        [Required(ErrorMessage = "Please enter : 图纸编号")]
        [Display(Name = "图纸编号")]
        [MaxLength(50)]
        public string GraphSKU { get; set; }

        [Required(ErrorMessage = "Please enter : 库存编号")]
        [Display(Name = "库存编号")]
        [MaxLength(30)]
        public string StockSKU { get; set; }
   
        [Display(Name = "生产方式(自制/外协/采购)")]
        [MaxLength(20)]
        public string MadeType { get; set; }

        [Display(Name = "组别")]
        [MaxLength(20)]
        public string SKUGroup { get; set; }

        [Display(Name = "备注1")]
        [MaxLength(300)]
        public string Remark1 { get; set; }

        [Display(Name = "备注2")]
        [MaxLength(300)]
        public string Remark2 { get; set; }
        [Display(Name = "成品SKU")]
        [MaxLength(30)]
        public string FinishedSKU { get; set; } 

        [Display(Name = "用量")]
        public decimal ConsumeQty { get; set; }

        [Display(Name = "生产耗时(H)")]
        public decimal ConsumeTime { get; set; }

        [Display(Name = "不良率")]
        public decimal RejectRatio { get; set; }

        [Display(Name = "装配位置")]
        public string Deploy { get; set; }

        [Display(Name = "工序代码")]
        public string Locator { get; set; }

        [Display(Name = "生产线")]
        public string ProductionLine { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "发料")]
        public bool NoPur { get; set; }

        [Display(Name = "设计图纸文件1")]
        public byte[] DesignPricture1 { get; set; }

        [Display(Name = "设计图纸文件1")]
        public string DesignPrictureURL1 { get; set; }

        [Display(Name = "设计图纸文件2")]
        public byte[] DesignPricture2 { get; set; }

        [Display(Name = "设计图纸文件2")]
        public string DesignPrictureURL2 { get; set; }

        [Display(Name = "零件SKU")]
        public int SKUId { get; set; }

        [Display(Name = "父件SKU")]
        public int ParentComponentId { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class BOMComponentChangeViewModel
    {
        public IEnumerable<BOMComponent> inserted { get; set; }
        public IEnumerable<BOMComponent> deleted { get; set; }
        public IEnumerable<BOMComponent> updated { get; set; }
    }

}
