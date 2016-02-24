using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class SKU:Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index("IX_SKU",IsUnique=true)]
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public string ALTSku { get; set; }
        public string ManufacturerSku { get; set; }
        public string Model { get; set; }
        public string CLASS { get; set; }
        public string SKUGroup { get; set; }
        [Required]
        public string MadeType { get; set; }
        public string STDUOM { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }

        public string PackKey { get; set; }
        public bool QCLOC { get; set; }

        public bool QCLOCOUT { get; set; } 
      
        public bool Active{get;set;}
   
        public decimal Price { get; set; }
        public decimal Cost{get;set;}

         

        public decimal STDGrossWGT {get;set;}
        public decimal STDNetWGT{get;set;}
        public decimal STDCube{get;set;}

        public int SUSR1 { get; set; }
        public string SUSR2 { get; set; }
        public string SUSR3 { get; set; }
        public string SUSR4 { get; set; }
        public DateTime? SUSR5 { get; set; }





        #region add

        [ScaffoldColumn(false)]
        [Display(Name = "新增用户", Description = "新增用户")]
        [StringLength(20)]
        public string CreatedUserId { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "新增时间", Description = "新增时间")]
        public DateTime? CreatedDateTime { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "最后修改用户", Description = "最后修改用户")]
        [StringLength(20)]
        public string LastEditUserId { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "最后修改时间", Description = "最后修改时间")]
        public DateTime? LastEditDateTime { get; set; }
        #endregion
    }
}