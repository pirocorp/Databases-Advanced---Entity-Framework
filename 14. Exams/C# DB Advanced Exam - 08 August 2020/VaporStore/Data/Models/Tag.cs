namespace VaporStore.Data.Models
{
    using System.Collections.Generic;

    public class Tag
    {
        public Tag()
        {
            this.GameTags = new List<GameTag>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<GameTag> GameTags { get; set; }
    }
}
