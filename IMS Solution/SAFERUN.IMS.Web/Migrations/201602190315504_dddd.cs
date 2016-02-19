namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dddd : DbMigration
    {
        public override void Up()
        {
            
            
            CreateTable(
                "dbo.ProcessSteps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StepName = c.String(nullable: false, maxLength: 30),
                        Order = c.Int(nullable: false),
                        ElapsedTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Equipment = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                        Description = c.String(maxLength: 100),
                        ProductionProcessId = c.Int(nullable: false),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductionProcesses", t => t.ProductionProcessId, cascadeDelete: true)
                .Index(t => new { t.ProductionProcessId, t.StepName }, unique: true, name: "IX_ProcessStep");
            
            CreateTable(
                "dbo.ProductionProcesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(maxLength: 50),
                        ElapsedTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductionLine = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_ProductionProcess");
            
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleMenus", "MenuId", "dbo.MenuItems");
            DropForeignKey("dbo.PurchasePlans", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.PurchasePlans", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.ProjectNodes", "ProjectTypeId", "dbo.ProjectTypes");
            DropForeignKey("dbo.ProjectNodes", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ProductionPlans", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.ProductionPlans", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.ProcessSteps", "ProductionProcessId", "dbo.ProductionProcesses");
            DropForeignKey("dbo.OrderAuditPlans", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Messages", "MessageId", "dbo.Messages");
            DropForeignKey("dbo.MenuItems", "ParentId", "dbo.MenuItems");
            DropForeignKey("dbo.Employees", "ManagerID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.DefectTrackings", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.DefectTrackings", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ProjectTypeId", "dbo.ProjectTypes");
            DropForeignKey("dbo.OrderDetails", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.DefectTrackings", "DefectTypeId", "dbo.DefectTypes");
            DropForeignKey("dbo.DefectTrackings", "DefectCodeId", "dbo.DefectCodes");
            DropForeignKey("dbo.DefectCodes", "DefectTypeId", "dbo.DefectTypes");
            DropForeignKey("dbo.BOMComponents", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.BOMComponents", "ParentComponentId", "dbo.BOMComponents");
            DropForeignKey("dbo.CodeItems", "BaseCodeId", "dbo.BaseCodes");
            DropIndex("dbo.Stations", "IX_Station");
            DropIndex("dbo.RoleMenus", "IX_rolemenu");
            DropIndex("dbo.PurchasePlans", new[] { "OrderId" });
            DropIndex("dbo.PurchasePlans", new[] { "SKUId" });
            DropIndex("dbo.ProjectNodes", new[] { "ProjectTypeId" });
            DropIndex("dbo.ProjectNodes", new[] { "DepartmentId" });
            DropIndex("dbo.ProductionPlans", new[] { "OrderId" });
            DropIndex("dbo.ProductionPlans", new[] { "SKUId" });
            DropIndex("dbo.ProductionProcesses", "IX_ProductionProcess");
            DropIndex("dbo.ProcessSteps", "IX_ProcessStep");
            DropIndex("dbo.OrderAuditPlans", new[] { "OrderId" });
            DropIndex("dbo.Multicomponents", "IX_Multicomponent");
            DropIndex("dbo.Messages", new[] { "MessageId" });
            DropIndex("dbo.MenuItems", new[] { "ParentId" });
            DropIndex("dbo.MenuItems", "IX_menuUrl");
            DropIndex("dbo.MenuItems", "IX_menuCode");
            DropIndex("dbo.MenuItems", "IX_menuTitle");
            DropIndex("dbo.Employees", new[] { "ManagerID" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", "IX_Employee");
            DropIndex("dbo.Departments", "IX_Department");
            DropIndex("dbo.OrderDetails", new[] { "SKUId" });
            DropIndex("dbo.OrderDetails", "IX_OrderDetail");
            DropIndex("dbo.Orders", new[] { "ProjectTypeId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Orders", new[] { "OrderKey" });
            DropIndex("dbo.DefectTrackings", new[] { "DefectCodeId" });
            DropIndex("dbo.DefectTrackings", new[] { "DefectTypeId" });
            DropIndex("dbo.DefectTrackings", new[] { "SKUId" });
            DropIndex("dbo.DefectTrackings", new[] { "OrderId" });
            DropIndex("dbo.DefectTypes", "IX_DefectType");
            DropIndex("dbo.DefectCodes", new[] { "DefectTypeId" });
            DropIndex("dbo.DefectCodes", "IX_DefectCode");
            DropIndex("dbo.DataTableImportMappings", "IX_DataTableImportMapping");
            DropIndex("dbo.Customers", "IX_customer");
            DropIndex("dbo.SKUs", "IX_SKU");
            DropIndex("dbo.BOMComponents", new[] { "ParentComponentId" });
            DropIndex("dbo.BOMComponents", new[] { "SKUId" });
            DropIndex("dbo.CodeItems", new[] { "BaseCodeId" });
            DropIndex("dbo.CodeItems", new[] { "Code" });
            DropIndex("dbo.BaseCodes", new[] { "CodeType" });
            DropTable("dbo.Stations");
            DropTable("dbo.RoleMenus");
            DropTable("dbo.PurchasePlans");
            DropTable("dbo.ProjectNodes");
            DropTable("dbo.ProductionPlans");
            DropTable("dbo.ProductionProcesses");
            DropTable("dbo.ProcessSteps");
            DropTable("dbo.ProcessNodes");
            DropTable("dbo.OrderAuditPlans");
            DropTable("dbo.Multicomponents");
            DropTable("dbo.Messages");
            DropTable("dbo.MenuItems");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
            DropTable("dbo.ProjectTypes");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.DefectTrackings");
            DropTable("dbo.DefectTypes");
            DropTable("dbo.DefectCodes");
            DropTable("dbo.DataTableImportMappings");
            DropTable("dbo.Customers");
            DropTable("dbo.SKUs");
            DropTable("dbo.BOMComponents");
            DropTable("dbo.CodeItems");
            DropTable("dbo.BaseCodes");
        }
    }
}
