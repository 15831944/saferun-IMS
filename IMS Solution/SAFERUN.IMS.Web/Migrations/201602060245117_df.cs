namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SKUs", "IX_SKU");
            AlterColumn("dbo.BOMComponents", "DesignName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.BOMComponents", "ComponentSKU", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.BOMComponents", "ALTSku", c => c.String(maxLength: 30));
            AlterColumn("dbo.BOMComponents", "GraphSKU", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.BOMComponents", "StockSKU", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.SKUs", "Sku", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.SKUs", "ALTSku", c => c.String(maxLength: 30));
            AlterColumn("dbo.SKUs", "ManufacturerSku", c => c.String(maxLength: 50));
            CreateIndex("dbo.SKUs", "Sku", unique: true, name: "IX_SKU");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SKUs", "IX_SKU");
            AlterColumn("dbo.SKUs", "ManufacturerSku", c => c.String(maxLength: 20));
            AlterColumn("dbo.SKUs", "ALTSku", c => c.String(maxLength: 20));
            AlterColumn("dbo.SKUs", "Sku", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.BOMComponents", "StockSKU", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.BOMComponents", "GraphSKU", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.BOMComponents", "ALTSku", c => c.String(maxLength: 20));
            AlterColumn("dbo.BOMComponents", "ComponentSKU", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.BOMComponents", "DesignName", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.SKUs", "Sku", unique: true, name: "IX_SKU");
        }
    }
}
