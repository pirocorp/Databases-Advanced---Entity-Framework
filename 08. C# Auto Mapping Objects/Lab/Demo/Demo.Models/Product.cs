namespace Demo.Models
{
    using System.Collections.Generic;

    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<ProductStock> Storages { get; set; }
    }
}