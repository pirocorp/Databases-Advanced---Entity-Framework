namespace Stations.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class CustomerCard
    {
        private ICollection<Ticket> boughtTickets;

        public CustomerCard()
        {
            this.boughtTickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        public CardType Type { get; set; }

        public virtual ICollection<Ticket> BoughtTickets
        {
            get => this.boughtTickets;
            set => this.boughtTickets = value;
        }
    }
}