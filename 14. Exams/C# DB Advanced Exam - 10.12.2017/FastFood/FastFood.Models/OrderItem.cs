namespace FastFood.Models
{
    using System.ComponentModel.DataAnnotations;

    public class OrderItem
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}