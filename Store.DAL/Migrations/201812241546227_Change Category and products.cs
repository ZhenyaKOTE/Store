namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCategoryandproducts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblCategories", "ParentId", "dbo.tblCategories");
            DropIndex("dbo.tblCategories", new[] { "ParentId" });
            AddColumn("dbo.tblProducts", "Description", c => c.String());
            DropColumn("dbo.tblCategories", "ParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblCategories", "ParentId", c => c.Int());
            DropColumn("dbo.tblProducts", "Description");
            CreateIndex("dbo.tblCategories", "ParentId");
            AddForeignKey("dbo.tblCategories", "ParentId", "dbo.tblCategories", "Id");
        }
    }
}
