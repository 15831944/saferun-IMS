using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class ProductionSchedule:Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index("IX_ProductionSchedule",IsUnique=true)]
        public string ScheduleNo { get; set; }
        public int WorkId { get; set; }
        [ForeignKey("WorkId")]
        public virtual Work Work { get; set; }
        public string OrderKey { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Ower { get; set; }
        public DateTime ScheduleDate { get; set; }

        public string Remark { get; set; }


        [ScaffoldColumn(false)]
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        [ScaffoldColumn(false)]
        public virtual Customer Customer { get; set; }






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