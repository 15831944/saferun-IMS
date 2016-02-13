namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddd : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MenuItems", "IX_menuTitle");
            AlterColumn("dbo.MenuItems", "Title", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.MenuItems", "Title", unique: true, name: "IX_menuTitle");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MenuItems", "IX_menuTitle");
            AlterColumn("dbo.MenuItems", "Title", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.MenuItems", "Title", unique: true, name: "IX_menuTitle");
        }
    }
}
