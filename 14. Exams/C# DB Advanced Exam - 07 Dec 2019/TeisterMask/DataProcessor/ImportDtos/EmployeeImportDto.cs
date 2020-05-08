namespace TeisterMask.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;

    public class EmployeeImportDto
    {
        [Required]
        [RegularExpression("[A-Za-z0-9]{3,40}")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("\\d{3}-\\d{3}-\\d{4}")]
        public string Phone { get; set; }

        public int[] Tasks { get; set; }
    }
}
