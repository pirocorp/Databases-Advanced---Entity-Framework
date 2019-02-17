﻿namespace TeamBuilder.App.Dtos.ShowEventCommandDtos
{
    using System;

    public class ShowEventDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string[] Teams { get; set; }
    }
}