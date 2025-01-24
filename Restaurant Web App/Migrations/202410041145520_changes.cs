namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderIngredientFoodOrders", "OrderIngredient_Id", "dbo.OrderIngredients");
            DropForeignKey("dbo.OrderIngredientFoodOrders", "FoodOrder_Id", "dbo.FoodOrders");
            DropForeignKey("dbo.OrderNoIngredientFoodOrders", "OrderNoIngredient_Id", "dbo.OrderNoIngredients");
            DropForeignKey("dbo.OrderNoIngredientFoodOrders", "FoodOrder_Id", "dbo.FoodOrders");
            DropForeignKey("dbo.OrderOptionFoodOrders", "OrderOption_Id", "dbo.OrderOptions");
            DropForeignKey("dbo.OrderOptionFoodOrders", "FoodOrder_Id", "dbo.FoodOrders");
            DropIndex("dbo.OrderIngredientFoodOrders", new[] { "OrderIngredient_Id" });
            DropIndex("dbo.OrderIngredientFoodOrders", new[] { "FoodOrder_Id" });
            DropIndex("dbo.OrderNoIngredientFoodOrders", new[] { "OrderNoIngredient_Id" });
            DropIndex("dbo.OrderNoIngredientFoodOrders", new[] { "FoodOrder_Id" });
            DropIndex("dbo.OrderOptionFoodOrders", new[] { "OrderOption_Id" });
            DropIndex("dbo.OrderOptionFoodOrders", new[] { "FoodOrder_Id" });
            AddColumn("dbo.FoodOrders", "OrderIngredient_Id", c => c.Int());
            AddColumn("dbo.FoodOrders", "OrderNoIngredient_Id", c => c.Int());
            AddColumn("dbo.FoodOrders", "OrderOption_Id", c => c.Int());
            CreateIndex("dbo.FoodOrders", "OrderIngredient_Id");
            CreateIndex("dbo.FoodOrders", "OrderNoIngredient_Id");
            CreateIndex("dbo.FoodOrders", "OrderOption_Id");
            AddForeignKey("dbo.FoodOrders", "OrderIngredient_Id", "dbo.OrderIngredients", "Id");
            AddForeignKey("dbo.FoodOrders", "OrderNoIngredient_Id", "dbo.OrderNoIngredients", "Id");
            AddForeignKey("dbo.FoodOrders", "OrderOption_Id", "dbo.OrderOptions", "Id");
            DropTable("dbo.OrderIngredientFoodOrders");
            DropTable("dbo.OrderNoIngredientFoodOrders");
            DropTable("dbo.OrderOptionFoodOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderOptionFoodOrders",
                c => new
                    {
                        OrderOption_Id = c.Int(nullable: false),
                        FoodOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderOption_Id, t.FoodOrder_Id });
            
            CreateTable(
                "dbo.OrderNoIngredientFoodOrders",
                c => new
                    {
                        OrderNoIngredient_Id = c.Int(nullable: false),
                        FoodOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderNoIngredient_Id, t.FoodOrder_Id });
            
            CreateTable(
                "dbo.OrderIngredientFoodOrders",
                c => new
                    {
                        OrderIngredient_Id = c.Int(nullable: false),
                        FoodOrder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderIngredient_Id, t.FoodOrder_Id });
            
            DropForeignKey("dbo.FoodOrders", "OrderOption_Id", "dbo.OrderOptions");
            DropForeignKey("dbo.FoodOrders", "OrderNoIngredient_Id", "dbo.OrderNoIngredients");
            DropForeignKey("dbo.FoodOrders", "OrderIngredient_Id", "dbo.OrderIngredients");
            DropIndex("dbo.FoodOrders", new[] { "OrderOption_Id" });
            DropIndex("dbo.FoodOrders", new[] { "OrderNoIngredient_Id" });
            DropIndex("dbo.FoodOrders", new[] { "OrderIngredient_Id" });
            DropColumn("dbo.FoodOrders", "OrderOption_Id");
            DropColumn("dbo.FoodOrders", "OrderNoIngredient_Id");
            DropColumn("dbo.FoodOrders", "OrderIngredient_Id");
            CreateIndex("dbo.OrderOptionFoodOrders", "FoodOrder_Id");
            CreateIndex("dbo.OrderOptionFoodOrders", "OrderOption_Id");
            CreateIndex("dbo.OrderNoIngredientFoodOrders", "FoodOrder_Id");
            CreateIndex("dbo.OrderNoIngredientFoodOrders", "OrderNoIngredient_Id");
            CreateIndex("dbo.OrderIngredientFoodOrders", "FoodOrder_Id");
            CreateIndex("dbo.OrderIngredientFoodOrders", "OrderIngredient_Id");
            AddForeignKey("dbo.OrderOptionFoodOrders", "FoodOrder_Id", "dbo.FoodOrders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderOptionFoodOrders", "OrderOption_Id", "dbo.OrderOptions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderNoIngredientFoodOrders", "FoodOrder_Id", "dbo.FoodOrders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderNoIngredientFoodOrders", "OrderNoIngredient_Id", "dbo.OrderNoIngredients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderIngredientFoodOrders", "FoodOrder_Id", "dbo.FoodOrders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderIngredientFoodOrders", "OrderIngredient_Id", "dbo.OrderIngredients", "Id", cascadeDelete: true);
        }
    }
}
