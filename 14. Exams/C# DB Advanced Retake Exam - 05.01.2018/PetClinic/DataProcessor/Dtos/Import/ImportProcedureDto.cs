namespace PetClinic.DataProcessor.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("Procedure")]
    public class ImportProcedureDto
    {
        [XmlElement("Vet")]
        public string Vet { get; set; }

        [XmlElement("Animal")]
        public string Animal { get; set; }

        [XmlElement("DateTime")]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public ImportProcedureAnimalAidDto[] AnimalAids { get; set; }
    }
}