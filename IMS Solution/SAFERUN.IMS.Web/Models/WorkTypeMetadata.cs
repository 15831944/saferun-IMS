using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(WorkTypeMetadata))]
    public partial class WorkType
    {
    }

    public partial class WorkTypeMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 任务单类型")]
        [Display(Name = "任务单类型")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "描述")]
        [MaxLength(50)]
        public string Description { get; set; }

    }




	public class WorkTypeChangeViewModel
    {
        public IEnumerable<WorkType> inserted { get; set; }
        public IEnumerable<WorkType> deleted { get; set; }
        public IEnumerable<WorkType> updated { get; set; }
    }

}
