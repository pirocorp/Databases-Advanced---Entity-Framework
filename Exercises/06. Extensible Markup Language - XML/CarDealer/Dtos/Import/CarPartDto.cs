﻿namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("partId")]
    public class CarPartDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
