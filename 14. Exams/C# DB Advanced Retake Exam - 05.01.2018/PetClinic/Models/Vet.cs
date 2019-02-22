namespace PetClinic.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Vet
    {
        private ICollection<Procedure> procedures;

        public Vet()
        {
            this.procedures = new HashSet<Procedure>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Profession { get; set; }
        
        [Required]
        [Range(22, 65)]
        public int Age { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Procedure> Procedures
        {
            get => this.procedures;
            set => this.procedures = value;
        }
    }
}