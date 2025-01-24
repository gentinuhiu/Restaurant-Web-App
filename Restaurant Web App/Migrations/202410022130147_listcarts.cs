namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listcarts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FoodOrders", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.FoodOrders", new[] { "Cart_Id" });
            CreateTable(
                "dbo.FoodOrderCarts",
                c => new
                    {
                        FoodOrder_Id = c.Int(nullable: false),
                        Cart_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FoodOrder_Id, t.Cart_Id })
                .ForeignKey("dbo.FoodOrders", t => t.FoodOrder_Id, cascadeDelete: true)
                .ForeignKey("dbo.Carts", t => t.Cart_Id, cascadeDelete: true)
                .Index(t => t.FoodOrder_Id)
                .Index(t => t.Cart_Id);
            
            DropColumn("dbo.FoodOrders", "Cart_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodOrders", "Cart_Id", c => c.Int());
            DropForeignKey("dbo.FoodOrderCarts", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.FoodOrderCarts", "FoodOrder_Id", "dbo.FoodOrders");
            DropIndex("dbo.FoodOrderCarts", new[] { "Cart_Id" });
            DropIndex("dbo.FoodOrderCarts", new[] { "FoodOrder_Id" });
            DropTable("dbo.FoodOrderCarts");
            CreateIndex("dbo.FoodOrders", "Cart_Id");
            AddForeignKey("dbo.FoodOrders", "Cart_Id", "dbo.Carts", "Id");
        }
    }
}
