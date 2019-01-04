namespace P05_FootballTeamGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            var teams = new Dictionary<string, Team>();
            var inputLine = string.Empty;

            while ((inputLine = Console.ReadLine()) != "END")
            {
                var inputTokens = inputLine.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);

                var command = inputTokens[0];
                var teamName = inputTokens[1];

                switch (command)
                {
                    case "Team":
                        CreateTeam(teamName, teams);
                        break;
                    case "Add":
                        Add(teamName, teams, inputTokens.Skip(2).ToArray());
                        break;
                    case "Remove":
                        Remove(teamName, teams, inputTokens.Skip(2).ToArray());
                        break;
                    case "Rating":
                        Rating(teamName, teams);
                        break;
                }
            }
        }

        private static void Rating(string teamName, Dictionary<string, Team> teams)
        {
            try
            {
                TeamExists(teamName, teams);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return;
            }

            Console.WriteLine($"{teamName} - {teams[teamName].Rating()}");
        }

        private static void Remove(string teamName, Dictionary<string, Team> teams, string[] inputTokens)
        {
            var playerName = inputTokens[0];

            try
            {
                TeamExists(teamName, teams);
                teams[teamName].RemovePlayer(playerName);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return;
            }
        }

        private static void Add(string teamName, Dictionary<string, Team> teams, string[] inputTokens)
        {
            Player player = null;

            try
            {
                TeamExists(teamName, teams);
                player = Player.Parse(inputTokens);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return;
            }

            teams[teamName].AddPlayer(player);
        }

        private static void TeamExists(string teamName, Dictionary<string, Team> teams)
        {
            if (!teams.ContainsKey(teamName))
            {
                throw new ArgumentException($"Team {teamName} does not exist.");
            }
        }

        private static void CreateTeam(string teamName, Dictionary<string, Team> teams)
        {
            var newTeam = new Team(teamName);
            teams.Add(teamName, newTeam);
        }
    }
}
