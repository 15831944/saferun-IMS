using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(OrderDetailMetadata))]
    public partial class OrderDetail
    {
    }

    public partial class OrderDetailMetadata
    {
        [Display(Name = "订单")]
        public Order Order { get; set; }

        [Display(Name = "产品")]
        public SKU SKU { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "订单号")]
        public string OrderKey { get; set; }

        [Display(Name = "订单")]
        public int OrderId { get; set; }

        [Display(Name = "行号")]
        [MaxLength(10)]
        public string LineNumber { get; set; }

        [Display(Name = "合同编号")]
        [MaxLength(30)]
        public string ContractNum { get; set; }

        [Required(ErrorMessage = "Please enter : 产品")]
        [Display(Name = "产品")]
        public int SKUId { get; set; }

        [Display(Name = "产品料号")]
        [MaxLength(30)]
        public string ProductionSku { get; set; }

        [Display(Name = "型号")]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required(ErrorMessage = "Please enter : 数量")]
        [Display(Name = "数量")]
        [Range(1, 999)]
        public int Qty { get; set; }

        [Display(Name = "单位")]
        [MaxLength(10)]
        public string UOM { get; set; }

        [Display(Name = "单价")]
        public decimal Price { get; set; }

        [Display(Name = "合计")]
        public decimal SubTotal { get; set; }

        [Display(Name = "备注")]
        [MaxLength(100)]
        public string Remark { get; set; }

        [Required(ErrorMessage = "Please enter : 状态")]
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




	public class OrderDetailChangeViewModel
    {
        public IEnumerable<OrderDetail> inserted { get; set; }
        public IEnumerable<OrderDetail> deleted { get; set; }
        public IEnumerable<OrderDetail> updated { get; set; }
    }

}
