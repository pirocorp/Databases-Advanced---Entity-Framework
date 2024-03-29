﻿namespace CarDealer.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("customer")]
    public class TotalSalesByCustomerDto
    {
        [XmlAttribute("full-name")]
        public string Name { get; set; }

        [XmlAttribute("bought-cars")]
        public int BoughtCars { get; set; }

        [XmlAttribute("spent-money")]
        public decimal SpendMoney { get; set; }
    }
}
