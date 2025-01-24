namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class error2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FoodCategories", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FoodCategories", "Name", c => c.String(nullable: false));
        }
    }
}
