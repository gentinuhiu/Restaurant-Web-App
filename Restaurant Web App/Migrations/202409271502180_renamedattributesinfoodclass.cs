namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamedattributesinfoodclass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Foods", "NormalSizedPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Foods", "SmallSizedPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Foods", "FamilySizedPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Foods", "NormalPizzaPrice");
            DropColumn("dbo.Foods", "SmallPizzaPrice");
            DropColumn("dbo.Foods", "FamilyPizzaPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "FamilyPizzaPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Foods", "SmallPizzaPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Foods", "NormalPizzaPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Foods", "FamilySizedPrice");
            DropColumn("dbo.Foods", "SmallSizedPrice");
            DropColumn("dbo.Foods", "NormalSizedPrice");
        }
    }
}
