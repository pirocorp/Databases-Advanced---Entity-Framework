namespace BookShopSystem.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public Category()
        {
            this.Books = new HashSet<CategoryBook>();
        }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public ICollection<CategoryBook> Books { get; set; }
    }
}