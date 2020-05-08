namespace TeisterMask.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Task")]
    public class ProjectTaskExportDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Label")]
        public string Label { get; set; }
    }
}
