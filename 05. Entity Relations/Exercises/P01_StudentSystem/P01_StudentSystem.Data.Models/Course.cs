namespace P01_StudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Course
    {
        private ICollection<StudentCourse> studentsEnrolled;
        private ICollection<Resource> resources;
        private ICollection<Homework> homeworkSubmissions;

        public Course()
        {
            this.studentsEnrolled = new HashSet<StudentCourse>();
            this.resources = new HashSet<Resource>();
            this.homeworkSubmissions = new HashSet<Homework>();
        }

        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<StudentCourse> StudentsEnrolled
        {
            get => this.studentsEnrolled;
            set => this.studentsEnrolled = value;
        }

        public virtual ICollection<Resource> Resources
        {
            get => this.resources;
            set => this.resources = value;
        }

        public virtual ICollection<Homework> HomeworkSubmissions
        {
            get => this.homeworkSubmissions;
            set => this.homeworkSubmissions = value;
        }
    }
}