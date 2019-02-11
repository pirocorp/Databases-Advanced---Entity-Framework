namespace ProductShop.App.Dtos.Alternative
{
    using System.Xml.Serialization;

    [XmlType("product")]
    public class ProductAlternativeDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}