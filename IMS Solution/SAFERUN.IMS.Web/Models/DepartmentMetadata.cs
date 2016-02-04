using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department
    {
    }

    public partial class DepartmentMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 部门名称")]
        [Display(Name = "部门名称")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "描述")]
        [MaxLength(50)]
        public string Description { get; set; }

    }




	public class DepartmentChangeViewModel
    {
        public IEnumerable<Department> inserted { get; set; }
        public IEnumerable<Department> deleted { get; set; }
        public IEnumerable<Department> updated { get; set; }
    }

}
