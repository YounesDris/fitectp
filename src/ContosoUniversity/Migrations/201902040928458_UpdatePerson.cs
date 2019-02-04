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
            AddColumn("dbo.Person", "ConfirmPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "ConfirmPassword");
            DropColumn("dbo.Person", "Password");
            DropColumn("dbo.Person", "UserName");
        }
    }
}
