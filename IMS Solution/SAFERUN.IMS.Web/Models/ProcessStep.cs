using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{

    public partial class ProductionProcess : Entity
    {
        public ProductionProcess()
        {
            ProcessSteps = new HashSet<ProcessStep>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Index("IX_ProductionProcess",IsUnique=true)]
        public string Name { get; set; }
        public string Description { get; set; }
        public  decimal ElapsedTime { get; set; }
        public string ProductionLine { get; set; }

        public int Status { get; set; }


        public virtual ICollection<ProcessStep> ProcessSteps { get; set; }


        [ScaffoldColumn(false)]
        [Display(Name = "新增用户", Description = "新增用户")]
        [StringLength(20)]
        public string CreatedUserId { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "新增时间", Description = "新增时间")]
        public DateTime? CreatedDateTime { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "最后修改用户", Description = "最后修改用户")]
        [StringLength(20)]
        public string LastEditUserId { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "最后修改时间", Description = "最后修改时间")]
        public DateTime? LastEditDateTime { get; set; }

    }

    public partial class ProcessStep:Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Index("IX_ProcessStep", IsUnique = true, Order = 2)]
        public string StepName { get; set; }

    
        public int Order { get; set; }
        
        public decimal ElapsedTime { get; set; }
        public string Equipment { get; set; }

        public int Status { get; set; }
        public string Description { get; set; }

        [Index("IX_ProcessStep", IsUnique = true, Order = 1)]
        [Display(Name="工序")]
        public int ProductionProcessId { get; set; }
        [ForeignKey("ProductionProcessId")]
        [Display(Name = "工序")]
        public virtual ProductionProcess ProductionProcess { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "新增用户", Description = "新增用户")]
        [StringLength(20)]
        public string CreatedUserId { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "新增时间", Description = "新增时间")]
        public DateTime? CreatedDateTime { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "最后修改用户", Description = "最后修改用户")]
        [StringLength(20)]
        public string LastEditUserId { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "最后修改时间", Description = "最后修改时间")]
        public DateTime? LastEditDateTime { get; set; }

    }
}