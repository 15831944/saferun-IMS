using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class Order:Entity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [Index("IX_OrderKey",IsUnique=true)]
        public string OrderKey { get; set; }
        public string Sales { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime AuditDate { get; set; }
      

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public int ProjectTypeId { get; set; }
        [ForeignKey("ProjectTypeId")]
        public virtual ProjectType ProjectType { get; set; }
        [Required]
        public string ProjectName { get; set; }

        public int Status { get; set; }
        public string AuditResult { get; set; }

        public string Remark { get; set; }

        

        public DateTime? PlanFinishDate { get; set; }
        public DateTime? ActualFinishDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? ColseDate { get; set; }


        public virtual ICollection<OrderDetail> OrderDetails { get; set; }



        #region add

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
        #endregion
    }
}