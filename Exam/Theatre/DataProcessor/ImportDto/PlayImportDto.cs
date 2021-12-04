namespace Theatre.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static Data.ValidationConstants.Play;

    [XmlType("Play")]
    public class PlayImportDto
    {
        [XmlElement("Title")]
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [XmlElement("Duration")]
        [Required]
        public string Duration { get; set; }

        [XmlElement("Rating")]
        [Required]
        [Range(RatingMinValue, RatingMaxValue)]
        public float Rating { get; set; }

        [XmlElement("Genre")]
        [Required]
        public string Genre { get; set; }

        [XmlElement("Description")]
        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [XmlElement("Screenwriter")]
        [Required]
        [StringLength(ScreenwriterMaxLength, MinimumLength = ScreenwriterMinLength)]
        public string Screenwriter { get; set; }
    }
}
