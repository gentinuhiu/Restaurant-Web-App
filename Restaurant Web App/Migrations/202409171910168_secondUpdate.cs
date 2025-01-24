namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pizzas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        NormalPizzaPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SmallPizzaPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FamilyPizzaPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NoIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PizzaIngredients",
                c => new
                    {
                        Pizza_Id = c.Int(nullable: false),
                        Ingredient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pizza_Id, t.Ingredient_Id })
                .ForeignKey("dbo.Pizzas", t => t.Pizza_Id, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_Id, cascadeDelete: true)
                .Index(t => t.Pizza_Id)
                .Index(t => t.Ingredient_Id);
            
            CreateTable(
                "dbo.NoIngredientPizzas",
                c => new
                    {
                        NoIngredient_Id = c.Int(nullable: false),
                        Pizza_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NoIngredient_Id, t.Pizza_Id })
                .ForeignKey("dbo.NoIngredients", t => t.NoIngredient_Id, cascadeDelete: true)
                .ForeignKey("dbo.Pizzas", t => t.Pizza_Id, cascadeDelete: true)
                .Index(t => t.NoIngredient_Id)
                .Index(t => t.Pizza_Id);
            
            CreateTable(
                "dbo.OptionPizzas",
                c => new
                    {
                        Option_Id = c.Int(nullable: false),
                        Pizza_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Option_Id, t.Pizza_Id })
                .ForeignKey("dbo.Options", t => t.Option_Id, cascadeDelete: true)
                .ForeignKey("dbo.Pizzas", t => t.Pizza_Id, cascadeDelete: true)
                .Index(t => t.Option_Id)
                .Index(t => t.Pizza_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OptionPizzas", "Pizza_Id", "dbo.Pizzas");
            DropForeignKey("dbo.OptionPizzas", "Option_Id", "dbo.Options");
            DropForeignKey("dbo.NoIngredientPizzas", "Pizza_Id", "dbo.Pizzas");
            DropForeignKey("dbo.NoIngredientPizzas", "NoIngredient_Id", "dbo.NoIngredients");
            DropForeignKey("dbo.PizzaIngredients", "Ingredient_Id", "dbo.Ingredients");
            DropForeignKey("dbo.PizzaIngredients", "Pizza_Id", "dbo.Pizzas");
            DropIndex("dbo.OptionPizzas", new[] { "Pizza_Id" });
            DropIndex("dbo.OptionPizzas", new[] { "Option_Id" });
            DropIndex("dbo.NoIngredientPizzas", new[] { "Pizza_Id" });
            DropIndex("dbo.NoIngredientPizzas", new[] { "NoIngredient_Id" });
            DropIndex("dbo.PizzaIngredients", new[] { "Ingredient_Id" });
            DropIndex("dbo.PizzaIngredients", new[] { "Pizza_Id" });
            DropTable("dbo.OptionPizzas");
            DropTable("dbo.NoIngredientPizzas");
            DropTable("dbo.PizzaIngredients");
            DropTable("dbo.Options");
            DropTable("dbo.NoIngredients");
            DropTable("dbo.Pizzas");
            DropTable("dbo.Ingredients");
        }
    }
}
