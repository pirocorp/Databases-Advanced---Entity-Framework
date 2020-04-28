namespace PetStore.Data.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Animal Category
    /// <remarks>
    /// each category represents different species
    /// </remarks>
    /// </summary>
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();

        public ICollection<Toy> Toys { get; set; } = new HashSet<Toy>();

        public ICollection<Food> Foods { get; set; } = new HashSet<Food>();
    }
}