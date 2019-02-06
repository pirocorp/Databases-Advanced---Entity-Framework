namespace BusTicketsSystem.Models
{
    using System;
    using System.Collections.Generic;
    using Enums;

    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public int HomeTownId { get; set; }
        public virtual Town HomeTown { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}