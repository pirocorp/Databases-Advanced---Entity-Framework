namespace MusicHub.Data.Models
{
    using System.Collections.Generic;

    public class Writer
    {
        public Writer()
        {
            this.Songs = new HashSet<Song>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Pseudonym { get; set; }

        public IEnumerable<Song> Songs { get; set; }
    }
}
