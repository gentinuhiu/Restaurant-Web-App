namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolscart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "isOrdered", c => c.Boolean(nullable: false));
            AddColumn("dbo.Carts", "isPrepared", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "isPrepared");
            DropColumn("dbo.Carts", "isOrdered");
        }
    }
}
