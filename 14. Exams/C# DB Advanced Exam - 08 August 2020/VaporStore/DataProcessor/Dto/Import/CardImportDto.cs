namespace VaporStore.DataProcessor.Dto.Import
{
    using System.ComponentModel.DataAnnotations;

    public class CardImportDto
    {
        [Required]
        [RegularExpression("[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}")]
        public string Number { get; set; }

        [Required]
        [RegularExpression("[0-9]{3}")]
        public string CVC { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
