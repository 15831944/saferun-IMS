namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workprocessdetsil : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkProcessDetails", "SKUId", c => c.Int(nullable: false));
            AddColumn("dbo.WorkProcessDetails", "ComponentSKU", c => c.String());
            AddColumn("dbo.WorkProcessDetails", "GraphSKU", c => c.String());
            CreateIndex("dbo.WorkProcessDetails", "SKUId");
            AddForeignKey("dbo.WorkProcessDetails", "SKUId", "dbo.SKUs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkProcessDetails", "SKUId", "dbo.SKUs");
            DropIndex("dbo.WorkProcessDetails", new[] { "SKUId" });
            DropColumn("dbo.WorkProcessDetails", "GraphSKU");
            DropColumn("dbo.WorkProcessDetails", "ComponentSKU");
            DropColumn("dbo.WorkProcessDetails", "SKUId");
        }
    }
}
