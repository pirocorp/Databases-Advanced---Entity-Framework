namespace Forum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            this.DropIndex("dbo.Categories", new[] { "ParentCategoryId" });
            this.AlterColumn("dbo.Categories", "ParentCategoryId", c => c.Int());
            this.CreateIndex("dbo.Categories", "ParentCategoryId");
        }
        
        public override void Down()
        {
            this.DropIndex("dbo.Categories", new[] { "ParentCategoryId" });
            this.AlterColumn("dbo.Categories", "ParentCategoryId", c => c.Int(nullable: false));
            this.CreateIndex("dbo.Categories", "ParentCategoryId");
        }
    }
}
