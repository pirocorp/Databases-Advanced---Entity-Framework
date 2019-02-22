namespace PetClinic.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Passport
    {
        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string SerialNumber { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string OwnerName { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 10)]
        public string OwnerPhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public virtual Animal Animal { get; set; }
    }
}