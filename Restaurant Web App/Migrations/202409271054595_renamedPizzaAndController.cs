namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamedPizzaAndController : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Pizzas", newName: "Foods");
            RenameTable(name: "dbo.IngredientPizzas", newName: "IngredientFoods");
            RenameTable(name: "dbo.NoIngredientPizzas", newName: "NoIngredientFoods");
            RenameTable(name: "dbo.OptionPizzas", newName: "OptionFoods");
            RenameColumn(table: "dbo.IngredientFoods", name: "Pizza_Id", newName: "Food_Id");
            RenameColumn(table: "dbo.NoIngredientFoods", name: "Pizza_Id", newName: "Food_Id");
            RenameColumn(table: "dbo.OptionFoods", name: "Pizza_Id", newName: "Food_Id");
            RenameIndex(table: "dbo.IngredientFoods", name: "IX_Pizza_Id", newName: "IX_Food_Id");
            RenameIndex(table: "dbo.NoIngredientFoods", name: "IX_Pizza_Id", newName: "IX_Food_Id");
            RenameIndex(table: "dbo.OptionFoods", name: "IX_Pizza_Id", newName: "IX_Food_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.OptionFoods", name: "IX_Food_Id", newName: "IX_Pizza_Id");
            RenameIndex(table: "dbo.NoIngredientFoods", name: "IX_Food_Id", newName: "IX_Pizza_Id");
            RenameIndex(table: "dbo.IngredientFoods", name: "IX_Food_Id", newName: "IX_Pizza_Id");
            RenameColumn(table: "dbo.OptionFoods", name: "Food_Id", newName: "Pizza_Id");
            RenameColumn(table: "dbo.NoIngredientFoods", name: "Food_Id", newName: "Pizza_Id");
            RenameColumn(table: "dbo.IngredientFoods", name: "Food_Id", newName: "Pizza_Id");
            RenameTable(name: "dbo.OptionFoods", newName: "OptionPizzas");
            RenameTable(name: "dbo.NoIngredientFoods", newName: "NoIngredientPizzas");
            RenameTable(name: "dbo.IngredientFoods", newName: "IngredientPizzas");
            RenameTable(name: "dbo.Foods", newName: "Pizzas");
        }
    }
}
