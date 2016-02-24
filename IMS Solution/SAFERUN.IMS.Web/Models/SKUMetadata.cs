using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(SKUMetadata))]
    public partial class SKU
    {
    }

    public partial class SKUMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : Sku")]
        [Display(Name = "Sku")]
        [MaxLength(30)]
        public string Sku { get; set; }
        [Display(Name = "品名")]
        [MaxLength(50)]
        public string ProductName { get; set; }
        [Display(Name = "替代料")]
        [MaxLength(30)]
        public string ALTSku { get; set; }

        [Display(Name = "图纸编号")]
        [MaxLength(50)]
        public string ManufacturerSku { get; set; }

        [Display(Name = "型号")]
        [MaxLength(20)]
        public string Model { get; set; }

        [Display(Name = "类型")]
        [MaxLength(20)]
        public string CLASS { get; set; }

        [Display(Name = "组别")]
        [MaxLength(20)]
        public string SKUGroup { get; set; }

        [Required(ErrorMessage = "Please enter : 自制/外协/采购")]
        [Display(Name = "自制/外协/采购")]
        [MaxLength(20)]
        public string MadeType { get; set; }

        [Display(Name = "单位")]
        [MaxLength(10)]
        public string STDUOM { get; set; }

        [Display(Name = "描述")]
        [MaxLength(100)]
        public string Description { get; set; }

        [Display(Name = "品牌")]
        [MaxLength(30)]
        public string Brand { get; set; }

        [Display(Name = "包装")]
        [MaxLength(20)]
        public string PackKey { get; set; }

        [Display(Name = "入库QC")]
        public bool QCLOC { get; set; }

        [Display(Name = "出库QC")]
        public bool QCLOCOUT { get; set; }

        [Display(Name = "是否可用")]
        public bool Active { get; set; }

        [Display(Name = "单价")]
        public decimal Price { get; set; }

        [Display(Name = "成本")]
        public decimal Cost { get; set; }

        [Display(Name = "毛重")]
        public decimal STDGrossWGT { get; set; }

        [Display(Name = "净重")]
        public decimal STDNetWGT { get; set; }

        [Display(Name = "体积")]
        public decimal STDCube { get; set; }

        [Display(Name = "SUSR1")]
        public int SUSR1 { get; set; }

        [Display(Name = "SUSR2")]
        [MaxLength(50)]
        public string SUSR2 { get; set; }

        [Display(Name = "SUSR3")]
        [MaxLength(50)]
        public string SUSR3 { get; set; }

        [Display(Name = "SUSR4")]
        [MaxLength(50)]
        public string SUSR4 { get; set; }

        [Display(Name = "SUSR5")]
        public DateTime SUSR5 { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class SKUChangeViewModel
    {
        public IEnumerable<SKU> inserted { get; set; }
        public IEnumerable<SKU> deleted { get; set; }
        public IEnumerable<SKU> updated { get; set; }
    }

}
