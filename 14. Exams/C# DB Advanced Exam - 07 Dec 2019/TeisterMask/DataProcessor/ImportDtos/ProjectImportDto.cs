namespace TeisterMask.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Project")]
    public class ProjectImportDto
    {
        [Required]
        [StringLength(40, MinimumLength = 2)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [XmlArray("Tasks")]
        public TaskProjectImportDto[] Tasks { get; set; }
    }
}
