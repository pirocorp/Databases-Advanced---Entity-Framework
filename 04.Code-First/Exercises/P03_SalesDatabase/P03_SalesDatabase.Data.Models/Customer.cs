﻿namespace P03_SalesDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Customer
    {
        private ICollection<Sale> sales;

        public Customer()
        {
            this.sales = new HashSet<Sale>();
        }

        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string CreditCardNumber { get; set; }

        public ICollection<Sale> Sales
        {
            get => this.sales;
            set => this.sales = value;
        }
    }
}