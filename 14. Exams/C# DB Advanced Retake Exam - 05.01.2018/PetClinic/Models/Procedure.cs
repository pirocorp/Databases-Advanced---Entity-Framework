namespace PetClinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Procedure
    {
        private ICollection<ProcedureAnimalAid> procedureAnimalAids;

        public Procedure()
        {
            this.procedureAnimalAids = new HashSet<ProcedureAnimalAid>();
        }

        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }

        public int VetId { get; set; }
        public virtual Vet Vet { get; set; }

        public decimal Cost => this.ProcedureAnimalAids.Sum(pa => pa.AnimalAid.Price);

        public virtual ICollection<ProcedureAnimalAid> ProcedureAnimalAids
        {
            get => this.procedureAnimalAids;
            set => this.procedureAnimalAids = value;
        }
    }
}