namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompletedPaths",
                c => new
                    {
                        CompletedPathId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        CompletedBidsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompletedPathId)
                .ForeignKey("dbo.CompletedBids", t => t.CompletedBidsID, cascadeDelete: true)
                .Index(t => t.CompletedBidsID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompletedPaths", "CompletedBidsID", "dbo.CompletedBids");
            DropIndex("dbo.CompletedPaths", new[] { "CompletedBidsID" });
            DropTable("dbo.CompletedPaths");
        }
    }
}
