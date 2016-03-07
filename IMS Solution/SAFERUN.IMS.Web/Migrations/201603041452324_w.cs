namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class w : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkNo = c.String(maxLength: 30),
                        WorkId = c.Int(nullable: false),
                        ParentSKUId = c.Int(nullable: false),
                        ComponentSKUId = c.Int(nullable: false),
                        GraphSKU = c.String(maxLength: 30),
                        GraphVer = c.String(maxLength: 30),
                        ConsumeQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RequirementQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Brand = c.String(maxLength: 50),
                        Process = c.String(maxLength: 50),
                        Responsibility = c.String(maxLength: 50),
                        AltOrderProdctionDate = c.DateTime(),
                        AltProdctionDate1 = c.DateTime(),
                        ActualProdctionDate1 = c.DateTime(),
                        AltProdctionDate2 = c.DateTime(),
                        ActualProdctionDate2 = c.DateTime(),
                        AltProdctionDate3 = c.DateTime(),
                        ActualProdctionDate3 = c.DateTime(),
                        AltProdctionDate4 = c.DateTime(),
                        ActualProdctionDate4 = c.DateTime(),
                        AltProdctionDate5 = c.DateTime(),
                        ActualProdctionDate5 = c.DateTime(),
                        ConfirmUser = c.String(maxLength: 20),
                        Remark1 = c.String(maxLength: 200),
                        Remark2 = c.String(maxLength: 100),
                        BomComponentId = c.Int(nullable: false),
                        ParentBomComponentId = c.Int(),
                        OrderKey = c.String(maxLength: 30),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SKUs", t => t.ComponentSKUId, cascadeDelete: false)
                .ForeignKey("dbo.SKUs", t => t.ParentSKUId, cascadeDelete: false)
                .ForeignKey("dbo.Works", t => t.WorkId, cascadeDelete: true)
                .Index(t => new { t.WorkId, t.ParentSKUId, t.ComponentSKUId }, unique: true, name: "IX_WorkDetail");
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkNo = c.String(nullable: false, maxLength: 30),
                        WorkTypeId = c.Int(nullable: false),
                        OrderKey = c.String(maxLength: 30),
                        OrderId = c.Int(nullable: false),
                        PO = c.String(maxLength: 30),
                        User = c.String(maxLength: 20),
                        WorkDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Review = c.String(maxLength: 50),
                        ProductionConfirm = c.String(maxLength: 50),
                        OutsourceConfirm = c.String(maxLength: 50),
                        AssembleConfirm = c.String(maxLength: 50),
                        PurchaseConfirm = c.String(maxLength: 50),
                        ReviewDate = c.DateTime(),
                        ProductionConfirmDate = c.DateTime(),
                        OutsourceConfirmDate = c.DateTime(),
                        AssembleConfirmDate = c.DateTime(),
                        PurchaseConfirmDate = c.DateTime(),
                        Remark = c.String(maxLength: 200),
                        CreatedUserId = c.String(maxLength: 20),
                        CreatedDateTime = c.DateTime(),
                        LastEditUserId = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.WorkTypes", t => t.WorkTypeId, cascadeDelete: true)
                .Index(t => t.WorkNo, unique: true, name: "IX_Work")
                .Index(t => t.WorkTypeId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.WorkTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_WorkType");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Works", "WorkTypeId", "dbo.WorkTypes");
            DropForeignKey("dbo.WorkDetails", "WorkId", "dbo.Works");
            DropForeignKey("dbo.Works", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.WorkDetails", "ParentSKUId", "dbo.SKUs");
            DropForeignKey("dbo.WorkDetails", "ComponentSKUId", "dbo.SKUs");
            DropIndex("dbo.WorkTypes", "IX_WorkType");
            DropIndex("dbo.Works", new[] { "OrderId" });
            DropIndex("dbo.Works", new[] { "WorkTypeId" });
            DropIndex("dbo.Works", "IX_Work");
            DropIndex("dbo.WorkDetails", "IX_WorkDetail");
            DropTable("dbo.WorkTypes");
            DropTable("dbo.Works");
            DropTable("dbo.WorkDetails");
        }
    }
}
