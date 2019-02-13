namespace CarDealer.App.Dtos
{
    using System.Xml.Serialization;

    using Models;

    [XmlType("sale")]
    public class SaleDto
    {
        [XmlElement("car")]
        public virtual CareSaleDto Car { get; set; }

        [XmlElement("customer-name")]
        public string CustomerName { get; set; }

        [XmlElement("discount")]
        public int Discount { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("price-with-discount")]
        public decimal PriceWithDiscount { get; set; }
    }
}