namespace UsersData.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tag
    {
        public int TagId { get; set; }

        public string Text { get; set; }

        public ICollection<AlbumTag> Albums { get; set; }
    }
}