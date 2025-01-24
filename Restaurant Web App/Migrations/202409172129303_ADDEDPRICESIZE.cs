namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDEDPRICESIZE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pizzas", "SelectedSize", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pizzas", "SelectedSize");
        }
    }
}
