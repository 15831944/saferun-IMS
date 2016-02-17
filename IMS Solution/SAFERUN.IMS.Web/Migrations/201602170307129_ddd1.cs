namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddd1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefectTrackings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false, maxLength: 50),
                        OrderKey = c.String(maxLength: 30),
                        OrderId = c.Int(nullable: false),
                        SKUId = c.Int(nullable: false),
                        ComponentSKU = c.String(nullable: false, maxLength: 30),
                        Supplier = c.String(maxLength: 50),
                        GraphSKU = c.String(maxLength: 30),
                        QCQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CheckedQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NGQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DefectTypeId = c.Int(nullable: false),
                        DefectCodeId = c.Int(nullable: false),
                        Locator = c.String(maxLength: 50),
                        DefectDesc = c.String(maxLength: 100),
                        Status = c.Int(nullable: false),
                        Result = c.String(maxLength: 100),
                        Remark = c.String(maxLength: 100),
                        QCUser = c.String(maxLength: 20),
                        TrackingDateTime = c.DateTime(nullable: false),
                        CheckedDateTime = c.DateTime(),
                        CloseDateTime = c.DateTime(),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DefectCodes", t => t.DefectCodeId, cascadeDelete: false)
                .ForeignKey("dbo.DefectTypes", t => t.DefectTypeId, cascadeDelete: false)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: false)
                .ForeignKey("dbo.SKUs", t => t.SKUId, cascadeDelete: false)
                .Index(t => t.OrderId)
                .Index(t => t.SKUId)
                .Index(t => t.DefectTypeId)
                .Index(t => t.DefectCodeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DefectTrackings", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.DefectTrackings", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.DefectTrackings", "DefectTypeId", "dbo.DefectTypes");
            DropForeignKey("dbo.DefectTrackings", "DefectCodeId", "dbo.DefectCodes");
            DropIndex("dbo.DefectTrackings", new[] { "DefectCodeId" });
            DropIndex("dbo.DefectTrackings", new[] { "DefectTypeId" });
            DropIndex("dbo.DefectTrackings", new[] { "SKUId" });
            DropIndex("dbo.DefectTrackings", new[] { "OrderId" });
            DropTable("dbo.DefectTrackings");
        }
    }
}
