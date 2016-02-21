using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class PurchasePlan:Entity
    {
        [Key]
        public int Id { get; set; }
        public string OrderKey { get; set; }

        public int SKUId { get; set; }
        public virtual SKU SKU { get; set; }
        public string DesignName { get; set; }
        public string ComponentSKU { get; set; }
        public string ALTSku { get; set; }
        public string GraphSKU { get; set; }
        public string StockSKU { get; set; }
        public int Status { get; set; }
        public decimal ConsumeQty { get; set; }

        public decimal RequirementQty { get; set; }

        public decimal RejectRatio { get; set; }

        public string Deploy { get; set; }

        public string Locator { get; set; }

        public string Brand { get; set; }
        public string Supplier { get; set; }
        public DateTime OrderPlanDate { get; set; }
        public DateTime PlanDeliveryDate { get; set; }

        public DateTime? ActualDeliveryDate { get; set; }

        public string Remark { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }


        [ScaffoldColumn(false)]
        public int BomComponentId { get; set; }
        [ScaffoldColumn(false)]
        public int? ParentBomComponentId { get; set; }



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