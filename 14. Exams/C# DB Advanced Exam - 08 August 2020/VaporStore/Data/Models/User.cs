namespace VaporStore.Data.Models
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Cards = new List<Card>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
