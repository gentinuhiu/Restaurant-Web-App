namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFoodOrder : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IngredientPizzaOrders", newName: "PizzaOrderIngredients");
            DropPrimaryKey("dbo.PizzaOrderIngredients");
            CreateTable(
                "dbo.FoodOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        FoodId = c.Int(nullable: false),
                        SelectedSize = c.String(),
                        FoodPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .ForeignKey("dbo.Foods", t => t.FoodId, cascadeDelete: true)
                .Index(t => t.FoodId)
                .Index(t => t.Cart_Id);
            
            CreateTable(
                "dbo.IngredientFoodOrders",
                c => new
                    {
                        Ingredient_Id = c.Int(nullable: false),
                        FoodOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ingredient_Id, t.FoodOrder_Id })
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_Id, cascadeDelete: true)
                .ForeignKey("dbo.FoodOrders", t => t.FoodOrder_Id, cascadeDelete: true)
                .Index(t => t.Ingredient_Id)
                .Index(t => t.FoodOrder_Id);
            
            CreateTable(
                "dbo.NoIngredientFoodOrders",
                c => new
                    {
                        NoIngredient_Id = c.Int(nullable: false),
                        FoodOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NoIngredient_Id, t.FoodOrder_Id })
                .ForeignKey("dbo.NoIngredients", t => t.NoIngredient_Id, cascadeDelete: true)
                .ForeignKey("dbo.FoodOrders", t => t.FoodOrder_Id, cascadeDelete: true)
                .Index(t => t.NoIngredient_Id)
                .Index(t => t.FoodOrder_Id);
            
            CreateTable(
                "dbo.OptionFoodOrders",
                c => new
                    {
                        Option_Id = c.Int(nullable: false),
                        FoodOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Option_Id, t.FoodOrder_Id })
                .ForeignKey("dbo.Options", t => t.Option_Id, cascadeDelete: true)
                .ForeignKey("dbo.FoodOrders", t => t.FoodOrder_Id, cascadeDelete: true)
                .Index(t => t.Option_Id)
                .Index(t => t.FoodOrder_Id);
            
            AddPrimaryKey("dbo.PizzaOrderIngredients", new[] { "PizzaOrder_Id", "Ingredient_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FoodOrders", "FoodId", "dbo.Foods");
            DropForeignKey("dbo.OptionFoodOrders", "FoodOrder_Id", "dbo.FoodOrders");
            DropForeignKey("dbo.OptionFoodOrders", "Option_Id", "dbo.Options");
            DropForeignKey("dbo.NoIngredientFoodOrders", "FoodOrder_Id", "dbo.FoodOrders");
            DropForeignKey("dbo.NoIngredientFoodOrders", "NoIngredient_Id", "dbo.NoIngredients");
            DropForeignKey("dbo.IngredientFoodOrders", "FoodOrder_Id", "dbo.FoodOrders");
            DropForeignKey("dbo.IngredientFoodOrders", "Ingredient_Id", "dbo.Ingredients");
            DropForeignKey("dbo.FoodOrders", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.OptionFoodOrders", new[] { "FoodOrder_Id" });
            DropIndex("dbo.OptionFoodOrders", new[] { "Option_Id" });
            DropIndex("dbo.NoIngredientFoodOrders", new[] { "FoodOrder_Id" });
            DropIndex("dbo.NoIngredientFoodOrders", new[] { "NoIngredient_Id" });
            DropIndex("dbo.IngredientFoodOrders", new[] { "FoodOrder_Id" });
            DropIndex("dbo.IngredientFoodOrders", new[] { "Ingredient_Id" });
            DropIndex("dbo.FoodOrders", new[] { "Cart_Id" });
            DropIndex("dbo.FoodOrders", new[] { "FoodId" });
            DropPrimaryKey("dbo.PizzaOrderIngredients");
            DropTable("dbo.OptionFoodOrders");
            DropTable("dbo.NoIngredientFoodOrders");
            DropTable("dbo.IngredientFoodOrders");
            DropTable("dbo.FoodOrders");
            AddPrimaryKey("dbo.PizzaOrderIngredients", new[] { "Ingredient_Id", "PizzaOrder_Id" });
            RenameTable(name: "dbo.PizzaOrderIngredients", newName: "IngredientPizzaOrders");
        }
    }
}
