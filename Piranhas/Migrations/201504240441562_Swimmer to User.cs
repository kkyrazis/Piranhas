namespace Piranhas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SwimmertoUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Swimmers", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Swimmers", "UserID");
        }
    }
}
