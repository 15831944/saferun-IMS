namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stations", "Equipment", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stations", "Equipment");
        }
    }
}
