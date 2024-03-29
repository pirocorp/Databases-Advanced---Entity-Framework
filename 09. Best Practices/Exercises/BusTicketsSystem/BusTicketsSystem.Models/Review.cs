﻿namespace BusTicketsSystem.Models
{
    using System;

    public class Review
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public double Grade { get; set; }

        public DateTime DateTimeOfPublishing { get; set; }

        public int BusStationId { get; set; }
        public virtual BusStation BusStation { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}