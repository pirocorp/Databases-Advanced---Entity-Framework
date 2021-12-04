namespace Theatre.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    using static Data.ValidationConstants.Theatre;

    public class TheatreImportDto
    {
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Range(NumberOfHallsMinValue, NumberOfHallsMaxValue)]
        public sbyte NumberOfHalls { get; set; }

        [StringLength(DirectorMaxLength, MinimumLength = DirectorMinLength)]
        public string Director { get; set; }

        public TicketImportDto[] Tickets { get; set; }
    }
}
