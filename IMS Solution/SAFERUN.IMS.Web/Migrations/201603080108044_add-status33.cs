namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatus33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionSchedules", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.ScheduleDetails", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduleDetails", "Status");
            DropColumn("dbo.ProductionSchedules", "Status");
        }
    }
}
