namespace TeisterMask.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Common;

    using static Common.ValidationConstants;
    using static Common.ValidationConstants.Project;

    [XmlType("Project")]
    public class ProjectDto
    {
        [XmlElement("Name")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [XmlElement("OpenDate")]
        [DataType(DataType.DateTime)]
        [Required]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        [DataType(DataType.DateTime)]
        [EndDateValidation(nameof(OpenDate), nameof(DueDate), DateTimeFormat, true)]
        public string DueDate { get; set; }

        [XmlArray("Tasks")] 
        public TaskDto[] Tasks { get; set; }
    }
}
