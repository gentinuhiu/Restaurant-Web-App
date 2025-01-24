namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryClassAndRelationBetweenFoodAndFoodCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FoodCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        imageUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Foods", "FoodCategory_Id", c => c.Int());
            CreateIndex("dbo.Foods", "FoodCategory_Id");
            AddForeignKey("dbo.Foods", "FoodCategory_Id", "dbo.FoodCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Foods", "FoodCategory_Id", "dbo.FoodCategories");
            DropIndex("dbo.Foods", new[] { "FoodCategory_Id" });
            DropColumn("dbo.Foods", "FoodCategory_Id");
            DropTable("dbo.FoodCategories");
        }
    }
}
