namespace BusTicketsSystem.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Interfaces;
    using Models;
    using Services.Interfaces;

    public class BuyTicketCommand : ICommand
    {
        private readonly ICustomerService customerService;
        private readonly IBankAccountService bankAccountService;
        private readonly ITicketService ticketService;
        private readonly ITripService tripService;

        public BuyTicketCommand(ICustomerService customerService, IBankAccountService bankAccountService, 
            ITicketService ticketService, ITripService tripService)
        {
            this.customerService = customerService;
            this.bankAccountService = bankAccountService;
            this.ticketService = ticketService;
            this.tripService = tripService;
        }

        //BuyTicket {customer ID} {Trip ID} {Price} {Seat}
        public string Execute(string[] args)
        {
            var customerId = int.Parse(args[0]);
            var tripId = int.Parse(args[1]);
            var price = decimal.Parse(args[2]);
            var seat = args[3];

            var customer = this.customerService.ById<Customer>(customerId);

            if (customer == null)
            {
                return "Customer doesn't exists!";
            }

            if (price < 0)
            {
                return "Price must be positive!";
            }

            var bankAccount = this.bankAccountService
                .FindBy<BankAccount>(b => b.Customer.Id == customerId && 
                                          b.Balance >= price)
                .FirstOrDefault();

            if (bankAccount == null)
            {
                return "No sufficient balance in any account!";
            }

            var trip = this.tripService.ById<Trip>(tripId);

            if (trip == null)
            {
                return "No such trip exists!";
            }

            var seatIsTaken = this.ticketService
                .FindBy<Ticket>(t => t.TripId == tripId && 
                                     string.Equals(t.Seat, seat, StringComparison.InvariantCultureIgnoreCase))
                .Any();

            if (seatIsTaken)
            {
                return "Seat is already taken";
            }

            this.bankAccountService.Withdraw(bankAccount.Id, price);
            var ticket = this.ticketService.Create(price, seat, customerId, tripId);

            return $"Customer {customer.FirstName} {customer.LastName} bought ticket for trip {trip.Id}: for {price} on seat {seat}";
        }
    }
}