﻿namespace CarDealer.Initializer.Dtos
{
    using System.Xml.Serialization;

    [XmlType("supplier")]
    public class SupplierDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("is-importer")]
        public string IsImporter { get; set; }
    }
}