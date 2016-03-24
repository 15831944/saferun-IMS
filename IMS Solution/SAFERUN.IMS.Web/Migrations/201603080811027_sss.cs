namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionSchedules", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductionSchedules", "OrderId");
            AddForeignKey("dbo.ProductionSchedules", "OrderId", "dbo.Orders", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductionSchedules", "OrderId", "dbo.Orders");
            DropIndex("dbo.ProductionSchedules", new[] { "OrderId" });
            DropColumn("dbo.ProductionSchedules", "OrderId");
        }
    }
}
