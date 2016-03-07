using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(ShiftMetadata))]
    public partial class Shift
    {
    }

    public partial class ShiftMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "版别")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Display(Name = "上班时间")]
        [MaxLength(20)]
        public string OnTime { get; set; }

        [Display(Name = "下班时间")]
        [MaxLength(20)]
        public string OffTime { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100)]
        public string Remark { get; set; }

    }




	public class ShiftChangeViewModel
    {
        public IEnumerable<Shift> inserted { get; set; }
        public IEnumerable<Shift> deleted { get; set; }
        public IEnumerable<Shift> updated { get; set; }
    }

}
