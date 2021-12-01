namespace SoftJail.Data.Models
{
    public class Mail
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Sender { get; set; }

        public string Address { get; set; }

        public int PrisonerId { get; set; }

        public Prisoner Prisoner { get; set; }
    }
}
