namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Multicomponents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceID = c.String(nullable: false, maxLength: 20),
                        Order = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 100),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.DeviceID, unique: true, name: "IX_Multicomponent");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Multicomponents", "IX_Multicomponent");
            DropTable("dbo.Multicomponents");
        }
    }
}
