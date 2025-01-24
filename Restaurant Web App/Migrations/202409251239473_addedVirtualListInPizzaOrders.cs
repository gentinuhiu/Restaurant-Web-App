namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedVirtualListInPizzaOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CartPizzaOrders",
                c => new
                    {
                        Cart_Id = c.Int(nullable: false),
                        PizzaOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Cart_Id, t.PizzaOrder_Id })
                .ForeignKey("dbo.Carts", t => t.Cart_Id, cascadeDelete: true)
                .ForeignKey("dbo.PizzaOrders", t => t.PizzaOrder_Id, cascadeDelete: true)
                .Index(t => t.Cart_Id)
                .Index(t => t.PizzaOrder_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartPizzaOrders", "PizzaOrder_Id", "dbo.PizzaOrders");
            DropForeignKey("dbo.CartPizzaOrders", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.CartPizzaOrders", new[] { "PizzaOrder_Id" });
            DropIndex("dbo.CartPizzaOrders", new[] { "Cart_Id" });
            DropTable("dbo.CartPizzaOrders");
            DropTable("dbo.Carts");
        }
    }
}
