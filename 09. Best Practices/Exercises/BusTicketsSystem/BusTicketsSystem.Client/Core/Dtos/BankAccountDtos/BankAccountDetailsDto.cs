namespace BusTicketsSystem.Client.Core.Dtos.BankAccountDtos
{
    public class BankAccountDetailsDto
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public int CustomerId { get; set; }

        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }
    }
}