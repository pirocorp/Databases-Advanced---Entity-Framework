namespace TeisterMask.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Project")]
    public class ProjectExportDto
    {
        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }

        [XmlElement("ProjectName")]
        public string ProjectName { get; set; }

        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public ProjectTaskExportDto[] Tasks { get; set; }
    }
}
