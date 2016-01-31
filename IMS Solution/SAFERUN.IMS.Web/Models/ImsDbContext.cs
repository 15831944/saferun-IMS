using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public class ImsDbContext:DataContext
    {
        public ImsDbContext()
            : base("Name=DefaultConnection")
        { 
        }
        public System.Data.Entity.DbSet< BaseCode> BaseCodes { get; set; }
        public System.Data.Entity.DbSet< CodeItem> CodeItems { get; set; }

        public System.Data.Entity.DbSet< MenuItem> MenuItems { get; set; }

        public DbSet<RoleMenu> RoleMenus { get; set; }

        public DbSet<ProcessNode> ProcessNodes { get; set; }
    }
}