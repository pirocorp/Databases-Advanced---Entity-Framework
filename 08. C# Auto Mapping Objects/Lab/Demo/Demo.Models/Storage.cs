namespace Demo.Models
{
    using System.Collections.Generic;

    public class Storage
    {
        public int StorageId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public ICollection<ProductStock> Products { get; set; }
    }
}