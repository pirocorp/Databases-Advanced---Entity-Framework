﻿namespace UsersData.Models
{
    using System.Collections.Generic;

    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<UserFriend> Friends { get; set; }

        public ICollection<AlbumUser> Albums { get; set; }
    }
}