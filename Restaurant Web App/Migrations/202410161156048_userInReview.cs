namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userInReview : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reviews", "UserId");
            AddForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Reviews", new[] { "UserId" });
            AlterColumn("dbo.Reviews", "UserId", c => c.String());
        }
    }
}
