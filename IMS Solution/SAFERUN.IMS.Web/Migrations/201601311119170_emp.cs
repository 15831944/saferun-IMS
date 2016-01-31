namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        WorkNo = c.String(nullable: false, maxLength: 10),
                        Title = c.String(maxLength: 10),
                        BirthDate = c.DateTime(nullable: false),
                        MaritalStatus = c.String(maxLength: 10),
                        Gender = c.String(maxLength: 10),
                        HireDate = c.DateTime(nullable: false),
                        ManagerID = c.Int(),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.ManagerID)
                .Index(t => t.WorkNo, unique: true, name: "IX_Employee")
                .Index(t => t.ManagerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "ManagerID", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "ManagerID" });
            DropIndex("dbo.Employees", "IX_Employee");
            DropTable("dbo.Employees");
        }
    }
}
