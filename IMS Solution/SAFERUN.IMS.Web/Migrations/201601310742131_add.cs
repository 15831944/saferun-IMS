namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodeType = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CodeType, unique: true);
            
            CreateTable(
                "dbo.CodeItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 20),
                        Text = c.String(nullable: false, maxLength: 20),
                        BaseCodeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BaseCodes", t => t.BaseCodeId, cascadeDelete: true)
                .Index(t => t.Code, unique: true)
                .Index(t => t.BaseCodeId);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 20),
                        Url = c.String(nullable: false, maxLength: 100),
                        IsEnabled = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.ParentId)
                .Index(t => t.Title, unique: true, name: "IX_menuTitle")
                .Index(t => t.Code, unique: true, name: "IX_menuCode")
                .Index(t => t.Url, unique: true, name: "IX_menuUrl")
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.RoleMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 20),
                        MenuId = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.MenuId, cascadeDelete: true)
                .Index(t => new { t.RoleName, t.MenuId }, unique: true, name: "IX_rolemenu");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleMenus", "MenuId", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItems", "ParentId", "dbo.MenuItems");
            DropForeignKey("dbo.CodeItems", "BaseCodeId", "dbo.BaseCodes");
            DropIndex("dbo.RoleMenus", "IX_rolemenu");
            DropIndex("dbo.MenuItems", new[] { "ParentId" });
            DropIndex("dbo.MenuItems", "IX_menuUrl");
            DropIndex("dbo.MenuItems", "IX_menuCode");
            DropIndex("dbo.MenuItems", "IX_menuTitle");
            DropIndex("dbo.CodeItems", new[] { "BaseCodeId" });
            DropIndex("dbo.CodeItems", new[] { "Code" });
            DropIndex("dbo.BaseCodes", new[] { "CodeType" });
            DropTable("dbo.RoleMenus");
            DropTable("dbo.MenuItems");
            DropTable("dbo.CodeItems");
            DropTable("dbo.BaseCodes");
        }
    }
}
