//One to Many relation in EF Code first Models is described as
//In model of one end of relation we have navigation property (ICollection) to many end
//In model of many end of relation we have navigation property (Class) to one end

//Many to Many relation in EF Code first Models is described as
//In model of each end of relation we have navigation property(ICollection) to mapping class

namespace P01_HospitalDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Patient
    {
        private ICollection<PatientMedicament> prescriptions;
        private ICollection<Diagnose> diagnoses;
        private ICollection<Visitation> visitations;

        public Patient()
        {
            this.prescriptions = new HashSet<PatientMedicament>();
            this.diagnoses = new HashSet<Diagnose>();
            this.visitations = new HashSet<Visitation>();
        }

        public int PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public bool HasInsurance { get; set; }

        public ICollection<PatientMedicament> Prescriptions
        {
            get => this.prescriptions;
            set => this.prescriptions = value;
        }

        public ICollection<Diagnose> Diagnoses
        {
            get => this.diagnoses;
            set => this.diagnoses = value;
        }

        public ICollection<Visitation> Visitations
        {
            get => this.visitations;
            set => this.visitations = value;
        }
    }
}