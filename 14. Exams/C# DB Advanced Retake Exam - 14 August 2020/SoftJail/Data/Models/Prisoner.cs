﻿namespace SoftJail.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Prisoner
    {
        public Prisoner()
        {
            this.Mails = new List<Mail>();
            this.PrisonerOfficers = new List<OfficerPrisoner>();
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public string Nickname { get; set; }

        public int Age { get; set; }

        public DateTime IncarcerationDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public Cell Cell { get; set; }

        public ICollection<Mail> Mails { get; set; }

        public ICollection<OfficerPrisoner> PrisonerOfficers  { get; set; }
    }
}