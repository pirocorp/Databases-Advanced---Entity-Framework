namespace ProductShop.App.Dtos.Alternative
{
    using System.Xml.Serialization;

    [XmlType("sold-products")]
    public class SoldProductsDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public ProductAlternativeDto[] Products { get; set; }
    }
}