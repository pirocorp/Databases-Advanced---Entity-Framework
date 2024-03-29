﻿namespace Stations.DataProcessor.Dto.Import
{
    using System.ComponentModel.DataAnnotations;

    public class TrainDto
    {
        [Required]
        [MaxLength(10)] //Unique
        public string TrainNumber { get; set; }

        public string Type { get; set; }

        public SeatDto[] Seats { get; set; } = new SeatDto[0];
    }
}