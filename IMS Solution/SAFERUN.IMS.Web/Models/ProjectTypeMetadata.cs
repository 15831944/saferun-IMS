using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(ProjectTypeMetadata))]
    public partial class ProjectType
    {
    }

    public partial class ProjectTypeMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 项目类型")]
        [Display(Name = "项目类型")]
        [MaxLength(50)]
        public string TypeName { get; set; }

        [Required(ErrorMessage = "Please enter : 机型")]
        [Display(Name = "机型")]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required(ErrorMessage = "Please enter : 版本号")]
        [Display(Name = "版本号")]
        [MaxLength(10)]
        public string Version { get; set; }

        [Required(ErrorMessage = "Please enter : 状态")]
        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter : 启用日期")]
        [Display(Name = "启用日期")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please enter : 到期日期")]
        [Display(Name = "到期日期")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class ProjectTypeChangeViewModel
    {
        public IEnumerable<ProjectType> inserted { get; set; }
        public IEnumerable<ProjectType> deleted { get; set; }
        public IEnumerable<ProjectType> updated { get; set; }
    }

}
