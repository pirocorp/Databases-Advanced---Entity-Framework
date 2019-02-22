namespace Stations.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SeatingClass
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]//Unique
        public string Name { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]//Unique
        public string Abbreviation { get; set; }
    }
}