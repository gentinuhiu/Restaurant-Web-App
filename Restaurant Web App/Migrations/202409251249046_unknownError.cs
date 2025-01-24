namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unknownError : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PizzaOrderIngredients", newName: "IngredientPizzaOrders");
            RenameTable(name: "dbo.PizzaIngredients", newName: "IngredientPizzas");
            RenameTable(name: "dbo.CartPizzaOrders", newName: "PizzaOrderCarts");
            DropPrimaryKey("dbo.IngredientPizzaOrders");
            DropPrimaryKey("dbo.IngredientPizzas");
            DropPrimaryKey("dbo.PizzaOrderCarts");
            AddPrimaryKey("dbo.IngredientPizzaOrders", new[] { "Ingredient_Id", "PizzaOrder_Id" });
            AddPrimaryKey("dbo.IngredientPizzas", new[] { "Ingredient_Id", "Pizza_Id" });
            AddPrimaryKey("dbo.PizzaOrderCarts", new[] { "PizzaOrder_Id", "Cart_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PizzaOrderCarts");
            DropPrimaryKey("dbo.IngredientPizzas");
            DropPrimaryKey("dbo.IngredientPizzaOrders");
            AddPrimaryKey("dbo.PizzaOrderCarts", new[] { "Cart_Id", "PizzaOrder_Id" });
            AddPrimaryKey("dbo.IngredientPizzas", new[] { "Pizza_Id", "Ingredient_Id" });
            AddPrimaryKey("dbo.IngredientPizzaOrders", new[] { "PizzaOrder_Id", "Ingredient_Id" });
            RenameTable(name: "dbo.PizzaOrderCarts", newName: "CartPizzaOrders");
            RenameTable(name: "dbo.IngredientPizzas", newName: "PizzaIngredients");
            RenameTable(name: "dbo.IngredientPizzaOrders", newName: "PizzaOrderIngredients");
        }
    }
}
