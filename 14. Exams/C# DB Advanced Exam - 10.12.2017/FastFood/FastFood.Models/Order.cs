﻿namespace FastFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Enums;

    public class Order
    {
        private ICollection<OrderItem> orderItems;

        public Order()
        {
            this.orderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }

        [Required]
        public string Customer { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public OrderType Type { get; set; }

        [NotMapped]
        public decimal TotalPrice => this.OrderItems.Sum(x => x.Item.Price * x.Quantity);

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual ICollection<OrderItem> OrderItems
        {
            get => this.orderItems;
            set => this.orderItems = value;
        }
    }
}