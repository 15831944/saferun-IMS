namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkProcesses", "OrderKey", c => c.String(maxLength: 30));
            DropColumn("dbo.WorkProcesses", "OrderNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkProcesses", "OrderNo", c => c.String(maxLength: 30));
            DropColumn("dbo.WorkProcesses", "OrderKey");
        }
    }
}
