namespace VaporStore.DataProcessor.Dto.Import
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Common.ValidationConstants.Game;

    public class GameImportDto
    {
        [Required]
        public string Name { get; set; }

        [Range(PriceMinValue, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string ReleaseDate { get; set; }

        [Required]
        public string Developer { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string[] Tags { get; set; }  
    }
}
