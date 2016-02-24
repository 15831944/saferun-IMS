namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class task : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductionTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderKey = c.String(nullable: false, maxLength: 20),
                        SKUId = c.Int(nullable: false),
                        DesignName = c.String(maxLength: 30),
                        ComponentSKU = c.String(maxLength: 30),
                        ALTSku = c.String(maxLength: 30),
                        GraphSKU = c.String(maxLength: 30),
                        ProcessName = c.String(maxLength: 50),
                        ProcessOrder = c.Int(nullable: false),
                        ProcessSetp = c.String(maxLength: 50),
                        AltElapsedTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductionLine = c.String(maxLength: 50),
                        Equipment = c.String(maxLength: 50),
                        OrderPlanDate = c.DateTime(nullable: false),
                        Owner = c.String(maxLength: 30),
                        PlanStartDateTime = c.DateTime(),
                        PlanCompletedDateTime = c.DateTime(),
                        ActualStartDateTime = c.DateTime(),
                        ActualCompletedDateTime = c.DateTime(),
                        ActualElapsedTime = c.Decimal(precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        Issue = c.String(maxLength: 200),
                        Remark = c.String(maxLength: 200),
                        OrderId = c.Int(nullable: false),
                        BomComponentId = c.Int(nullable: false),
                        ParentBomComponentId = c.Int(),
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
            DropForeignKey("dbo.ProductionTasks", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.ProductionTasks", "OrderId", "dbo.Orders");
            DropIndex("dbo.ProductionTasks", new[] { "OrderId" });
            DropIndex("dbo.ProductionTasks", new[] { "SKUId" });
            DropTable("dbo.ProductionTasks");
        }
    }
}
