namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectTypes", "Model", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectTypes", "Model");
        }
    }
}
