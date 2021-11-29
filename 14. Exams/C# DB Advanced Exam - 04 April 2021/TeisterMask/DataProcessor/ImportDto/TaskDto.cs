namespace TeisterMask.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Common;

    using static Common.ValidationConstants;
    using static Common.ValidationConstants.Task;

    [XmlType("Task")]
    public class TaskDto
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
        [Required]
        [EndDateValidation(nameof(OpenDate), nameof(DueDate), DateTimeFormat)]
        public string DueDate { get; set; }

        [XmlElement("ExecutionType")]
        public int ExecutionType { get; set; }

        [XmlElement("LabelType")]
        public int LabelType { get; set; }
    }
}
