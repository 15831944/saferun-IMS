namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfd1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BOMComponents", "FinishedSKU", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.BOMComponents", "Remark1", c => c.String(maxLength: 300));
            AlterColumn("dbo.BOMComponents", "Remark2", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BOMComponents", "Remark2", c => c.String());
            AlterColumn("dbo.BOMComponents", "Remark1", c => c.String());
            DropColumn("dbo.BOMComponents", "FinishedSKU");
        }
    }
}
