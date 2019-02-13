namespace CarDealer.App.Dtos
{
    using System.Xml.Serialization;

    [XmlType("suplier")]
    public class LocalSupplier
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("parts-count")]
        public int PartsCount { get; set; }
    }
}