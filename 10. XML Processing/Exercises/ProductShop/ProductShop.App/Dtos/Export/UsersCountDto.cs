namespace ProductShop.App.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlRoot("users")]
    public class UsersCountDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("user")]
        public UsersAndProductsDto[] UsersAndProducts { get; set; }
    }
}