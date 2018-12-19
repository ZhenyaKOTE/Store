namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddednewpropertytoProdcutDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProducts", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblProducts", "Description");
        }
    }
}
