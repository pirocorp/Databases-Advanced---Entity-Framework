namespace Stations.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Train
    {
        private ICollection<TrainSeat> trainSeats;
        private ICollection<Trip> trips;

        public Train()
        {
            this.trainSeats = new HashSet<TrainSeat>();
            this.trips = new HashSet<Trip>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(10)] //Unique
        public string TrainNumber { get; set; }

        public TrainType? Type { get; set; }

        public virtual ICollection<TrainSeat> TrainSeats
        {
            get => this.trainSeats;
            set => this.trainSeats = value;
        }

        public virtual ICollection<Trip> Trips
        {
            get => this.trips;
            set => this.trips = value;
        }
    }
}