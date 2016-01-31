using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(CustomerMetadata))]
    public partial class Customer
    {
    }

    public partial class CustomerMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 客户代码")]
        [Display(Name = "客户代码")]
        [MaxLength(20)]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Please enter : 简称")]
        [Display(Name = "简称")]
        [MaxLength(20)]
        public string ShortName { get; set; }

        [Display(Name = "全称")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "客户类型")]
        [MaxLength(20)]
        public string CustomerType { get; set; }

        [Display(Name = "客户地址1")]
        [MaxLength(110)]
        public string AddressLine1 { get; set; }

        [Display(Name = "客户地址2")]
        [MaxLength(110)]
        public string AddressLine2 { get; set; }

        [Display(Name = "城市")]
        [MaxLength(20)]
        public string City { get; set; }

        [Display(Name = "省份")]
        [MaxLength(20)]
        public string Province { get; set; }

        [Display(Name = "联系人")]
        [MaxLength(20)]
        public string ContactName { get; set; }

        [Display(Name = "电话1")]
        [MaxLength(20)]
        public string Phone1 { get; set; }

        [Display(Name = "电话2")]
        [MaxLength(20)]
        public string Phone2 { get; set; }

        [Display(Name = "Email")]
        [MaxLength(20)]
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




	public class CustomerChangeViewModel
    {
        public IEnumerable<Customer> inserted { get; set; }
        public IEnumerable<Customer> deleted { get; set; }
        public IEnumerable<Customer> updated { get; set; }
    }

}
