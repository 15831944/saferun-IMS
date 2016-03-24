namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doingqty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkDetails", "DoingQty", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkDetails", "DoingQty");
        }
    }
}
