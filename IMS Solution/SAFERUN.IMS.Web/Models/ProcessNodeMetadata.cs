using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(ProcessNodeMetadata))]
    public partial class ProcessNode
    {
    }

    public partial class ProcessNodeMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 工序")]
        [Display(Name = "工序")]
        [Range(1, 100)]
        public int Order { get; set; }

        [Required(ErrorMessage = "Please enter : 项目类型")]
        [Display(Name = "项目类型")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter : 机型")]
        [Display(Name = "机型")]
        [MaxLength(20)]
        public string MachineModel { get; set; }

        [Required(ErrorMessage = "Please enter : 节点")]
        [Display(Name = "节点")]
        [MaxLength(50)]
        public string Node { get; set; }

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




	public class ProcessNodeChangeViewModel
    {
        public IEnumerable<ProcessNode> inserted { get; set; }
        public IEnumerable<ProcessNode> deleted { get; set; }
        public IEnumerable<ProcessNode> updated { get; set; }
    }

}
