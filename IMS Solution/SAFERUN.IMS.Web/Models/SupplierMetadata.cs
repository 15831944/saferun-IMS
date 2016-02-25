using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(SupplierMetadata))]
    public partial class Supplier
    {
    }

    public partial class SupplierMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "供应商代码")]
        [MaxLength(20)]
        public string AccountNumber { get; set; }

        [Display(Name = "简称")]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [Display(Name = "名称")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "供应商类型")]
        [MaxLength(50)]
        public string CustomerType { get; set; }

        [Display(Name = "地址1")]
        [MaxLength(50)]
        public string AddressLine1 { get; set; }

        [Display(Name = "地址2")]
        [MaxLength(50)]
        public string AddressLine2 { get; set; }

        [Display(Name = "城市")]
        [MaxLength(50)]
        public string City { get; set; }

        [Display(Name = "省")]
        [MaxLength(50)]
        public string Province { get; set; }

        [Display(Name = "联系人")]
        [MaxLength(50)]
        public string ContactName { get; set; }

        [Display(Name = "联系电话1")]
        [MaxLength(50)]
        public string Phone1 { get; set; }

        [Display(Name = "联系电话2")]
        [MaxLength(50)]
        public string Phone2 { get; set; }

        [Display(Name = "Email")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class SupplierChangeViewModel
    {
        public IEnumerable<Supplier> inserted { get; set; }
        public IEnumerable<Supplier> deleted { get; set; }
        public IEnumerable<Supplier> updated { get; set; }
    }

}
