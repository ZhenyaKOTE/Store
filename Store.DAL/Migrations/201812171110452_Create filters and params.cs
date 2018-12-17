namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Createfiltersandparams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.vFilterNameGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FilterNameId = c.Int(nullable: false),
                        FilterName = c.String(nullable: false, maxLength: 250),
                        FilterValueId = c.Int(),
                        FilterValue = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.vFilterNameGroups");
        }
    }
}
