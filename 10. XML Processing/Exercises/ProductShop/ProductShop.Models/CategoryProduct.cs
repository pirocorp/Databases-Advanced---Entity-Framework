namespace ProductShop.Models
{
    public class CategoryProduct
    {
        public CategoryProduct()
        {
        }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}