using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class AssemblyPlan:Entity
    {
        [Key]
        public int Id { get; set; }
        public string OrderKey { get; set; }
        public int SKUId { get; set; }
        public virtual SKU SKU { get; set; }
        public string DesignName { get; set; }
        public string ComponentSKU { get; set; }
        public string FinishedSKU { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? RequirementDate { get; set; }
       

        public DateTime? ResolveDate1 { get; set; }
        public DateTime? ActualReleaseDate1 { get; set; }

        public DateTime? SetPlanDate2 { get; set; }
        public DateTime? PutawayDate2 { get; set; }

        public DateTime? SetPlanDate3 { get; set; }
        public DateTime? PutawayDate3 { get; set; }

        public DateTime? SetPlanDate4 { get; set; }
        public DateTime? PutawayDate4 { get; set; }

       
        public DateTime? SetPlanDate5 { get; set; }
        public DateTime? DeliveryDate5 { get; set; }
        public DateTime? PutawayDate5 { get; set; }

        public DateTime? SetPlanDate6 { get; set; }
        public DateTime? ActualStartDate6 { get; set; }
        public DateTime? ActualCompletedDate6 { get; set; }


        public string Remark { get; set; }

        public int Status { get; set; }


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