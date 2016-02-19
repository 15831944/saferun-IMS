using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(ProductionProcessMetadata))]
    public partial class ProductionProcess
    {
    }

    public partial class ProductionProcessMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 加工工序")]
        [Display(Name = "加工工序")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Display(Name = "描述")]
        [MaxLength(50)]
        public string Description { get; set; }

        [Display(Name = "标准耗时(H)")]
        public decimal ElapsedTime { get; set; }

        [Display(Name = "生产线")]
        [MaxLength(50)]
        public string ProductionLine { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class ProductionProcessChangeViewModel
    {
        public IEnumerable<ProductionProcess> inserted { get; set; }
        public IEnumerable<ProductionProcess> deleted { get; set; }
        public IEnumerable<ProductionProcess> updated { get; set; }
    }

}
