namespace Stations.DataProcessor.Dto.Import
{
    using System.ComponentModel.DataAnnotations;

    public class SeatDto
    {
        [Required]
        [MaxLength(30)]//Unique
        public string Name { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]//Unique
        public string Abbreviation { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int? Quantity { get; set; }
    }
}