namespace ProductShop.App.Dtos.Alternative
{
    using System.Xml.Serialization;

    [XmlRoot("users")]
    public class CountDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("user")]
        public AlternativeUserDto[] Users { get; set; }
    }
}