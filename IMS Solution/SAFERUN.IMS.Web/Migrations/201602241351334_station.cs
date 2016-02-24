namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class station : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stations", "StandardElapsedTime", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stations", "WorkingTime", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stations", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Stations", "Description", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stations", "Description", c => c.String());
            DropColumn("dbo.Stations", "Status");
            DropColumn("dbo.Stations", "WorkingTime");
            DropColumn("dbo.Stations", "StandardElapsedTime");
        }
    }
}
