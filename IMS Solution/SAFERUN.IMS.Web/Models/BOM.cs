using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class BOMComponent : Entity
    {
        public BOMComponent()
        {
            Components = new HashSet<BOMComponent>();
        }
        [Key]
        public int Id { get; set; }
        public string DesignName { get; set; }
        public string ComponentSKU { get; set; }
        public string ALTSku { get; set; }
        public string GraphSKU { get; set; }
        public string StockSKU { get; set; }
        public string MadeType { get; set; }
        public string SKUGroup { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public decimal ConsumeQty { get; set; }
        public decimal ConsumeTime { get; set; }

        public decimal RejectRatio { get; set; }

        public string Deploy { get; set; }

        public string Locator { get; set; }

        public string ProductionLine { get; set; }

        public int Status { get; set; }

        public bool NoPur { get; set; }
        [Required]
        public string FinishedSKU { get; set; } 

        [ScaffoldColumn(false)]
        public byte[] DesignPricture1 { get; set; }
        [ScaffoldColumn(false)]
        public string DesignPrictureURL1 { get; set; }

        [ScaffoldColumn(false)]
        public byte[] DesignPricture2 { get; set; }
        [ScaffoldColumn(false)]
        public string DesignPrictureURL2 { get; set; }
       

        public int SKUId { get; set; }
        [ForeignKey("SKUId")]
        public virtual SKU SKU { get; set; }


        public int? ParentComponentId { get; set; }
        [ForeignKey("ParentComponentId")]
        public virtual BOMComponent ParentComponent { get; set; }

        public virtual ICollection<BOMComponent> Components { get; set; }


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