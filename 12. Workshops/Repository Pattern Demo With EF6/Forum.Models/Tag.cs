namespace Forum.Models
{
    using System.Collections.Generic;

    public class Tag
    {
        private ICollection<Post> posts;

        public Tag()
        {
            this.posts = new HashSet<Post>();
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public virtual ICollection<Post> Posts
        {
            get => this.posts;
            set => this.posts = value;
        }
    }
}