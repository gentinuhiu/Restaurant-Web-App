namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminclientdeletebool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "AdminDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Carts", "ClientDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "ClientDeleted");
            DropColumn("dbo.Carts", "AdminDeleted");
        }
    }
}
