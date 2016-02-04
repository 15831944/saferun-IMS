using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(ProjectNodeMetadata))]
    public partial class ProjectNode
    {
    }

    public partial class ProjectNodeMetadata
    {
        [Display(Name = "参与部门")]
        public Department Department { get; set; }

        [Display(Name = "项目类型")]
        public ProjectType ProjectType { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 节点")]
        [Display(Name = "节点")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter : 序号")]
        [Display(Name = "序号")]
        public int Order { get; set; }

        [Required(ErrorMessage = "Please enter : 用时(天)")]
        [Display(Name = "用时(天)")]
        public int ElapsedDay { get; set; }

        [Required(ErrorMessage = "Please enter : 参与部门")]
        [Display(Name = "参与部门")]
        public int DepartmentId { get; set; }

        [Display(Name = "描述")]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter : 项目类型")]
        [Display(Name = "项目类型")]
        public int ProjectTypeId { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class ProjectNodeChangeViewModel
    {
        public IEnumerable<ProjectNode> inserted { get; set; }
        public IEnumerable<ProjectNode> deleted { get; set; }
        public IEnumerable<ProjectNode> updated { get; set; }
    }

}
