namespace Piranhas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StrokePreferences",
                c => new
                    {
                        StrokePreferenceID = c.Int(nullable: false, identity: true),
                        Butterfly = c.Boolean(nullable: false),
                        Backstroke = c.Boolean(nullable: false),
                        Breaststroke = c.Boolean(nullable: false),
                        Freestyle = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StrokePreferenceID);
            
            CreateTable(
                "dbo.Swimmers",
                c => new
                    {
                        SwimmerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        StrokePreferenceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SwimmerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Swimmers");
            DropTable("dbo.StrokePreferences");
        }
    }
}
