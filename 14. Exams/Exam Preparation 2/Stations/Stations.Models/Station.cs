namespace Stations.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Station
    {
        private ICollection<Trip> tripsTo;
        private ICollection<Trip> tripsFrom;

        public Station()
        {
            this.tripsTo = new HashSet<Trip>();
            this.tripsFrom = new HashSet<Trip>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]//Unique
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Town { get; set; }

        public virtual ICollection<Trip> TripsTo
        {
            get => this.tripsTo;
            set => this.tripsTo = value;
        }

        public virtual ICollection<Trip> TripsFrom
        {
            get => this.tripsFrom;
            set => this.tripsFrom = value;
        }
    }
}
