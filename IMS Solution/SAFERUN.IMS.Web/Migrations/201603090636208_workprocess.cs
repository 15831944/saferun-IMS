namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workprocess : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkProcessDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkProcessId = c.Int(nullable: false),
                        ProcessStepId = c.Int(nullable: false),
                        StepName = c.String(maxLength: 50),
                        StationId = c.Int(),
                        StandardElapsedTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartingDateTime = c.DateTime(),
                        CompletedDateTime = c.DateTime(),
                        ElapsedTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WorkingTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Operator = c.String(maxLength: 30),
                        QCConfirm = c.String(maxLength: 30),
                        QCConfirmDateTime = c.DateTime(),
                        CompletedConfirm = c.String(maxLength: 30),
                        Status = c.Int(nullable: false),
                        Remark = c.String(maxLength: 100),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProcessSteps", t => t.ProcessStepId, cascadeDelete: false)
                .ForeignKey("dbo.Stations", t => t.StationId)
                .ForeignKey("dbo.WorkProcesses", t => t.WorkProcessId, cascadeDelete: true)
                .Index(t => t.WorkProcessId)
                .Index(t => t.ProcessStepId)
                .Index(t => t.StationId);
            
            CreateTable(
                "dbo.WorkProcesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkNo = c.String(maxLength: 30),
                        WorkId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        OrderKey = c.String(maxLength: 30),
                        ProjectName = c.String(maxLength: 50),
                        SKUId = c.Int(nullable: false),
                        GraphSKU = c.String(maxLength: 30),
                        RequirementQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductionQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinishedQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WorkItems = c.Int(nullable: false),
                        ProductionProcessId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Operator = c.String(maxLength: 30),
                        WorkDate = c.DateTime(nullable: false),
                        Remark = c.String(maxLength: 100),
                        WorkDetailId = c.Int(),
                        CustomerId = c.Int(nullable: false),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: false)
                .ForeignKey("dbo.ProductionProcesses", t => t.ProductionProcessId, cascadeDelete: true)
                .ForeignKey("dbo.SKUs", t => t.SKUId, cascadeDelete: false)
                .ForeignKey("dbo.Works", t => t.WorkId, cascadeDelete: false)
                .ForeignKey("dbo.WorkDetails", t => t.WorkDetailId)
                .Index(t => t.WorkId)
                .Index(t => t.OrderId)
                .Index(t => t.SKUId)
                .Index(t => t.ProductionProcessId)
                .Index(t => t.WorkDetailId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkProcessDetails", "WorkProcessId", "dbo.WorkProcesses");
            DropForeignKey("dbo.WorkProcesses", "WorkDetailId", "dbo.WorkDetails");
            DropForeignKey("dbo.WorkProcesses", "WorkId", "dbo.Works");
            DropForeignKey("dbo.WorkProcesses", "SKUId", "dbo.SKUs");
            DropForeignKey("dbo.WorkProcesses", "ProductionProcessId", "dbo.ProductionProcesses");
            DropForeignKey("dbo.WorkProcesses", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.WorkProcesses", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.WorkProcessDetails", "StationId", "dbo.Stations");
            DropForeignKey("dbo.WorkProcessDetails", "ProcessStepId", "dbo.ProcessSteps");
            DropIndex("dbo.WorkProcesses", new[] { "CustomerId" });
            DropIndex("dbo.WorkProcesses", new[] { "WorkDetailId" });
            DropIndex("dbo.WorkProcesses", new[] { "ProductionProcessId" });
            DropIndex("dbo.WorkProcesses", new[] { "SKUId" });
            DropIndex("dbo.WorkProcesses", new[] { "OrderId" });
            DropIndex("dbo.WorkProcesses", new[] { "WorkId" });
            DropIndex("dbo.WorkProcessDetails", new[] { "StationId" });
            DropIndex("dbo.WorkProcessDetails", new[] { "ProcessStepId" });
            DropIndex("dbo.WorkProcessDetails", new[] { "WorkProcessId" });
            DropTable("dbo.WorkProcesses");
            DropTable("dbo.WorkProcessDetails");
        }
    }
}
