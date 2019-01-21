namespace StudentSystem.Data.Migrations
{
    using StudentSystem.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "StudentSystem.Data.StudentSystemDbContext";
        }

        protected override void Seed(StudentSystemDbContext context)
        {
            this.SeedCourses(context);
            this.SeedStudents(context);
        }

        private void SeedStudents(StudentSystemDbContext context)
        {
            if (context.Students.Any())
            {
                return;
            }

            context.Students.Add(new Student
            {
                FirstName = "Ivaylo",
                LastName = "Kenov",
                Level = 0,
            });

            context.Students.Add(new Student
            {
                FirstName = "Teodor",
                LastName = "Kurtev",
                Level = 2
            });

            context.Students.Add(new Student
            {
                FirstName = "Vladimir",
                LastName = "Georgiev",
                Level = 1
            });

            context.Students.Add(new Student
            {
                FirstName = "Svetlin",
                LastName = "Nakov",
                Level = 9999
            });
        }

        private void SeedCourses(StudentSystemDbContext context)
        {
            if (context.Courses.Any())
            {
                return;
            }

            context.Courses.Add(new Course
            {
                Name = "Seeded course",
                Description = "Initial course for testing"
            });
        }
    }
}
