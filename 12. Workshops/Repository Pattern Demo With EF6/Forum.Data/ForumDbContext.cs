namespace Forum.Data
{
    using System.Data.Entity;

    using Models;

    public class ForumDbContext : DbContext
    {
        public ForumDbContext()
            :base("Forum")
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostAnswer> PostAnswers { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
