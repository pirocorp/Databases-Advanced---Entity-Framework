namespace BookShopSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BookRelatedBook
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int RelatedId { get; set; }
        public Book RelatedBook { get; set; }
    }
}