using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(OrderMetadata))]
    public partial class Order
    {
    }

    public partial class OrderMetadata
    {
        [Display(Name = "客户")]
        public Customer Customer { get; set; }

        [Display(Name = "订单明细")]
        public OrderDetail OrderDetails { get; set; }

        [Display(Name = "项目类型")]
        public ProjectType ProjectType { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "订单号")]
        [MaxLength(20)]
        public string OrderKey { get; set; }

        [Display(Name = "销售")]
        [MaxLength(20)]
        public string Sales { get; set; }

        [Required(ErrorMessage = "Please enter : 订单日期")]
        [Display(Name = "订单日期")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Please enter : 评审日期")]
        [Display(Name = "评审日期")]
        public DateTime AuditDate { get; set; }

        [Required(ErrorMessage = "Please enter : 客户")]
        [Display(Name = "客户")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter : 项目类型")]
        [Display(Name = "项目类型")]
        public int ProjectTypeId { get; set; }

        [Required(ErrorMessage = "Please enter : 项目名称")]
        [Display(Name = "项目名称")]
        [MaxLength(50)]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Please enter : 状态")]
        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "评审意见")]
        [MaxLength(50)]
        public string AuditResult { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : 计划完成时间")]
        [Display(Name = "计划完成时间")]
        public DateTime PlanFinishDate { get; set; }

        [Display(Name = "实际完成时间")]
        public DateTime ActualFinishDate { get; set; }

        [Display(Name = "发货日期")]
        public DateTime ShipDate { get; set; }

        [Display(Name = "关闭日期")]
        public DateTime ColseDate { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class OrderChangeViewModel
    {
        public IEnumerable<Order> inserted { get; set; }
        public IEnumerable<Order> deleted { get; set; }
        public IEnumerable<Order> updated { get; set; }
    }

}
