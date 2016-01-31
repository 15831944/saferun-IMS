namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(nullable: false, maxLength: 20),
                        ShortName = c.String(nullable: false, maxLength: 20),
                        Name = c.String(maxLength: 50),
                        CustomerType = c.String(maxLength: 20),
                        AddressLine1 = c.String(maxLength: 110),
                        AddressLine2 = c.String(maxLength: 110),
                        City = c.String(maxLength: 20),
                        Province = c.String(maxLength: 20),
                        ContactName = c.String(maxLength: 20),
                        Phone1 = c.String(maxLength: 20),
                        Phone2 = c.String(maxLength: 20),
                        Email = c.String(maxLength: 20),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.AccountNumber, unique: true, name: "IX_customer");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", "IX_customer");
            DropTable("dbo.Customers");
        }
    }
}
