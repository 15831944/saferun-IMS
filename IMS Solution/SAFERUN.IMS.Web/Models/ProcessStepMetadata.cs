using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(ProcessStepMetadata))]
    public partial class ProcessStep
    {
    }

    public partial class ProcessStepMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 工序名称")]
        [Display(Name = "工序名称")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter : 工序")]
        [Display(Name = "工序")]
        [Range(1, 100)]
        public int Order { get; set; }

        [Display(Name = "加工设备")]
        [MaxLength(50)]
        public string Equipment { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100)]
        public string Description { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class ProcessStepChangeViewModel
    {
        public IEnumerable<ProcessStep> inserted { get; set; }
        public IEnumerable<ProcessStep> deleted { get; set; }
        public IEnumerable<ProcessStep> updated { get; set; }
    }

}
