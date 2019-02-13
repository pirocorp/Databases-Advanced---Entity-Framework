﻿namespace CarDealer.Models
{
    using System.Collections.Generic;

    public class Supplier
    {
        private ICollection<Part> parts;

        public Supplier()
        {
            this.parts = new HashSet<Part>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public virtual ICollection<Part> Parts
        {
            get => this.parts;
            set => this.parts = value;
        }
    }
}