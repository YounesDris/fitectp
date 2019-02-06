namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "UserName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Person", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "UserName");
            DropColumn("dbo.Person", "Password");
        }
    }
}
