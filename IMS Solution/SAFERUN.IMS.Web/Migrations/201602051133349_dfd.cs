namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BOMComponents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DesignName = c.String(nullable: false, maxLength: 20),
                        ComponentSKU = c.String(nullable: false, maxLength: 20),
                        ALTSku = c.String(maxLength: 20),
                        GraphSKU = c.String(nullable: false, maxLength: 20),
                        StockSKU = c.String(nullable: false, maxLength: 20),
                        Remark1 = c.String(),
                        Remark2 = c.String(),
                        ConsumeQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumeTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RejectRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deploy = c.String(),
                        Locator = c.String(),
                        ProductionLine = c.String(),
                        Status = c.Int(nullable: false),
                        NoPur = c.Boolean(nullable: false),
                        DesignPricture1 = c.Binary(),
                        DesignPrictureURL1 = c.String(),
                        DesignPricture2 = c.Binary(),
                        DesignPrictureURL2 = c.String(),
                        SKUId = c.Int(nullable: false),
                        ParentComponentId = c.Int(),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BOMComponents", t => t.ParentComponentId)
                .ForeignKey("dbo.SKUs", t => t.SKUId, cascadeDelete: true)
                .Index(t => t.SKUId)
                .Index(t => t.ParentComponentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BOMComponents", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.BOMComponents", "ParentComponentId", "dbo.BOMComponents");
            DropIndex("dbo.BOMComponents", new[] { "ParentComponentId" });
            DropIndex("dbo.BOMComponents", new[] { "SKUId" });
            DropTable("dbo.BOMComponents");
        }
    }
}
