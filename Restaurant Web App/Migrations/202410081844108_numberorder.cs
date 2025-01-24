namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numberorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodOrders", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodOrders", "Number");
        }
    }
}
