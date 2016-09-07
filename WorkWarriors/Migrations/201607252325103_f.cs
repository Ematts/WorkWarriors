namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ConUsername = c.String(),
                        HomeUsername = c.String(),
                        ConFirstName = c.String(),
                        HomeFirstname = c.String(),
                        ConLastName = c.String(),
                        HomeLastName = c.String(),
                        ConAddress = c.String(),
                        HomeAddress = c.String(),
                        ConCity = c.String(),
                        HomeCity = c.String(),
                        ConState = c.String(),
                        HomeState = c.String(),
                        ConZip = c.String(),
                        HomeZip = c.String(),
                        ConEmail = c.String(),
                        HomeEmail = c.String(),
                        PostedDate = c.DateTime(nullable: false),
                        Bid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CompletionDeadline = c.DateTime(nullable: false),
                        Description = c.String(),
                        Completed = c.Boolean(nullable: false),
                        Invoice = c.Int(nullable: false),
                        ContractorDue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContractorPaid = c.Boolean(nullable: false),
                        Service_Number = c.Int(nullable: false),
                        Expired = c.Boolean(nullable: false),
                        Review = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reviews");
        }
    }
}
