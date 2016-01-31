namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProcessNodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        MachineModel = c.String(nullable: false, maxLength: 20),
                        Node = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 50),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProcessNodes");
        }
    }
}
