namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviewRange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Comment", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Comment", c => c.String());
        }
    }
}
