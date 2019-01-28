namespace BookShopSystem.Models
{
    using System.Collections.Generic;
    using Enums;

    public class Book
    {
        public Book()
        {
            this.Categories = new HashSet<CategoryBook>();
            this.RelatedBooks = new HashSet<BookRelatedBook>();
        }

        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public EditionType Edition { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public ICollection<CategoryBook> Categories { get; set; }

        public ICollection<BookRelatedBook> RelatedBooks { get; set; }
    }
}
