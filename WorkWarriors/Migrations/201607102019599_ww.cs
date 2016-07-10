namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ww : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "CompletedBidsId", "dbo.CompletedBids");
            DropIndex("dbo.Files", new[] { "CompletedBidsId" });
            RenameColumn(table: "dbo.Files", name: "CompletedBidsId", newName: "CompletedBids_ID");
            AlterColumn("dbo.Files", "CompletedBids_ID", c => c.Int());
            CreateIndex("dbo.Files", "CompletedBids_ID");
            AddForeignKey("dbo.Files", "CompletedBids_ID", "dbo.CompletedBids", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "CompletedBids_ID", "dbo.CompletedBids");
            DropIndex("dbo.Files", new[] { "CompletedBids_ID" });
            AlterColumn("dbo.Files", "CompletedBids_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Files", name: "CompletedBids_ID", newName: "CompletedBidsId");
            CreateIndex("dbo.Files", "CompletedBidsId");
            AddForeignKey("dbo.Files", "CompletedBidsId", "dbo.CompletedBids", "ID", cascadeDelete: true);
        }
    }
}
