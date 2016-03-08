namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workdetailid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleDetails", "WorkDetailId", c => c.Int());
            CreateIndex("dbo.ScheduleDetails", "WorkDetailId");
            AddForeignKey("dbo.ScheduleDetails", "WorkDetailId", "dbo.WorkDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleDetails", "WorkDetailId", "dbo.WorkDetails");
            DropIndex("dbo.ScheduleDetails", new[] { "WorkDetailId" });
            DropColumn("dbo.ScheduleDetails", "WorkDetailId");
        }
    }
}
