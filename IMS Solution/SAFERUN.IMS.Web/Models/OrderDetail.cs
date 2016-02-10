using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class OrderDetail:Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string OrderKey { get; set; }
        [Index("IX_OrderDetail",1, IsUnique = true )]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [Required]
        [Index("IX_OrderDetail",2, IsUnique = true )]
        public string LineNumber { get; set; }
        public string ContractNum { get; set; }

        public int SKUId { get; set; }
        [ForeignKey("SKUId")]
        public virtual SKU SKU { get; set; }
        public string ProductionSku { get; set; }
        public string Model { get; set; }

        public int Qty { get; set; }
        public string UOM { get; set; }

        public decimal Price { get; set; }

        public decimal SubTotal { get; set; }

        public string Remark { get; set; }

        public int Status { get; set; }



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