namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BOMComponents", "ProductionProcessId", c => c.Int(nullable: false));
            AddColumn("dbo.BOMComponents", "Version", c => c.Int(nullable: false));
            CreateIndex("dbo.BOMComponents", "ProductionProcessId");
            AddForeignKey("dbo.BOMComponents", "ProductionProcessId", "dbo.ProductionProcesses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BOMComponents", "ProductionProcessId", "dbo.ProductionProcesses");
            DropIndex("dbo.BOMComponents", new[] { "ProductionProcessId" });
            DropColumn("dbo.BOMComponents", "Version");
            DropColumn("dbo.BOMComponents", "ProductionProcessId");
        }
    }
}
