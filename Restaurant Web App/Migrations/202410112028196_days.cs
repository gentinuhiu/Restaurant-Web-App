namespace Restaurant_Web_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class days : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.String(),
                        EndTime = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Days");
        }
    }
}
