namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sku : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SKUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sku = c.String(nullable: false, maxLength: 20),
                        ALTSku = c.String(maxLength: 20),
                        ManufacturerSku = c.String(maxLength: 20),
                        Model = c.String(maxLength: 20),
                        CLASS = c.String(maxLength: 20),
                        SKUGroup = c.String(maxLength: 20),
                        MadeType = c.String(nullable: false, maxLength: 20),
                        STDUOM = c.String(maxLength: 10),
                        Description = c.String(maxLength: 100),
                        Brand = c.String(maxLength: 30),
                        PackKey = c.String(maxLength: 20),
                        QCLOC = c.Boolean(nullable: false),
                        QCLOCOUT = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        STDGrossWGT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        STDNetWGT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        STDCube = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SUSR1 = c.Int(nullable: false),
                        SUSR2 = c.String(maxLength: 50),
                        SUSR3 = c.String(maxLength: 50),
                        SUSR4 = c.String(maxLength: 50),
                        SUSR5 = c.DateTime(),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Sku, unique: true, name: "IX_SKU");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.SKUs", "IX_SKU");
            DropTable("dbo.SKUs");
        }
    }
}
