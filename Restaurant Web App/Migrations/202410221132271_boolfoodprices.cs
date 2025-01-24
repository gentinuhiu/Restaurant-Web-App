namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolfoodprices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Foods", "disabledPrices", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Foods", "disabledPrices");
        }
    }
}
