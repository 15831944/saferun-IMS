namespace SAFERUN.IMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.String(nullable: false, maxLength: 20),
                        To = c.String(nullable: false, maxLength: 20),
                        Title = c.String(maxLength: 50),
                        Content = c.String(maxLength: 500),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        MessageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.MessageId)
                .Index(t => t.MessageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "MessageId", "dbo.Messages");
            DropIndex("dbo.Messages", new[] { "MessageId" });
            DropTable("dbo.Messages");
        }
    }
}
