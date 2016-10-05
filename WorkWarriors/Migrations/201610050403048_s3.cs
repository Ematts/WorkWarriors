namespace WorkWarriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayPalListenerModels", "_PayPalCheckoutInfo_memo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayPalListenerModels", "_PayPalCheckoutInfo_memo");
        }
    }
}
