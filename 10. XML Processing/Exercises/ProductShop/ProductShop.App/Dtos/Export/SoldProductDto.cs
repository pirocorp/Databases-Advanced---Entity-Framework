﻿namespace ProductShop.App.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("product")]
    public class SoldProductDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}