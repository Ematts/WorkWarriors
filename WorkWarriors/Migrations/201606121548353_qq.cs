namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qq : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractorAcceptedBids", "ConFirstName", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "HomeFirstname", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "ConLastName", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "HomeLastName", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "ConAddress", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "HomeAddress", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "ConCity", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "HomeCity", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "ConState", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "HomeState", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "ConZip", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "HomeZip", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "Conemail", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "Homeemail", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "PostedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ContractorAcceptedBids", "Bid", c => c.Double(nullable: false));
            AddColumn("dbo.ContractorAcceptedBids", "CompletionDeadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.ContractorAcceptedBids", "Description", c => c.String());
            AddColumn("dbo.ContractorAcceptedBids", "confirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContractorAcceptedBids", "confirmed");
            DropColumn("dbo.ContractorAcceptedBids", "Description");
            DropColumn("dbo.ContractorAcceptedBids", "CompletionDeadline");
            DropColumn("dbo.ContractorAcceptedBids", "Bid");
            DropColumn("dbo.ContractorAcceptedBids", "PostedDate");
            DropColumn("dbo.ContractorAcceptedBids", "Homeemail");
            DropColumn("dbo.ContractorAcceptedBids", "Conemail");
            DropColumn("dbo.ContractorAcceptedBids", "HomeZip");
            DropColumn("dbo.ContractorAcceptedBids", "ConZip");
            DropColumn("dbo.ContractorAcceptedBids", "HomeState");
            DropColumn("dbo.ContractorAcceptedBids", "ConState");
            DropColumn("dbo.ContractorAcceptedBids", "HomeCity");
            DropColumn("dbo.ContractorAcceptedBids", "ConCity");
            DropColumn("dbo.ContractorAcceptedBids", "HomeAddress");
            DropColumn("dbo.ContractorAcceptedBids", "ConAddress");
            DropColumn("dbo.ContractorAcceptedBids", "HomeLastName");
            DropColumn("dbo.ContractorAcceptedBids", "ConLastName");
            DropColumn("dbo.ContractorAcceptedBids", "HomeFirstname");
            DropColumn("dbo.ContractorAcceptedBids", "ConFirstName");
        }
    }
}
