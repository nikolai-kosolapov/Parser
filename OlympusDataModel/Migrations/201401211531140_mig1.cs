namespace OlympusDataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Order", c => c.Int(nullable: false));
            DropColumn("dbo.News", "UniqueKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "UniqueKey", c => c.Int(nullable: false));
            DropColumn("dbo.News", "Order");
        }
    }
}
