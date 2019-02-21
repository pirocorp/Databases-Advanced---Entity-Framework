namespace FastFood.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        private ICollection<Item> items;

        public Category()
        {
            this.items = new HashSet<Item>();
        }

        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Item> Items
        {
            get => this.items;
            set => this.items = value;
        }
    }
}