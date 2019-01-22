//Defining the relationship fully at both ends with the foreign key property and navigational property in the dependent entity
//and Collection in the principal entity creates a one-to-many relationship.

//In EF Core, a one-to-one relationship requires a reference navigation property at both sides.
//On principle end declaring only navigational property
//On dependent end declaring navigational property and Foreign Key				
//EF Core creates a unique index on the NotNull foreign key column in the dependent table.
//This ensures that the value of the foreign key column must be unique in the dependent table, 
//which is necessary of a one-to-one relationship. 
//Unique constraint is supported in Entity Framework Core but not in EF 6
namespace P01_HospitalDatabase.Data.Models
{
    using System;

    public class Visitation
    {
        public int VisitationId { get; set; }

        public DateTime Date { get; set; }

        public string Comments { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}