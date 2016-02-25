namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionPlans", "PlanedStartDate", c => c.DateTime());
            AddColumn("dbo.ProductionPlans", "PlanedCompletedDate", c => c.DateTime());
            AddColumn("dbo.ProductionPlans", "ActualStartDate", c => c.DateTime());
            AddColumn("dbo.ProductionPlans", "ActualCompletedDate", c => c.DateTime());
            AddColumn("dbo.PurchasePlans", "PlanedStartDate", c => c.DateTime());
            AddColumn("dbo.PurchasePlans", "ActualStartDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchasePlans", "ActualStartDate");
            DropColumn("dbo.PurchasePlans", "PlanedStartDate");
            DropColumn("dbo.ProductionPlans", "ActualCompletedDate");
            DropColumn("dbo.ProductionPlans", "ActualStartDate");
            DropColumn("dbo.ProductionPlans", "PlanedCompletedDate");
            DropColumn("dbo.ProductionPlans", "PlanedStartDate");
        }
    }
}
