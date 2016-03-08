using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(ProductionScheduleMetadata))]
    public partial class ProductionSchedule
    {
    }

    public partial class ProductionScheduleMetadata
    {
        [Display(Name = "客户")]
        public Customer Customer { get; set; }

        [Display(Name = "生产排程明细")]
        public ScheduleDetail ScheduleDetails { get; set; }

        [Display(Name = "生产任务单号")]
        public Work Work { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "生产排程版本号")]
        [MaxLength(30)]
        public string ScheduleNo { get; set; }

        [Display(Name = "生产任务单号")]
        public int WorkId { get; set; }

        [Display(Name = "订单号")]
        [MaxLength(30)]
        public string OrderKey { get; set; }

        [Display(Name = "订单日期")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "排程开始日期")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "排程结束日期")]
        public DateTime CompletedDate { get; set; }

        [Display(Name = "负责人")]
        [MaxLength(20)]
        public string Ower { get; set; }

        [Display(Name = "排程下单日期")]
        public DateTime ScheduleDate { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100)]
        public string Remark { get; set; }

        [Display(Name = "客户")]
        public int CustomerId { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class ProductionScheduleChangeViewModel
    {
        public IEnumerable<ProductionSchedule> inserted { get; set; }
        public IEnumerable<ProductionSchedule> deleted { get; set; }
        public IEnumerable<ProductionSchedule> updated { get; set; }
    }

}
