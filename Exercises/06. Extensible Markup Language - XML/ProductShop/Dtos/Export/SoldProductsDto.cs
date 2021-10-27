namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    public class SoldProductsDto
    {
        [XmlElement("count")] 
        public int Count { get; set; }

        [XmlArray("products")]
        public SoldProductDto[] SoldProducts { get; set; }
    }
}
