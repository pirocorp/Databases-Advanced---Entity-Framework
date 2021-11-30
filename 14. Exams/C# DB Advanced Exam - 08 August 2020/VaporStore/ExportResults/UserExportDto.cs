namespace VaporStore.ExportResults
{
    using System.Linq;
    using System.Xml.Serialization;

    [XmlType("User")]
    public class UserExportDto
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlArray("Purchases")] 
        public PurchaseExportDto[] Purchases { get; set; }

        [XmlElement("TotalSpent")] 
        public decimal TotalSpent
        {
            get => this.Purchases.Sum(p => p.Game.Price);
            set { }
        }
    }
}
