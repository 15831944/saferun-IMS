using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class DefectTracking:Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProjectName { get; set; }
        public string OrderKey { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        public int SKUId { get; set; }
        [ForeignKey("SKUId")]
        public virtual SKU SKU { get; set; }
        [Required]
        public string ComponentSKU { get; set; }
        public string Supplier { get; set; }
        public string GraphSKU { get; set; }
        public decimal QCQty { get; set; }
        public decimal CheckedQty { get; set; }
        public decimal NGQty { get; set; }
        public int DefectTypeId{get;set;}
        [ForeignKey("DefectTypeId")]
        public virtual DefectType DefectType{get;set;}
        public int DefectCodeId { get; set; }
        [ForeignKey("DefectCodeId")]
        public virtual DefectCode DefectCode { get; set; }
        public string Locator { get; set; }
        public string DefectDesc { get; set; }

        public int Status { get; set; }

        public string Result { get; set; }
        public string Remark { get; set; }

        public string QCUser { get; set; }

        public DateTime TrackingDateTime { get; set; }
        public DateTime? CheckedDateTime { get; set; }
        public DateTime? CloseDateTime { get; set; }




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