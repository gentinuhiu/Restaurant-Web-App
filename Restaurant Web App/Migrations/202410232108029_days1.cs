namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class days1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Days", "StartTime2", c => c.String());
            AddColumn("dbo.Days", "EndTime2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Days", "EndTime2");
            DropColumn("dbo.Days", "StartTime2");
        }
    }
}
