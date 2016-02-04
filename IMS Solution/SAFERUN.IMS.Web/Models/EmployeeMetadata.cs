using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
    }

    public partial class EmployeeMetadata
    {
        [Display(Name = "主管")]
        public Employee Manager { get; set; }
        [Display(Name = "部门")]
        public Department Department { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 姓名")]
        [Display(Name = "姓名")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "所在部门")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please enter : 工号")]
        [Display(Name = "工号")]
        [MaxLength(10)]
        public string WorkNo { get; set; }

        [Display(Name = "职位")]
        [MaxLength(10)]
        public string Title { get; set; }

        [Display(Name = "出生日期")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "婚姻")]
        [MaxLength(10)]
        public string MaritalStatus { get; set; }

        [Display(Name = "性别")]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Display(Name = "入职日期")]
        public DateTime HireDate { get; set; }

        [Display(Name = "主管")]
        public int ManagerID { get; set; }

    }




	public class EmployeeChangeViewModel
    {
        public IEnumerable<Employee> inserted { get; set; }
        public IEnumerable<Employee> deleted { get; set; }
        public IEnumerable<Employee> updated { get; set; }
    }

}
