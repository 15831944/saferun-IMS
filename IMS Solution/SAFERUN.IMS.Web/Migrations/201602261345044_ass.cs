namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssemblyPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderKey = c.String(maxLength: 20),
                        SKUId = c.Int(nullable: false),
                        DesignName = c.String(maxLength: 50),
                        ComponentSKU = c.String(maxLength: 30),
                        FinishedSKU = c.String(maxLength: 30),
                        OrderDate = c.DateTime(nullable: false),
                        RequirementDate = c.DateTime(),
                        ResolveDate1 = c.DateTime(),
                        ActualReleaseDate1 = c.DateTime(),
                        SetPlanDate2 = c.DateTime(),
                        PutawayDate2 = c.DateTime(),
                        SetPlanDate3 = c.DateTime(),
                        PutawayDate3 = c.DateTime(),
                        SetPlanDate4 = c.DateTime(),
                        PutawayDate4 = c.DateTime(),
                        SetPlanDate5 = c.DateTime(),
                        DeliveryDate5 = c.DateTime(),
                        PutawayDate5 = c.DateTime(),
                        SetPlanDate6 = c.DateTime(),
                        ActualStartDate6 = c.DateTime(),
                        ActualCompletedDate6 = c.DateTime(),
                        Remark = c.String(maxLength: 100),
                        Status = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        BomComponentId = c.Int(nullable: false),
                        ParentBomComponentId = c.Int(),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.SKUs", t => t.SKUId, cascadeDelete: true)
                .Index(t => t.SKUId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssemblyPlans", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.AssemblyPlans", "OrderId", "dbo.Orders");
            DropIndex("dbo.AssemblyPlans", new[] { "OrderId" });
            DropIndex("dbo.AssemblyPlans", new[] { "SKUId" });
            DropTable("dbo.AssemblyPlans");
        }
    }
}
