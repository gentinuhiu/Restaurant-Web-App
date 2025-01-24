namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class totalprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodOrders", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodOrders", "TotalPrice");
        }
    }
}
