//Defining the relationship fully at both ends with the foreign key property and navigational property in the dependent entity
//and Collection in the principal entity creates a one-to-many relationship.
//----------------------------------------------------------------------------------------------------------------------------------------------
//In EF Core, a one-to-one relationship requires a reference navigation property at both sides.
//On principle end declaring only navigational property
//On dependent end declaring navigational property and Foreign Key				
//EF Core creates a unique index on the NotNull foreign key column in the dependent table.
//This ensures that the value of the foreign key column must be unique in the dependent table, 
//which is necessary of a one-to-one relationship. 
//Unique constraint is supported in Entity Framework Core but not in EF 6
//------------------------------------------------------------------------------------------------------------------------------------------------
//ChangeTracker in Entity Framework Core
//Unchanged State
//First, all the entities retrieved using direct SQL query or LINQ-to-Entities queries will have the Unchanged state.
//Added State
//All the new entities without key property value, added in the DbContext using the Add() or Update() method will be marked as Added.
//Modified State
//If the value of any property of an entity is changed in the scope of the DbContext, then it will be marked as Modified state.
//Deleted State
//If any entity is removed from the DbContext using the DbContext.Remove or DbSet.Remove method, then it will be marked as Deleted.
//Detached State
//All the entities which were created or retrieved out of the scope of the current DbContext instance, will have the Detached state.
//They are also called disconnected entities and are not being tracked by an existing DbContext instance.
//------------------------------------------------------------------------------------------------------------------------------------------------
//Entity Framework Core provides the following different methods, which not only attach an entity to a context, but also change the EntityState of 
//each entity in a disconnected entity graph: 
//Attach()
//The DbContext.Attach() and DbSet.Attach() methods attach the specified disconnected entity graph and start tracking it. They return an instance
//of EntityEntry, which is used to assign the appropriate EntityState. 
//Entry()
//attaches an entity to a context and applies the specified EntityState to the root entity, irrespective of whether it contains a Key property value or not.
//It ignores all the child entities in a graph and does not attach or set their EntityState. 
//Add()
//The DbContext.Add and DbSet.Add methods attach an entity graph to a context and set Added EntityState to a root and child entities, irrespective of whether 
//they have key values or not.
//Update()
//The DbContext.Update() and DbSet.Update() methods attach an entity graph to a context and set the EntityState of each entity
//in a graph depending on whether it contains a key property value or not.
//Remove()
//The DbContext.Remove() and DbSet.Remove() methods set the Deleted EntityState to the root entity. 
//Thus, be careful while using the above methods in EF Core. 
namespace P01_HospitalDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class Demo
    {
        private HospitalContext db;

        public Demo()
        {
            this.db = new HospitalContext();
        }

        public void Run()
        {
            using (this.db)
            {
                //this.EagerLoading();
                //this.ThenInclude();
                //this.ProjectionMultipleLevels();
                //this.ExplicitLoading();
                //this.InsertData();
                //this.UpdatingData();
                //this.DeletingData();
                //this.RawSql();
            }
            //Working with Disconnected Entity Graph in Entity Framework Core
            //this.Attach();
        }

        private void RawSql()
        {
            //FromSql Limitations

            //1. SQL queries must return entities of the same type as DbSet<T> type.e.g.the specified query cannot return the
            //Course entities if FromSql is used after Students.Returning ad-hoc types from FromSql() method is in the backlog.
            //2. The SQL query must return all the columns of the table.e.g.
            //db.Patients.FromSql("Select FirstName, LastName from Patients).ToList() will throw an exception.
            //3. The SQL query cannot include JOIN queries to get related data.Use Include method to load related entities after FromSql() method.

            var name = "Stefan";
            var patients = this.db.Patients
                .FromSql($"SELECT * FROM Patients WHERE FirstName = {name}")
                .ToList();
        }

        private void Attach()
        {
            //The Attach() method sets Added EntityState to the root entity(in this case Patient)
            //irrespective of whether it contains the Key value or not. If a child entity contains the key value,
            //then it will be marked as Unchanged, otherwise it will be marked as Added.
            var stud = new Patient()
            { //Root entity (empty key)
                FirstName = "Bill",
                LastName = "Gates",
                Address = "Laencaster, Prospect Street 11",
                Email = "kaloyanpetrov885@mail.bg",
                Diagnoses = new List<Diagnose>() //Child entity (empty value)
                {
                    new Diagnose()
                    {
                        Comments = "Disconnected Insert",
                        Name = "Attach"
                    }
                }
            };

            this.db = new HospitalContext();
            this.db.Attach(stud).State = EntityState.Added;

            DisplayStates(this.db.ChangeTracker.Entries());
        }

        private static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State.ToString()}");
            }
        }

        private void DeletingData()
        {
            var visitation = this.db.Visitations.FirstOrDefault(v => v.VisitationId == 299);

            this.db.Visitations.Remove(visitation); //EF6 Original Method

            this.db.Remove(visitation); //EF Core New Method

            this.db.SaveChanges();
        }

        private void UpdatingData()
        {
            var doctor = this.db.Doctors.FirstOrDefault(d => d.Name == "New Stefan Kara");
            doctor.Name = "Updated Name";
            this.db.SaveChanges();
        }

        private void InsertData()
        {
            var doctor = new Doctor()
            {
                Name = "New Stefan Kara",
                Specialty = "ER"
            };

            this.db.Add(doctor); //EF Core new Method

            this.db.Doctors.Add(doctor); //EF6 Original Method

            this.db.SaveChanges();
        }

        private void ExplicitLoading()
        {
            var patient = this.db.Patients.First();

            this.db.Entry(patient).Collection(p => p.Diagnoses).Load(); //Loads Diagnoses
            this.db.Entry(patient).Collection(p => p.Visitations).Load(); //Visitations
        }

        private void ProjectionMultipleLevels()
        {
            var patient = this.db.Patients
                .Where(p => p.PatientId == 1)
                .Select(p => new
                {
                    p.FirstName,
                    p.LastName,
                    Visitations = p.Visitations.Select(v => new
                    {
                        v.Doctor,
                        v.Comments,
                        v.Date
                    }).ToList(),
                    Diagnoses = p.Diagnoses.Select(d => new
                    {
                        d.Name,
                        d.Comments
                    }).ToList()
                })
                .ToList();
        }

        private void ThenInclude()
        {
            //Eager Loading - multiple levels of related entities
            var patients = this.db.Patients
                .Include(p => p.Visitations)
                    .ThenInclude(v => v.Doctor)
                .ToList();
        }

        private void EagerLoading()
        {
            //Eager Loading - when using Include() and ThenInclude() extension methods
            var patient = this.db.Patients
                .Include(p => p.Diagnoses)
                .Include(p => p.Prescriptions)
                .Include(p => p.Visitations)
                .First();
        }
    }
}