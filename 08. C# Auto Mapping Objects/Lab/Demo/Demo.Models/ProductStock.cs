namespace Demo.Models
{
    public class ProductStock
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int StorageId { get; set; }
        public Storage Storage { get; set; }

        public int Quantity { get; set; }
    }
}