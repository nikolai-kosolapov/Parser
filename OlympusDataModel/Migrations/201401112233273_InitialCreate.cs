namespace OlympusDataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Title = c.String(),
                        Provider_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Providers", t => t.Provider_Id)
                .Index(t => t.Provider_Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        Url = c.String(),
                        ImageUrl = c.String(),
                        HeaderText = c.String(),
                        ShortText = c.String(),
                        UniqueKey = c.Int(nullable: false),
                        Provider_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Providers", t => t.Provider_Id)
                .Index(t => t.Provider_Id);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        HasApi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsCategories",
                c => new
                    {
                        News_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.News_Id, t.Category_Id })
                .ForeignKey("dbo.News", t => t.News_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.News_Id)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "Provider_Id", "dbo.Providers");
            DropForeignKey("dbo.Categories", "Provider_Id", "dbo.Providers");
            DropForeignKey("dbo.NewsCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.NewsCategories", "News_Id", "dbo.News");
            DropIndex("dbo.News", new[] { "Provider_Id" });
            DropIndex("dbo.Categories", new[] { "Provider_Id" });
            DropIndex("dbo.NewsCategories", new[] { "Category_Id" });
            DropIndex("dbo.NewsCategories", new[] { "News_Id" });
            DropTable("dbo.NewsCategories");
            DropTable("dbo.Providers");
            DropTable("dbo.News");
            DropTable("dbo.Categories");
        }
    }
}
