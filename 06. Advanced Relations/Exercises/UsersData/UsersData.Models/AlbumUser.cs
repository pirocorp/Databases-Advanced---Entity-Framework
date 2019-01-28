namespace UsersData.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AlbumUser
    {
        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}