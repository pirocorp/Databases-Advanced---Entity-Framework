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
                var patients = context.Patients.ToList();

                foreach (var patient in patients)
                {
                    Console.WriteLine($"{patient.FirstName} {patient.LastName}");
                }
            }
        }
    }
}