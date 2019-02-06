namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Person : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Person", "test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "test", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
