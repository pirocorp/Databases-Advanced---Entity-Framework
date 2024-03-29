﻿namespace CarDealer.App.Dtos.CarsWithPartsDtos
{
    using System.Xml.Serialization;

    [XmlType("car")]
    public class CarWithTheirPartsDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public PartDto[] Parts { get; set; }
    }
}