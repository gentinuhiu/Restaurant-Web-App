namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserIdInPizzaOrderClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PizzaOrders", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.PizzaOrders", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.PizzaOrders", "ApplicationUser_Id");
            AddForeignKey("dbo.PizzaOrders", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PizzaOrders", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PizzaOrders", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.PizzaOrders", "ApplicationUser_Id");
            DropColumn("dbo.PizzaOrders", "UserId");
        }
    }
}
