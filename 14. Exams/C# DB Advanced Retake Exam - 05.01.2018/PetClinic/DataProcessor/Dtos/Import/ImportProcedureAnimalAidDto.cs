namespace PetClinic.DataProcessor.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("AnimalAid")]
    public class ImportProcedureAnimalAidDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }
    }
}