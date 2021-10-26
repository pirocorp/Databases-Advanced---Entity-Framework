﻿namespace MusicHub.Data.Models
{
    using System.Collections.Generic;

    public class Producer
    {
        public Producer()
        {
            this.Albums = new HashSet<Album>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Pseudonym { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<Album> Albums { get; set; }
    }
}
