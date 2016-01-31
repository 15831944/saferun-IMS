using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(MulticomponentMetadata))]
    public partial class Multicomponent
    {
    }

    public partial class MulticomponentMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 设备编号")]
        [Display(Name = "设备编号")]
        [MaxLength(20)]
        public string DeviceID { get; set; }

        [Required(ErrorMessage = "Please enter : 序号")]
        [Display(Name = "序号")]
        [Range(1, 100)]
        public int Order { get; set; }

        [Required(ErrorMessage = "Please enter : 设备名称")]
        [Display(Name = "设备名称")]
        [MaxLength(50)]
        public string Name { get; set; }

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




	public class MulticomponentChangeViewModel
    {
        public IEnumerable<Multicomponent> inserted { get; set; }
        public IEnumerable<Multicomponent> deleted { get; set; }
        public IEnumerable<Multicomponent> updated { get; set; }
    }

}
