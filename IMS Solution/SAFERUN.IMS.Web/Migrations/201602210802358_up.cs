namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchasePlans", "BomComponentId", c => c.Int(nullable: false));
            AddColumn("dbo.PurchasePlans", "ParentBomComponentId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchasePlans", "ParentBomComponentId");
            DropColumn("dbo.PurchasePlans", "BomComponentId");
        }
    }
}
