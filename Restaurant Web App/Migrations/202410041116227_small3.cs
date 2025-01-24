namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class small3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FoodOrderCarts", "FoodOrder_Id", "dbo.FoodOrders");
            DropForeignKey("dbo.FoodOrderCarts", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.FoodOrderCarts", new[] { "FoodOrder_Id" });
            DropIndex("dbo.FoodOrderCarts", new[] { "Cart_Id" });
            CreateTable(
                "dbo.OrderIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderNoIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoIngredientId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OptionId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderIngredientFoodOrders",
                c => new
                    {
                        OrderIngredient_Id = c.Int(nullable: false),
                        FoodOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderIngredient_Id, t.FoodOrder_Id })
                .ForeignKey("dbo.OrderIngredients", t => t.OrderIngredient_Id, cascadeDelete: true)
                .ForeignKey("dbo.FoodOrders", t => t.FoodOrder_Id, cascadeDelete: true)
                .Index(t => t.OrderIngredient_Id)
                .Index(t => t.FoodOrder_Id);
            
            CreateTable(
                "dbo.OrderNoIngredientFoodOrders",
                c => new
                    {
                        OrderNoIngredient_Id = c.Int(nullable: false),
                        FoodOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderNoIngredient_Id, t.FoodOrder_Id })
                .ForeignKey("dbo.OrderNoIngredients", t => t.OrderNoIngredient_Id, cascadeDelete: true)
                .ForeignKey("dbo.FoodOrders", t => t.FoodOrder_Id, cascadeDelete: true)
                .Index(t => t.OrderNoIngredient_Id)
                .Index(t => t.FoodOrder_Id);
            
            CreateTable(
                "dbo.OrderOptionFoodOrders",
                c => new
                    {
                        OrderOption_Id = c.Int(nullable: false),
                        FoodOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderOption_Id, t.FoodOrder_Id })
                .ForeignKey("dbo.OrderOptions", t => t.OrderOption_Id, cascadeDelete: true)
                .ForeignKey("dbo.FoodOrders", t => t.FoodOrder_Id, cascadeDelete: true)
                .Index(t => t.OrderOption_Id)
                .Index(t => t.FoodOrder_Id);
            
            AddColumn("dbo.FoodOrders", "Cart_Id", c => c.Int());
            CreateIndex("dbo.FoodOrders", "Cart_Id");
            AddForeignKey("dbo.FoodOrders", "Cart_Id", "dbo.Carts", "Id");
            DropTable("dbo.FoodOrderCarts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FoodOrderCarts",
                c => new
                    {
                        FoodOrder_Id = c.Int(nullable: false),
                        Cart_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FoodOrder_Id, t.Cart_Id });
            
            DropForeignKey("dbo.FoodOrders", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.OrderOptionFoodOrders", "FoodOrder_Id", "dbo.FoodOrders");
            DropForeignKey("dbo.OrderOptionFoodOrders", "OrderOption_Id", "dbo.OrderOptions");
            DropForeignKey("dbo.OrderNoIngredientFoodOrders", "FoodOrder_Id", "dbo.FoodOrders");
            DropForeignKey("dbo.OrderNoIngredientFoodOrders", "OrderNoIngredient_Id", "dbo.OrderNoIngredients");
            DropForeignKey("dbo.OrderIngredientFoodOrders", "FoodOrder_Id", "dbo.FoodOrders");
            DropForeignKey("dbo.OrderIngredientFoodOrders", "OrderIngredient_Id", "dbo.OrderIngredients");
            DropIndex("dbo.OrderOptionFoodOrders", new[] { "FoodOrder_Id" });
            DropIndex("dbo.OrderOptionFoodOrders", new[] { "OrderOption_Id" });
            DropIndex("dbo.OrderNoIngredientFoodOrders", new[] { "FoodOrder_Id" });
            DropIndex("dbo.OrderNoIngredientFoodOrders", new[] { "OrderNoIngredient_Id" });
            DropIndex("dbo.OrderIngredientFoodOrders", new[] { "FoodOrder_Id" });
            DropIndex("dbo.OrderIngredientFoodOrders", new[] { "OrderIngredient_Id" });
            DropIndex("dbo.FoodOrders", new[] { "Cart_Id" });
            DropColumn("dbo.FoodOrders", "Cart_Id");
            DropTable("dbo.OrderOptionFoodOrders");
            DropTable("dbo.OrderNoIngredientFoodOrders");
            DropTable("dbo.OrderIngredientFoodOrders");
            DropTable("dbo.OrderOptions");
            DropTable("dbo.OrderNoIngredients");
            DropTable("dbo.OrderIngredients");
            CreateIndex("dbo.FoodOrderCarts", "Cart_Id");
            CreateIndex("dbo.FoodOrderCarts", "FoodOrder_Id");
            AddForeignKey("dbo.FoodOrderCarts", "Cart_Id", "dbo.Carts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FoodOrderCarts", "FoodOrder_Id", "dbo.FoodOrders", "Id", cascadeDelete: true);
        }
    }
}
