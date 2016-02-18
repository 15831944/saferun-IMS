namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProcessSteps", "IX_ProcessStep");
            AddColumn("dbo.ProcessSteps", "StepName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.ProcessSteps", "ElapsedTime", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ProcessSteps", "Status", c => c.Int(nullable: false));
            CreateIndex("dbo.ProcessSteps", new[] { "Name", "Order", "StepName" }, unique: true, name: "IX_ProcessStep");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProcessSteps", "IX_ProcessStep");
            DropColumn("dbo.ProcessSteps", "Status");
            DropColumn("dbo.ProcessSteps", "ElapsedTime");
            DropColumn("dbo.ProcessSteps", "StepName");
            CreateIndex("dbo.ProcessSteps", "Name", unique: true, name: "IX_ProcessStep");
        }
    }
}
