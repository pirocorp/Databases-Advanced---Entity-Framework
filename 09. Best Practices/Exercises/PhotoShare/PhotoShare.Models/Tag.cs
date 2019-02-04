namespace PhotoShare.Models
{
    using System.Collections.Generic;

    public class Tag
    {
        private ICollection<AlbumTag> albumTags;

        public Tag()
        {
            this.albumTags = new HashSet<AlbumTag>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AlbumTag> AlbumTags
        {
            get => this.albumTags;
            set => this.albumTags = value;
        }
    }
}
