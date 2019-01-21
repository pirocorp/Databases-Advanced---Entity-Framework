namespace HospitalDatabaseInitializer
{
    using Generators;
    using P01_HospitalDatabase.Data;

    //using System.IO;

    public class DatabaseInitializer
    {
        public static void ResetDatabase()
        {
            using (var context = new HospitalContext())
            {
                InitialSeed(context);
            }
        }

        public static void InitialSeed(HospitalContext context)
        {
            SeedMedicaments(context);

            SeedPatients(context, 200);

            SeedPrescriptions(context);
        }

        private static void SeedMedicaments(HospitalContext context)
        {
            MedicamentGenerator.InitialMedicamentSeed(context);
        }

        public static void SeedPatients(HospitalContext context, int count)
        {
            for (var i = 0; i < count; i++)
            {
                context.Patients.Add(PatientGenerator.NewPatient(context));
            }

            context.SaveChanges();
        }

        private static void SeedPrescriptions(HospitalContext context)
        {
            PrescriptionGenerator.InitialPrescriptionSeed(context);
        }
    }
}
