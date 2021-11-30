namespace VaporStore.DataProcessor.Dto.Import
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ValidationConstants.User;

    public class UserImportDto
    {
        [Required]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength)]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^[A-Z][a-z]+ [A-Z][a-z]+$")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(AgeMinValue, AgeMaxValue)]
        public int Age { get; set; }

        [Required]
        public CardImportDto[] Cards { get; set; }
    }
}
