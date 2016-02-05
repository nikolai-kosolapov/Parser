namespace OlympusDataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "Title2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Title2", c => c.String());
        }
    }
}
