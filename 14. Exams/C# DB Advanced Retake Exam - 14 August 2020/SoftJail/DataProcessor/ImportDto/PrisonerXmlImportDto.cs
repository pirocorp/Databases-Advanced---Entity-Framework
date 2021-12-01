namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Prisoner")]
    public class PrisonerXmlImportDto
    {
        [XmlAttribute("id")]
        [Required]
        public int Id { get; set; }
    }
}
