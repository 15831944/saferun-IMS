namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectNodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                        ElapsedDay = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Description = c.String(maxLength: 100),
                        ProjectTypeId = c.Int(nullable: false),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTypes", t => t.ProjectTypeId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.ProjectTypeId);
            
            CreateTable(
                "dbo.ProjectTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false, maxLength: 50),
                        Version = c.String(nullable: false, maxLength: 10),
                        Status = c.Int(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectNodes", "ProjectTypeId", "dbo.ProjectTypes");
            DropForeignKey("dbo.ProjectNodes", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.ProjectNodes", new[] { "ProjectTypeId" });
            DropIndex("dbo.ProjectNodes", new[] { "DepartmentId" });
            DropTable("dbo.ProjectTypes");
            DropTable("dbo.ProjectNodes");
        }
    }
}
