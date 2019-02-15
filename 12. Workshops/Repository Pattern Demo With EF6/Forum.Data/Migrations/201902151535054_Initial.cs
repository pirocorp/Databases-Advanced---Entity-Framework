namespace Forum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.ParentCategoryId)
                .Index(t => t.ParentCategoryId);

            this.CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        PostType = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);

            this.CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        AnsweredOn = c.DateTime(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);

            this.CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Post_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Post_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Post_Id);
            
        }
        
        public override void Down()
        {
            this.DropForeignKey("dbo.TagPosts", "Post_Id", "dbo.Posts");
            this.DropForeignKey("dbo.TagPosts", "Tag_Id", "dbo.Tags");
            this.DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            this.DropForeignKey("dbo.Answers", "PostId", "dbo.Posts");
            this.DropForeignKey("dbo.Categories", "ParentCategoryId", "dbo.Categories");
            this.DropIndex("dbo.TagPosts", new[] { "Post_Id" });
            this.DropIndex("dbo.TagPosts", new[] { "Tag_Id" });
            this.DropIndex("dbo.Answers", new[] { "PostId" });
            this.DropIndex("dbo.Posts", new[] { "CategoryId" });
            this.DropIndex("dbo.Categories", new[] { "ParentCategoryId" });
            this.DropTable("dbo.TagPosts");
            this.DropTable("dbo.Tags");
            this.DropTable("dbo.Answers");
            this.DropTable("dbo.Posts");
            this.DropTable("dbo.Categories");
        }
    }
}
