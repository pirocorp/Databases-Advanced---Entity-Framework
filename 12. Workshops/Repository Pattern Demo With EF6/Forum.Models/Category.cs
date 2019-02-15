namespace Forum.Models
{
    using System.Collections.Generic;

    public class Category
    {
        private ICollection<Post> posts;

        public Category()
        {
            this.posts = new HashSet<Post>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Post> Posts
        {
            get => this.posts;
            set => this.posts = value;
        }
    }
}