namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BOMComponents", "ProductionProcessId", "dbo.ProductionProcesses");
            DropIndex("dbo.BOMComponents", new[] { "ProductionProcessId" });
            AlterColumn("dbo.BOMComponents", "ProductionProcessId", c => c.Int());
            CreateIndex("dbo.BOMComponents", "ProductionProcessId");
            AddForeignKey("dbo.BOMComponents", "ProductionProcessId", "dbo.ProductionProcesses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BOMComponents", "ProductionProcessId", "dbo.ProductionProcesses");
            DropIndex("dbo.BOMComponents", new[] { "ProductionProcessId" });
            AlterColumn("dbo.BOMComponents", "ProductionProcessId", c => c.Int(nullable: false));
            CreateIndex("dbo.BOMComponents", "ProductionProcessId");
            AddForeignKey("dbo.BOMComponents", "ProductionProcessId", "dbo.ProductionProcesses", "Id", cascadeDelete: true);
        }
    }
}
