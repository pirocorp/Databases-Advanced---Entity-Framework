namespace CarDealer.Dtos.Export
{
    using System.Xml.Serialization;
    using Import;

    [XmlType("car")]
    public class CarWithTheirPartsDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("travelled-distance")]
        public long TravelledDistance { get; set; }
    }
}