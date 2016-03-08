using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class WorkDetail:Entity
    {
        public int Id { get; set; }
        public string WorkNo { get; set; }
        [Index("IX_WorkDetail",IsUnique=true, Order=1)]
        public int WorkId { get; set; }
        [ForeignKey("WorkId")]
        public virtual Work Work { get; set; }
        [Index("IX_WorkDetail", IsUnique = true, Order = 2)]
        public int ParentSKUId { get; set; }
        [ForeignKey("ParentSKUId")]
        public virtual SKU ParentSKU { get; set; }
        
        [Index("IX_WorkDetail", IsUnique = true, Order = 3)]
        public int ComponentSKUId { get; set; }
        [ForeignKey("ComponentSKUId")]
        public virtual SKU ComponentSKU { get; set; }
       

        public string GraphSKU { get; set; }
        public string GraphVer {get;set;}

        public decimal ConsumeQty { get; set; }

        public decimal StockQty { get; set; }
        public decimal RequirementQty { get; set; }

        public string Brand { get; set; }

        public string Process { get; set; }

        public string Responsibility { get; set; }

        public DateTime? AltOrderProdctionDate { get; set; }

        public DateTime? AltProdctionDate1 { get; set; }
        public DateTime? ActualProdctionDate1 { get; set; }
        public DateTime? AltProdctionDate2 { get; set; }
        public DateTime? ActualProdctionDate2 { get; set; }
        public DateTime? AltProdctionDate3 { get; set; }
        public DateTime? ActualProdctionDate3 { get; set; }
        public DateTime? AltProdctionDate4 { get; set; }
        public DateTime? ActualProdctionDate4 { get; set; }
        public DateTime? AltProdctionDate5 { get; set; }
        public DateTime? ActualProdctionDate5 { get; set; }
        
        

        
        
       

        public string ConfirmUser { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }

        public int Status { get; set; }

        [ScaffoldColumn(false)]
        public int BomComponentId { get; set; }
        [ScaffoldColumn(false)]
        public int? ParentBomComponentId { get; set; }
        [ScaffoldColumn(false)]
        public string OrderKey { get; set; }

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