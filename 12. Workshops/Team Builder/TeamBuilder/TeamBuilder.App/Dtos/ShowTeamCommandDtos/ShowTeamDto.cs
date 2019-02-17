namespace TeamBuilder.App.Dtos.ShowTeamCommandDtos
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ShowTeamDto
    {
        public string Name { get; set; }

        public string Acronym { get; set; }

        public ShowTeamUserDto[] Users { get; set; }
    }
}