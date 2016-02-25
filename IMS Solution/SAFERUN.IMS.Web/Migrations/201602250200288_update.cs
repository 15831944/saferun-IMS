namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProcessSteps", "StationId", c => c.Int());
            AddColumn("dbo.Stations", "EquipmentNumber", c => c.Int(nullable: false));
            CreateIndex("dbo.ProcessSteps", "StationId");
            AddForeignKey("dbo.ProcessSteps", "StationId", "dbo.Stations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProcessSteps", "StationId", "dbo.Stations");
            DropIndex("dbo.ProcessSteps", new[] { "StationId" });
            DropColumn("dbo.Stations", "EquipmentNumber");
            DropColumn("dbo.ProcessSteps", "StationId");
        }
    }
}
