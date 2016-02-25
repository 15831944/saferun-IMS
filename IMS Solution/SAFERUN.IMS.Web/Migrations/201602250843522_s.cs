namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(nullable: false, maxLength: 20),
                        ShortName = c.String(maxLength: 50),
                        Name = c.String(maxLength: 50),
                        CustomerType = c.String(maxLength: 50),
                        AddressLine1 = c.String(maxLength: 50),
                        AddressLine2 = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        Province = c.String(maxLength: 50),
                        ContactName = c.String(maxLength: 50),
                        Phone1 = c.String(maxLength: 50),
                        Phone2 = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.AccountNumber, unique: true, name: "IX_Supplier");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Suppliers", "IX_Supplier");
            DropTable("dbo.Suppliers");
        }
    }
}
