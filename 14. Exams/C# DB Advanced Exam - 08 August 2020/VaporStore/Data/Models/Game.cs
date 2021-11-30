namespace VaporStore.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        public Game()
        {
            this.Purchases = new List<Purchase>();
            this.GameTags = new List<GameTag>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int DeveloperId { get; set; }

        public Developer Developer { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        public ICollection<GameTag> GameTags { get; set; }
    }
}
