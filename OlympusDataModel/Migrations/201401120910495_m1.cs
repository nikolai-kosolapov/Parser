namespace OlympusDataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Title2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Title2");
        }
    }
}
