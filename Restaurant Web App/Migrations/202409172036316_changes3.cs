namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PizzaOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PizzaId = c.Int(nullable: false),
                        SelectedSize = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pizzas", t => t.PizzaId, cascadeDelete: true)
                .Index(t => t.PizzaId);
            
            AddColumn("dbo.Ingredients", "PizzaOrder_Id", c => c.Int());
            AddColumn("dbo.NoIngredients", "PizzaOrder_Id", c => c.Int());
            AddColumn("dbo.Options", "PizzaOrder_Id", c => c.Int());
            CreateIndex("dbo.Ingredients", "PizzaOrder_Id");
            CreateIndex("dbo.NoIngredients", "PizzaOrder_Id");
            CreateIndex("dbo.Options", "PizzaOrder_Id");
            AddForeignKey("dbo.Ingredients", "PizzaOrder_Id", "dbo.PizzaOrders", "Id");
            AddForeignKey("dbo.NoIngredients", "PizzaOrder_Id", "dbo.PizzaOrders", "Id");
            AddForeignKey("dbo.Options", "PizzaOrder_Id", "dbo.PizzaOrders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Options", "PizzaOrder_Id", "dbo.PizzaOrders");
            DropForeignKey("dbo.NoIngredients", "PizzaOrder_Id", "dbo.PizzaOrders");
            DropForeignKey("dbo.Ingredients", "PizzaOrder_Id", "dbo.PizzaOrders");
            DropForeignKey("dbo.PizzaOrders", "PizzaId", "dbo.Pizzas");
            DropIndex("dbo.PizzaOrders", new[] { "PizzaId" });
            DropIndex("dbo.Options", new[] { "PizzaOrder_Id" });
            DropIndex("dbo.NoIngredients", new[] { "PizzaOrder_Id" });
            DropIndex("dbo.Ingredients", new[] { "PizzaOrder_Id" });
            DropColumn("dbo.Options", "PizzaOrder_Id");
            DropColumn("dbo.NoIngredients", "PizzaOrder_Id");
            DropColumn("dbo.Ingredients", "PizzaOrder_Id");
            DropTable("dbo.PizzaOrders");
        }
    }
}
