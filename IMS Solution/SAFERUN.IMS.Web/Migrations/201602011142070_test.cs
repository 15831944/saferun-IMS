namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefectCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Code = c.String(maxLength: 20),
                        Description = c.String(maxLength: 50),
                        DefectTypeId = c.Int(nullable: false),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DefectTypes", t => t.DefectTypeId, cascadeDelete: true)
                .Index(t => t.Name, unique: true, name: "IX_DefectCode")
                .Index(t => t.DefectTypeId);
            
            CreateTable(
                "dbo.DefectTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Code = c.String(maxLength: 20),
                        Description = c.String(maxLength: 50),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_DefectType");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DefectCodes", "DefectTypeId", "dbo.DefectTypes");
            DropIndex("dbo.DefectTypes", "IX_DefectType");
            DropIndex("dbo.DefectCodes", new[] { "DefectTypeId" });
            DropIndex("dbo.DefectCodes", "IX_DefectCode");
            DropTable("dbo.DefectTypes");
            DropTable("dbo.DefectCodes");
        }
    }
}
