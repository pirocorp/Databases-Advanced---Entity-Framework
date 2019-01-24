namespace P01_StudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Student
    {
        private ICollection<StudentCourse> courseEnrollments;
        private ICollection<Homework> homeworkSubmissions;

        public Student()
        {
            this.courseEnrollments = new HashSet<StudentCourse>();
            this.homeworkSubmissions = new HashSet<Homework>();
        }

        public int StudentId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; }

        public virtual ICollection<StudentCourse> CourseEnrollments
        {
            get => this.courseEnrollments;
            set => this.courseEnrollments = value;
        }

        public virtual ICollection<Homework> HomeworkSubmissions
        {
            get => this.homeworkSubmissions;
            set => this.homeworkSubmissions = value;
        }
    }
}