namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MenuItems", "IX_menuTitle");
            DropIndex("dbo.MenuItems", "IX_menuCode");
            DropIndex("dbo.MenuItems", "IX_menuUrl");
            CreateIndex("dbo.MenuItems", new[] { "Title", "Code", "Url" }, unique: true, name: "IX_MenuItem");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MenuItems", "IX_MenuItem");
            CreateIndex("dbo.MenuItems", "Url", unique: true, name: "IX_menuUrl");
            CreateIndex("dbo.MenuItems", "Code", unique: true, name: "IX_menuCode");
            CreateIndex("dbo.MenuItems", "Title", unique: true, name: "IX_menuTitle");
        }
    }
}
