namespace P01_HospitalDatabase
{
    using System;
    using System.Linq;
    using Data;

    public class Startup
    {
        public static void Main()
        {
            using (var context = new HospitalContext())
            {
                var medicaments = context.Medicaments.ToList();

                foreach (var medicament in medicaments)
                {
                    Console.WriteLine($"{medicament.MedicamentId}: {medicament.Name}");
                }
            }
        }
    }
}