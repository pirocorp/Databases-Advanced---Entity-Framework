namespace PhotoShare.Models
{
    using System.Collections.Generic;

    using Enums;

    public class Album
    {
        private ICollection<AlbumRole> albumRoles;
        private ICollection<Picture> pictures;
        private ICollection<AlbumTag> albumTags;

        public Album()
        {
            this.pictures = new HashSet<Picture>();
            this.albumTags = new HashSet<AlbumTag>();
            this.albumRoles = new HashSet<AlbumRole>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Color? BackgroundColor { get; set; }

        public bool IsPublic { get; set; }

        public virtual ICollection<AlbumRole> AlbumRoles
        {
            get => this.albumRoles;
            set => this.albumRoles = value;
        }

        public virtual ICollection<Picture> Pictures
        {
            get => this.pictures;
            set => this.pictures = value;
        }

        public virtual ICollection<AlbumTag> AlbumTags
        {
            get => this.albumTags;
            set => this.albumTags = value;
        }
    }
}
