namespace UsersData.Models
{
    using System.Collections.Generic;

    using Attributes;

    public class Tag
    {
        public int TagId { get; set; }

        [Tag]
        public string Text { get; set; }

        public ICollection<AlbumTag> Albums { get; set; }
    }
}