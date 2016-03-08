using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class ScheduleDetail:Entity
    {
        [Key]
        public int Id { get; set; }
        public string ScheduleNo { get; set; }
        public string WorkNo { get; set; }

        public int ParentSKUId { get; set; }
        [ForeignKey("ParentSKUId")]
        public virtual SKU ParentSKU { get; set; }

     
        public int ComponentSKUId { get; set; }
        [ForeignKey("ComponentSKUId")]
        public virtual SKU ComponentSKU { get; set; }


        public string GraphSKU { get; set; }
        public string GraphVer { get; set; }

        public decimal ConsumeQty { get; set; }

        public decimal StockQty { get; set; }
        public decimal RequirementQty { get; set; }
        public decimal ScheduleProductionQty { get; set; }
        public decimal ActualProductionQty { get; set; }

        public int? StationId { get; set; }
        [ForeignKey("StationId")]
        public virtual Station Station { get; set; }

        public int? ShiftId { get; set; }
        [ForeignKey("ShiftId")]
        public virtual Shift Shift { get; set; }

        public string Operator { get; set; }

        public int Status { get; set; }

        public DateTime? AltProdctionDate1 { get; set; }
        public DateTime? ActualProdctionDate1 { get; set; }
        public DateTime? AltProdctionDate2 { get; set; }
        public DateTime? ActualProdctionDate2 { get; set; }
        public DateTime? AltProdctionDate3 { get; set; }
        public DateTime? ActualProdctionDate3 { get; set; }

        public decimal AltConsumeTime { get; set; }
        public decimal ActualConsumeTime { get; set; }

        public string Remark1 { get; set; }
        public string Remark2 { get; set; }



        public int ProductionScheduleId { get; set; }
        [ForeignKey("ProductionScheduleId")]
        public virtual ProductionSchedule ProductionSchedule { get; set; }

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