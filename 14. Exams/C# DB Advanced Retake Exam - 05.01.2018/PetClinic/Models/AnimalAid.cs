namespace PetClinic.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AnimalAid
    {
        private ICollection<ProcedureAnimalAid> animalAidProcedures;

        public AnimalAid()
        {
            this.animalAidProcedures = new HashSet<ProcedureAnimalAid>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public virtual ICollection<ProcedureAnimalAid> AnimalAidProcedures
        {
            get => this.animalAidProcedures;
            set => this.animalAidProcedures = value;
        }
    }
}