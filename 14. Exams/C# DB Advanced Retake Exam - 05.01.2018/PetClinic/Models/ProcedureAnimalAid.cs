namespace PetClinic.Models
{
    public class ProcedureAnimalAid
    {
        public int ProcedureId { get; set; }
        public virtual Procedure Procedure { get; set; }

        public int AnimalAidId { get; set; }
        public virtual AnimalAid AnimalAid { get; set; }
    }
}