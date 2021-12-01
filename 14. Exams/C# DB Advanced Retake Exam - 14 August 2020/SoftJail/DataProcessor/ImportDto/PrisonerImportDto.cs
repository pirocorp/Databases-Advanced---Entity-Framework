namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ValidationConstants.Prisoner;

    public class PrisonerImportDto
    {
        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        public string FullName { get; set; }

        [Required]
        [RegularExpression("^The [A-Z][a-z]*$")]
        public string Nickname { get; set; }

        [Range(AgeMin, AgeMax)]
        public int Age { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }

        public string ReleaseDate { get; set; }

        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public MailImportDto[] Mails { get; set; }
    }
}
