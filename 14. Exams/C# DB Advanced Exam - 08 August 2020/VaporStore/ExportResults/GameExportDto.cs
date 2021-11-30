﻿namespace VaporStore.ExportResults
{
    using System.Xml.Serialization;

    [XmlType("Game")]
    public class GameExportDto
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlElement("Genre")]
        public string Genre { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}
