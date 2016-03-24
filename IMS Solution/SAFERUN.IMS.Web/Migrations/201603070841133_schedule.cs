namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductionSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScheduleNo = c.String(nullable: false, maxLength: 30),
                        WorkId = c.Int(nullable: false),
                        OrderKey = c.String(maxLength: 30),
                        OrderDate = c.DateTime(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        CompletedDate = c.DateTime(nullable: false),
                        Ower = c.String(maxLength: 20),
                        ScheduleDate = c.DateTime(nullable: false),
                        Remark = c.String(maxLength: 100),
                        CustomerId = c.Int(),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Works", t => t.WorkId, cascadeDelete: true)
                .Index(t => t.ScheduleNo, unique: true, name: "IX_ProductionSchedule")
                .Index(t => t.WorkId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.ScheduleDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScheduleNo = c.String(maxLength: 30),
                        WorkNo = c.String(maxLength: 30),
                        ParentSKUId = c.Int(nullable: false),
                        ComponentSKUId = c.Int(nullable: false),
                        GraphSKU = c.String(maxLength: 30),
                        GraphVer = c.String(maxLength: 20),
                        ConsumeQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RequirementQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ScheduleProductionQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActualProductionQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StationId = c.Int(),
                        ShiftId = c.Int(),
                        Operator = c.String(maxLength: 20),
                        AltProdctionDate1 = c.DateTime(),
                        ActualProdctionDate1 = c.DateTime(),
                        AltProdctionDate2 = c.DateTime(),
                        ActualProdctionDate2 = c.DateTime(),
                        AltProdctionDate3 = c.DateTime(),
                        ActualProdctionDate3 = c.DateTime(),
                        AltConsumeTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActualConsumeTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark1 = c.String(maxLength: 100),
                        Remark2 = c.String(maxLength: 100),
                        ProductionScheduleId = c.Int(nullable: false),
                        BomComponentId = c.Int(nullable: false),
                        ParentBomComponentId = c.Int(),
                        OrderKey = c.String(),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SKUs", t => t.ComponentSKUId, cascadeDelete: false)
                .ForeignKey("dbo.SKUs", t => t.ParentSKUId, cascadeDelete: false)
                .ForeignKey("dbo.ProductionSchedules", t => t.ProductionScheduleId, cascadeDelete: true)
                .ForeignKey("dbo.Shifts", t => t.ShiftId)
                .ForeignKey("dbo.Stations", t => t.StationId)
                .Index(t => t.ParentSKUId)
                .Index(t => t.ComponentSKUId)
                .Index(t => t.StationId)
                .Index(t => t.ShiftId)
                .Index(t => t.ProductionScheduleId);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        OnTime = c.String(maxLength: 20),
                        OffTime = c.String(maxLength: 20),
                        Remark = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, name: "IX_Shift");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductionSchedules", "WorkId", "dbo.Works");
            DropForeignKey("dbo.ScheduleDetails", "StationId", "dbo.Stations");
            DropForeignKey("dbo.ScheduleDetails", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.ScheduleDetails", "ProductionScheduleId", "dbo.ProductionSchedules");
            DropForeignKey("dbo.ScheduleDetails", "ParentSKUId", "dbo.SKUs");
            DropForeignKey("dbo.ScheduleDetails", "ComponentSKUId", "dbo.SKUs");
            DropForeignKey("dbo.ProductionSchedules", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Shifts", "IX_Shift");
            DropIndex("dbo.ScheduleDetails", new[] { "ProductionScheduleId" });
            DropIndex("dbo.ScheduleDetails", new[] { "ShiftId" });
            DropIndex("dbo.ScheduleDetails", new[] { "StationId" });
            DropIndex("dbo.ScheduleDetails", new[] { "ComponentSKUId" });
            DropIndex("dbo.ScheduleDetails", new[] { "ParentSKUId" });
            DropIndex("dbo.ProductionSchedules", new[] { "CustomerId" });
            DropIndex("dbo.ProductionSchedules", new[] { "WorkId" });
            DropIndex("dbo.ProductionSchedules", "IX_ProductionSchedule");
            DropTable("dbo.Shifts");
            DropTable("dbo.ScheduleDetails");
            DropTable("dbo.ProductionSchedules");
        }
    }
}
