using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(WorkProcessDetailMetadata))]
    public partial class WorkProcessDetail
    {
    }

    public partial class WorkProcessDetailMetadata
    {
        [Display(Name = "工序")]
        public ProcessStep ProcessStep { get; set; }

        [Display(Name = "加工机台")]
        public Station Station { get; set; }

        [Display(Name = "零件加工工序卡")]
        public WorkProcess WorkProcess { get; set; }

         [Display(Name = "零件编号")]
        public int SKUId { get; set; }
     [Display(Name = "零件编号")]
        public virtual SKU SKU { get; set; }
        [Display(Name = "零件编号")]
        public string ComponentSKU { get; set; }
        [Display(Name = "零件图号")]
        public string GraphSKU { get; set; }


        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "零件加工工序卡")]
        public int WorkProcessId { get; set; }

        [Display(Name = "工序")]
        public int ProcessStepId { get; set; }


        [Display(Name = "工序步骤")]
        [MaxLength(50)]
        public string StepName { get; set; }

        [Display(Name = "加工机台")]
        public int StationId { get; set; }

        [Display(Name = "标准用时")]
        public decimal StandardElapsedTime { get; set; }

        [Display(Name = "开始时间")]
        public DateTime StartingDateTime { get; set; }

        [Display(Name = "完成时间")]
        public DateTime CompletedDateTime { get; set; }

        [Display(Name = "实际工时")]
        public decimal ElapsedTime { get; set; }

        [Display(Name = "个人实际工时")]
        public decimal WorkingTime { get; set; }

        [Display(Name = "操作员")]
        [MaxLength(30)]
        public string Operator { get; set; }

        [Display(Name = "检验确认")]
        [MaxLength(30)]
        public string QCConfirm { get; set; }

        [Display(Name = "检验确认时间")]
        public DateTime QCConfirmDateTime { get; set; }

        [Display(Name = "转序确认")]
        [MaxLength(30)]
        public string CompletedConfirm { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100)]
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




	public class WorkProcessDetailChangeViewModel
    {
        public IEnumerable<WorkProcessDetail> inserted { get; set; }
        public IEnumerable<WorkProcessDetail> deleted { get; set; }
        public IEnumerable<WorkProcessDetail> updated { get; set; }
    }

}
