namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ValidationConstants.Department;

    public class DepartmentImportDto
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        public CellImportDto[] Cells { get; set; }
    }
}
