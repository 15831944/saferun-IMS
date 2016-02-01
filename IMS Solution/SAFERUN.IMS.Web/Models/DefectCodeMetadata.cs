using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(DefectCodeMetadata))]
    public partial class DefectCode
    {
    }

    public partial class DefectCodeMetadata
    {
        [Display(Name = "缺陷类型")]
        public DefectType DefectType { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 缺陷名称")]
        [Display(Name = "缺陷名称")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Display(Name = "缺陷代码")]
        [MaxLength(20)]
        public string Code { get; set; }

        [Display(Name = "描述")]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter : 缺陷类型")]
        [Display(Name = "缺陷类型")]
        public int DefectTypeId { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class DefectCodeChangeViewModel
    {
        public IEnumerable<DefectCode> inserted { get; set; }
        public IEnumerable<DefectCode> deleted { get; set; }
        public IEnumerable<DefectCode> updated { get; set; }
    }

}
