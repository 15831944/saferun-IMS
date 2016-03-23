namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddd1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkDetails", "ProductionQty", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.WorkDetails", "ProdctionQty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkDetails", "ProdctionQty", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.WorkDetails", "ProductionQty");
        }
    }
}
