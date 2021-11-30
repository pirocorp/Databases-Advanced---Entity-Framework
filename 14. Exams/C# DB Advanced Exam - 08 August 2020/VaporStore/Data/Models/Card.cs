namespace VaporStore.Data.Models
{
    using System.Collections.Generic;
    using Enums;

    public class Card
    {
        public Card()
        {
            this.Purchases = new List<Purchase>();
        }

        public int Id { get; set; }

        public string Number { get; set; }

        public string Cvc { get; set; }

        public CardType Type { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
}
