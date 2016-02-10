using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(OrderAuditPlanMetadata))]
    public partial class OrderAuditPlan
    {
    }

    public partial class OrderAuditPlanMetadata
    {
        [Display(Name = "订单")]
        public Order Order { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "订单号")]
        [MaxLength(20)]
        public string OrderKey { get; set; }

        [Display(Name = "评审内容")]
        [MaxLength(50)]
        public string AuditContent { get; set; }

        [Display(Name = "评审部门")]
        [MaxLength(20)]
        public string Department { get; set; }

        [Display(Name = "评审结果")]
        [MaxLength(50)]
        public string AuditResult { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "计划评审日期")]
        public DateTime PlanAuditDate { get; set; }

        [Display(Name = "实际评审日期")]
        public DateTime AuditDate { get; set; }

        [Display(Name = "评审人")]
        [MaxLength(20)]
        public string AuditUser { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : 订单")]
        [Display(Name = "订单")]
        public int OrderId { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class OrderAuditPlanChangeViewModel
    {
        public IEnumerable<OrderAuditPlan> inserted { get; set; }
        public IEnumerable<OrderAuditPlan> deleted { get; set; }
        public IEnumerable<OrderAuditPlan> updated { get; set; }
    }

}
