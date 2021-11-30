namespace VaporStore.ExportResults
{
    using System.Xml.Serialization;

    [XmlType("Purchase")]
    public class PurchaseExportDto
    {
        [XmlElement("Card")]
        public string Card { get; set; }

        [XmlElement("Cvc")]
        public string Cvc { get; set; }

        [XmlElement("Date")]
        public string Date { get; set; }

        [XmlElement("Game")]
        public GameExportDto Game { get; set; }
    }
}
