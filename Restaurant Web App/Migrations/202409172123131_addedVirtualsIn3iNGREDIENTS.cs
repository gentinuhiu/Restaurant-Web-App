namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedVirtualsIn3iNGREDIENTS : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ingredients", "PizzaOrder_Id", "dbo.PizzaOrders");
            DropForeignKey("dbo.NoIngredients", "PizzaOrder_Id", "dbo.PizzaOrders");
            DropForeignKey("dbo.Options", "PizzaOrder_Id", "dbo.PizzaOrders");
            DropIndex("dbo.Ingredients", new[] { "PizzaOrder_Id" });
            DropIndex("dbo.NoIngredients", new[] { "PizzaOrder_Id" });
            DropIndex("dbo.Options", new[] { "PizzaOrder_Id" });
            CreateTable(
                "dbo.NoIngredientPizzaOrders",
                c => new
                    {
                        NoIngredient_Id = c.Int(nullable: false),
                        PizzaOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NoIngredient_Id, t.PizzaOrder_Id })
                .ForeignKey("dbo.NoIngredients", t => t.NoIngredient_Id, cascadeDelete: true)
                .ForeignKey("dbo.PizzaOrders", t => t.PizzaOrder_Id, cascadeDelete: true)
                .Index(t => t.NoIngredient_Id)
                .Index(t => t.PizzaOrder_Id);
            
            CreateTable(
                "dbo.OptionPizzaOrders",
                c => new
                    {
                        Option_Id = c.Int(nullable: false),
                        PizzaOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Option_Id, t.PizzaOrder_Id })
                .ForeignKey("dbo.Options", t => t.Option_Id, cascadeDelete: true)
                .ForeignKey("dbo.PizzaOrders", t => t.PizzaOrder_Id, cascadeDelete: true)
                .Index(t => t.Option_Id)
                .Index(t => t.PizzaOrder_Id);
            
            CreateTable(
                "dbo.PizzaOrderIngredients",
                c => new
                    {
                        PizzaOrder_Id = c.Int(nullable: false),
                        Ingredient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PizzaOrder_Id, t.Ingredient_Id })
                .ForeignKey("dbo.PizzaOrders", t => t.PizzaOrder_Id, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_Id, cascadeDelete: true)
                .Index(t => t.PizzaOrder_Id)
                .Index(t => t.Ingredient_Id);
            
            DropColumn("dbo.Ingredients", "PizzaOrder_Id");
            DropColumn("dbo.NoIngredients", "PizzaOrder_Id");
            DropColumn("dbo.Options", "PizzaOrder_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Options", "PizzaOrder_Id", c => c.Int());
            AddColumn("dbo.NoIngredients", "PizzaOrder_Id", c => c.Int());
            AddColumn("dbo.Ingredients", "PizzaOrder_Id", c => c.Int());
            DropForeignKey("dbo.PizzaOrderIngredients", "Ingredient_Id", "dbo.Ingredients");
            DropForeignKey("dbo.PizzaOrderIngredients", "PizzaOrder_Id", "dbo.PizzaOrders");
            DropForeignKey("dbo.OptionPizzaOrders", "PizzaOrder_Id", "dbo.PizzaOrders");
            DropForeignKey("dbo.OptionPizzaOrders", "Option_Id", "dbo.Options");
            DropForeignKey("dbo.NoIngredientPizzaOrders", "PizzaOrder_Id", "dbo.PizzaOrders");
            DropForeignKey("dbo.NoIngredientPizzaOrders", "NoIngredient_Id", "dbo.NoIngredients");
            DropIndex("dbo.PizzaOrderIngredients", new[] { "Ingredient_Id" });
            DropIndex("dbo.PizzaOrderIngredients", new[] { "PizzaOrder_Id" });
            DropIndex("dbo.OptionPizzaOrders", new[] { "PizzaOrder_Id" });
            DropIndex("dbo.OptionPizzaOrders", new[] { "Option_Id" });
            DropIndex("dbo.NoIngredientPizzaOrders", new[] { "PizzaOrder_Id" });
            DropIndex("dbo.NoIngredientPizzaOrders", new[] { "NoIngredient_Id" });
            DropTable("dbo.PizzaOrderIngredients");
            DropTable("dbo.OptionPizzaOrders");
            DropTable("dbo.NoIngredientPizzaOrders");
            CreateIndex("dbo.Options", "PizzaOrder_Id");
            CreateIndex("dbo.NoIngredients", "PizzaOrder_Id");
            CreateIndex("dbo.Ingredients", "PizzaOrder_Id");
            AddForeignKey("dbo.Options", "PizzaOrder_Id", "dbo.PizzaOrders", "Id");
            AddForeignKey("dbo.NoIngredients", "PizzaOrder_Id", "dbo.PizzaOrders", "Id");
            AddForeignKey("dbo.Ingredients", "PizzaOrder_Id", "dbo.PizzaOrders", "Id");
        }
    }
}
