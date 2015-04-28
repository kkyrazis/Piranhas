namespace Piranhas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Articles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleModels",
                c => new
                    {
                        ArticleModelID = c.Int(nullable: false, identity: true),
                        ArticleTitle = c.String(),
                        ArticleData = c.String(),
                    })
                .PrimaryKey(t => t.ArticleModelID);
            
            CreateTable(
                "dbo.FilePaths",
                c => new
                    {
                        FilePathID = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileTitle = c.String(maxLength: 255),
                        ArticleModelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FilePathID)
                .ForeignKey("dbo.ArticleModels", t => t.ArticleModelID, cascadeDelete: true)
                .Index(t => t.ArticleModelID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilePaths", "ArticleModelID", "dbo.ArticleModels");
            DropIndex("dbo.FilePaths", new[] { "ArticleModelID" });
            DropTable("dbo.FilePaths");
            DropTable("dbo.ArticleModels");
        }
    }
}
