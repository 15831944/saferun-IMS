namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkDetails", "ProdctionQty", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.WorkDetails", "FinishedQty", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkDetails", "FinishedQty");
            DropColumn("dbo.WorkDetails", "ProdctionQty");
        }
    }
}
