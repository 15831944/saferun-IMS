namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BOMComponents", "MadeType", c => c.String(maxLength: 20));
            AddColumn("dbo.BOMComponents", "SKUGroup", c => c.String(maxLength: 20));
            AddColumn("dbo.ProductionPlans", "SKUGroup", c => c.String(maxLength: 20));
            AddColumn("dbo.ProductionPlans", "MadeType", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductionPlans", "MadeType");
            DropColumn("dbo.ProductionPlans", "SKUGroup");
            DropColumn("dbo.BOMComponents", "SKUGroup");
            DropColumn("dbo.BOMComponents", "MadeType");
        }
    }
}
