﻿using Repository.Pattern.Ef6;
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

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Multicomponent> Multicomponents { get; set; }

        public DbSet<ProcessStep> ProcessSteps { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.Station> Stations { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.DefectType> DefectTypes { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.DefectCode> DefectCodes { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.ProjectType> ProjectTypes { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.ProjectNode> ProjectNodes { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.SKU> SKUs { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.BOMComponent> BOMComponents { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.OrderDetail> OrderDetails { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.OrderAuditPlan> OrderAuditPlans { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.ProductionPlan> ProductionPlans { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.PurchasePlan> PurchasePlans { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.DataTableImportMapping> DataTableImportMappings { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.DefectTracking> DefectTrackings { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.ProductionProcess> ProductionProcesses { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.ProductionTask> ProductionTasks { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.RepairJob> RepairJobs { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.Supplier> Suppliers { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.AssemblyPlan> AssemblyPlans { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.Work> Works { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.WorkType> WorkTypes { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.WorkDetail> WorkDetails { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.Shift> Shifts { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.ScheduleDetail> ScheduleDetails { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.ProductionSchedule> ProductionSchedules { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.WorkProcessDetail> WorkProcessDetails { get; set; }

        public System.Data.Entity.DbSet<SAFERUN.IMS.Web.Models.WorkProcess> WorkProcesses { get; set; }
    }
}