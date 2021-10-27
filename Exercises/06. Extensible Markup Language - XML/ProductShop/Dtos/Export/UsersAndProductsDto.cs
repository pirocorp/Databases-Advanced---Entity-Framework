namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlRoot("Users")]
    public class UsersAndProductsDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public UserWithSoldProductsDto[] UsersAndProducts { get; set; }
    }
}