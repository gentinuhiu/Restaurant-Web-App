namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class times : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "OrderedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Carts", "PreparedTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "PreparedTime");
            DropColumn("dbo.Carts", "OrderedTime");
        }
    }
}
