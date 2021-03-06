﻿namespace UsersData.Models
{
    using System.Collections.Generic;
    using Enums;

    public class Album
    {
        public int AlbumId { get; set; }

        public string Name { get; set; }

        public string BackgroundColor { get; set; }

        public AccessModificator Access { get; set; }

        public ICollection<AlbumUser> Users { get; set; }

        public ICollection<AlbumPicture> Pictures { get; set; }

        public ICollection<AlbumTag> Tags { get; set; }
    }
}