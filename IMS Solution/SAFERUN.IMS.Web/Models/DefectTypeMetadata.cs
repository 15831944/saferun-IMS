using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(DefectTypeMetadata))]
    public partial class DefectType
    {
    }

    public partial class DefectTypeMetadata
    {
        [Display(Name = "缺陷子代码")]
        public DefectCode DefectCodes { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 缺陷类型")]
        [Display(Name = "缺陷类型")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Display(Name = "缺陷代码")]
        [MaxLength(20)]
        public string Code { get; set; }

        [Display(Name = "描述")]
        [MaxLength(50)]
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




	public class DefectTypeChangeViewModel
    {
        public IEnumerable<DefectType> inserted { get; set; }
        public IEnumerable<DefectType> deleted { get; set; }
        public IEnumerable<DefectType> updated { get; set; }
    }

}
