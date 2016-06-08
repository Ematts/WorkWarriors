namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceRequestAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Homeowner_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Homeowners", t => t.Homeowner_ID)
                .Index(t => t.Homeowner_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceRequests", "Homeowner_ID", "dbo.Homeowners");
            DropIndex("dbo.ServiceRequests", new[] { "Homeowner_ID" });
            DropTable("dbo.ServiceRequests");
        }
    }
}
