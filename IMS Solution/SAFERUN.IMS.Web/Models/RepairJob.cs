using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class RepairJob:Entity
    {
        [Key]
        public int Id { get; set; }
        public string OrderKey { get; set; }
       public string ProjectName { get; set; }

       public int SKUId { get; set; }
       public virtual SKU SKU { get; set; }
       public string GraphSKU { get; set; }
       public string DesignName { get; set; }
       public decimal RepairQty { get; set; }
       public string Issue { get; set; }
       public string ProcessName { get; set; }
       public string ResponsibleDepartment { get; set; }
       public string Owner { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public decimal ElapsedTime { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }




        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ScaffoldColumn(false)]
        public int BomComponentId { get; set; }
        [ScaffoldColumn(false)]
        public int? ParentBomComponentId { get; set; }

    }
}