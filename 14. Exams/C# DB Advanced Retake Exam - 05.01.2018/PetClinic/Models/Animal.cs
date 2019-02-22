namespace PetClinic.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Animal
    {
        private ICollection<Procedure> procedures;

        public Animal()
        {
            this.procedures = new HashSet<Procedure>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Type { get; set; }

        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        public string PassportSerialNumber { get; set; }
        public virtual Passport Passport { get; set; }

        public virtual ICollection<Procedure> Procedures
        {
            get => this.procedures;
            set => this.procedures = value;
        }
    }
}
