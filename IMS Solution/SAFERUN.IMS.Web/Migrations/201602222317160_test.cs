namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SKUs", "ProductName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SKUs", "ProductName");
        }
    }
}
