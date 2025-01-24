namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringUserId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PizzaOrders", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PizzaOrders", "UserId", c => c.Int(nullable: false));
        }
    }
}
