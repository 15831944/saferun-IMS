namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkProcesses", "ProductionProcessId", "dbo.ProductionProcesses");
            DropIndex("dbo.WorkProcesses", new[] { "ProductionProcessId" });
            AlterColumn("dbo.WorkProcesses", "ProductionProcessId", c => c.Int());
            CreateIndex("dbo.WorkProcesses", "ProductionProcessId");
            AddForeignKey("dbo.WorkProcesses", "ProductionProcessId", "dbo.ProductionProcesses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkProcesses", "ProductionProcessId", "dbo.ProductionProcesses");
            DropIndex("dbo.WorkProcesses", new[] { "ProductionProcessId" });
            AlterColumn("dbo.WorkProcesses", "ProductionProcessId", c => c.Int(nullable: false));
            CreateIndex("dbo.WorkProcesses", "ProductionProcessId");
            AddForeignKey("dbo.WorkProcesses", "ProductionProcessId", "dbo.ProductionProcesses", "Id", cascadeDelete: true);
        }
    }
}
