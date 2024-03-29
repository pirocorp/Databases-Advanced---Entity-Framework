﻿namespace ProductShop.App.Dtos.Alternative
{
    using System.Xml.Serialization;

    [XmlType("user")]
    public class AlternativeUserDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public string Age { get; set; }

        [XmlElement("sold-products")]
        public SoldProductsDto SoldProducts { get; set; }
    }
}