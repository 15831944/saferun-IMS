﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

 
namespace SAFERUN.IMS.Web.Models
{
    public class RoleViewModel
    {
        [Required]
        [DisplayName("角色")]
        public string Name { get; set; }
    }
    public class ManagementViewModel
    {
        public ICollection<ApplicationRole> Roles { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
    public class AccountRecordViewModel
    {
        public ApplicationUser User { get; set; }
        public ICollection<ApplicationRole> Roles { get; set; }
    }
    public class AttachRoleViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
    public class AccountViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "密码长度(6-100)。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }


    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserChangeViewModel
    {
        public IEnumerable<UserViewModel> inserted { get; set; }
        public IEnumerable<UserViewModel> deleted { get; set; }
        public IEnumerable<UserViewModel> updated { get; set; }
    }
}