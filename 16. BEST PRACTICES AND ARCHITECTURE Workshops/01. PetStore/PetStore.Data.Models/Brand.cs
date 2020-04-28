namespace PetStore.Data.Models
{
    using System.Collections.Generic;

    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Toy> Toys { get; set; } = new HashSet<Toy>();

        public ICollection<Food> Foods { get; set; } = new HashSet<Food>();
    }
}
