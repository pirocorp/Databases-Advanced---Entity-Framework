namespace Theatre.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    using static Data.ValidationConstants.Ticket;

    public class TicketImportDto
    {
        public decimal Price { get; set; }

        [Range(RowNumberMinValue, RowNumberMaxValue)]
        public sbyte RowNumber { get; set; }

        public int PlayId { get; set; }
    }
}
