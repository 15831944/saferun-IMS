using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class Work:Entity
    {
        public Work()
        {
            WorkDetails = new HashSet<WorkDetail>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Index("IX_Work",IsUnique=true)]
        public string WorkNo { get; set; }

        public int WorkTypeId { get; set; }
        [ForeignKey("WorkTypeId")]
        public virtual WorkType WorkType { get; set; }
        public string OrderKey { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public string PO { get; set; }
        public string User { get; set; }
        public DateTime WorkDate { get; set; }
        public int Status { get; set; }
        public string Review { get; set; }
        public string ProductionConfirm { get; set; }
        public string OutsourceConfirm { get; set; }
        public string AssembleConfirm { get; set; }
        public string PurchaseConfirm { get; set; }

        public DateTime? ReviewDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? ProductionConfirmDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? OutsourceConfirmDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? AssembleConfirmDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? PurchaseConfirmDate { get; set; }
        public string Remark { get; set; }



        public virtual ICollection<WorkDetail> WorkDetails { get; set; }

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