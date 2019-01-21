namespace P01_HospitalDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Medicament
    {
        private ICollection<PatientMedicament> prescriptions;

        public Medicament()
        {
            this.prescriptions = new HashSet<PatientMedicament>();
        }

        public int MedicamentId { get; set; }

        public string Name { get; set; }

        public ICollection<PatientMedicament> Prescriptions
        {
            get => this.prescriptions;
            set => this.prescriptions = value;
        }
    }
}