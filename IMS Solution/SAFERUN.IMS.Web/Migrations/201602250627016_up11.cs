namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionPlans", "ProductionProcessId", c => c.Int());
            CreateIndex("dbo.ProductionPlans", "ProductionProcessId");
            AddForeignKey("dbo.ProductionPlans", "ProductionProcessId", "dbo.ProductionProcesses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductionPlans", "ProductionProcessId", "dbo.ProductionProcesses");
            DropIndex("dbo.ProductionPlans", new[] { "ProductionProcessId" });
            DropColumn("dbo.ProductionPlans", "ProductionProcessId");
        }
    }
}
