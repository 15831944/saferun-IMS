namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class job : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RepairJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderKey = c.String(maxLength: 30),
                        ProjectName = c.String(maxLength: 50),
                        SKUId = c.Int(nullable: false),
                        GraphSKU = c.String(maxLength: 30),
                        DesignName = c.String(maxLength: 50),
                        RepairQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Issue = c.String(maxLength: 200),
                        ProcessName = c.String(maxLength: 50),
                        ResponsibleDepartment = c.String(maxLength: 20),
                        Owner = c.String(maxLength: 20),
                        StartDateTime = c.DateTime(),
                        CompletedDateTime = c.DateTime(),
                        ElapsedTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        Remark = c.String(maxLength: 200),
                        OrderId = c.Int(nullable: false),
                        BomComponentId = c.Int(nullable: false),
                        ParentBomComponentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.SKUs", t => t.SKUId, cascadeDelete: true)
                .Index(t => t.SKUId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RepairJobs", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.RepairJobs", "OrderId", "dbo.Orders");
            DropIndex("dbo.RepairJobs", new[] { "OrderId" });
            DropIndex("dbo.RepairJobs", new[] { "SKUId" });
            DropTable("dbo.RepairJobs");
        }
    }
}
