using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(WorkMetadata))]
    public partial class Work
    {
    }

    public partial class WorkMetadata
    {
        [Display(Name = "订单")]
        public Order Order { get; set; }

        [Display(Name = "生产任务单明细")]
        public WorkDetail WorkDetails { get; set; }

        [Display(Name = "任务单类型")]
        public WorkType WorkType { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "生产任务单号")]
        [MaxLength(30)]
        public string WorkNo { get; set; }

        [Display(Name = "任务单类型")]
        public int WorkTypeId { get; set; }

        [Display(Name = "订单号")]
        [MaxLength(30)]
        public string OrderKey { get; set; }

        [Display(Name = "订单号")]
        public int OrderId { get; set; }

        [Display(Name = "采购单号")]
        [MaxLength(30)]
        public string PO { get; set; }

        [Display(Name = "创建人")]
        [MaxLength(20)]
        public string User { get; set; }

        [Display(Name = "下单日期")]
        public DateTime WorkDate { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "事业部审核")]
        [MaxLength(50)]
        public string Review { get; set; }

        [Display(Name = "生产签字确认")]
        [MaxLength(50)]
        public string ProductionConfirm { get; set; }

        [Display(Name = "外协签字确认")]
        [MaxLength(50)]
        public string OutsourceConfirm { get; set; }

        [Display(Name = "组装签字确认")]
        [MaxLength(50)]
        public string AssembleConfirm { get; set; }

        [Display(Name = "采购签字确认")]
        [MaxLength(50)]
        public string PurchaseConfirm { get; set; }

        [Display(Name = "审核日期")]
        public DateTime ReviewDate { get; set; }

        [Display(Name = "生产确认日期")]
        public DateTime ProductionConfirmDate { get; set; }

        [Display(Name = "外协确认日期")]
        public DateTime OutsourceConfirmDate { get; set; }

        [Display(Name = "组装确认日期")]
        public DateTime AssembleConfirmDate { get; set; }

        [Display(Name = "采购确认日期")]
        public DateTime PurchaseConfirmDate { get; set; }

        [Display(Name = "备注")]
        [MaxLength(200)]
        public string Remark { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class WorkChangeViewModel
    {
        public IEnumerable<Work> inserted { get; set; }
        public IEnumerable<Work> deleted { get; set; }
        public IEnumerable<Work> updated { get; set; }
    }

}
