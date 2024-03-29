﻿namespace ProductShop.App.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("user")]
    public class UsersAndProductsDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public string Age { get; set; }

        [XmlArray("sold-products")]
        public SoldProductDto[] SoldProducts { get; set; }
    }
}