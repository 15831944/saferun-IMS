namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderAuditPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderKey = c.String(nullable: false, maxLength: 20),
                        AuditContent = c.String(nullable: false, maxLength: 50),
                        Department = c.String(maxLength: 20),
                        AuditResult = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                        PlanAuditDate = c.DateTime(nullable: false),
                        AuditDate = c.DateTime(),
                        AuditUser = c.String(maxLength: 20),
                        Remark = c.String(maxLength: 100),
                        OrderId = c.Int(nullable: false),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderKey = c.String(nullable: false, maxLength: 20),
                        Sales = c.String(maxLength: 20),
                        OrderDate = c.DateTime(nullable: false),
                        AuditDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ProjectTypeId = c.Int(nullable: false),
                        ProjectName = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                        AuditResult = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 100),
                        PlanFinishDate = c.DateTime(nullable: false),
                        ActualFinishDate = c.DateTime(),
                        ShipDate = c.DateTime(),
                        ColseDate = c.DateTime(),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTypes", t => t.ProjectTypeId, cascadeDelete: true)
                .Index(t => t.OrderKey, unique: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProjectTypeId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderKey = c.String(nullable: false),
                        OrderId = c.Int(nullable: false),
                        LineNumber = c.String(nullable: false, maxLength: 10),
                        ContractNum = c.String(maxLength: 30),
                        SKUId = c.Int(nullable: false),
                        ProductionSku = c.String(maxLength: 30),
                        Model = c.String(maxLength: 50),
                        Qty = c.Int(nullable: false),
                        UOM = c.String(maxLength: 10),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(maxLength: 100),
                        Status = c.Int(nullable: false),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.SKUs", t => t.SKUId, cascadeDelete: true)
                .Index(t => new { t.OrderId, t.LineNumber }, unique: true, name: "IX_OrderDetail")
                .Index(t => t.SKUId);
            
            CreateTable(
                "dbo.ProductionPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderKey = c.String(maxLength: 20),
                        SKUId = c.Int(nullable: false),
                        DesignName = c.String(maxLength: 30),
                        ComponentSKU = c.String(maxLength: 30),
                        ALTSku = c.String(maxLength: 30),
                        GraphSKU = c.String(maxLength: 30),
                        StockSKU = c.String(maxLength: 30),
                        Status = c.Int(nullable: false),
                        ConsumeQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RequirementQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RejectRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deploy = c.String(maxLength: 50),
                        Locator = c.String(maxLength: 50),
                        ProductionLine = c.String(maxLength: 50),
                        OrderPlanDate = c.DateTime(nullable: false),
                        ActualFinishDate = c.DateTime(),
                        Remark = c.String(maxLength: 100),
                        OrderId = c.Int(nullable: false),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.SKUs", t => t.SKUId, cascadeDelete: true)
                .Index(t => t.SKUId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.PurchasePlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderKey = c.String(maxLength: 20),
                        SKUId = c.Int(nullable: false),
                        DesignName = c.String(maxLength: 30),
                        ComponentSKU = c.String(maxLength: 30),
                        ALTSku = c.String(maxLength: 30),
                        GraphSKU = c.String(maxLength: 30),
                        StockSKU = c.String(maxLength: 30),
                        Status = c.Int(nullable: false),
                        ConsumeQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RequirementQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RejectRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deploy = c.String(maxLength: 50),
                        Locator = c.String(maxLength: 30),
                        Brand = c.String(maxLength: 20),
                        Supplier = c.String(maxLength: 20),
                        OrderPlanDate = c.DateTime(nullable: false),
                        PlanDeliveryDate = c.DateTime(nullable: false),
                        ActualDeliveryDate = c.DateTime(),
                        Remark = c.String(maxLength: 100),
                        OrderId = c.Int(nullable: false),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.SKUs", t => t.SKUId, cascadeDelete: true)
                .Index(t => t.SKUId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchasePlans", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.PurchasePlans", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.ProductionPlans", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.ProductionPlans", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderAuditPlans", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ProjectTypeId", "dbo.ProjectTypes");
            DropForeignKey("dbo.OrderDetails", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.PurchasePlans", new[] { "OrderId" });
            DropIndex("dbo.PurchasePlans", new[] { "SKUId" });
            DropIndex("dbo.ProductionPlans", new[] { "OrderId" });
            DropIndex("dbo.ProductionPlans", new[] { "SKUId" });
            DropIndex("dbo.OrderDetails", new[] { "SKUId" });
            DropIndex("dbo.OrderDetails", "IX_OrderDetail");
            DropIndex("dbo.Orders", new[] { "ProjectTypeId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Orders", new[] { "OrderKey" });
            DropIndex("dbo.OrderAuditPlans", new[] { "OrderId" });
            DropTable("dbo.PurchasePlans");
            DropTable("dbo.ProductionPlans");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderAuditPlans");
        }
    }
}
