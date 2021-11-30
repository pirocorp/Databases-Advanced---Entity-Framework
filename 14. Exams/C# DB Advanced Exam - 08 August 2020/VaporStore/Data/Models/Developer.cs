namespace VaporStore.Data.Models
{
    using System.Collections.Generic;

    public class Developer
    {
        public Developer()
        {
            this.Games = new List<Game>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
