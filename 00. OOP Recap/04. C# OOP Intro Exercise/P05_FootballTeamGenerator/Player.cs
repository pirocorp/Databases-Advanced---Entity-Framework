namespace P05_FootballTeamGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Player
    {
        private readonly string name;
        private readonly Dictionary<string, int> stats;

        private Player(string name, Dictionary<string, int> stats)
        {
            this.name = name;
            this.stats = stats;
        }

        public string Name => this.name;

        public double SkillLevel => this.stats.Average(x => x.Value);

        public static Player Parse(string[] inputArgs)
        {
            var defaultStats = new[]{ "Endurance", "Sprint", "Dribble", "Passing", "Shooting" };
            var name = inputArgs[0];
            var stats = new Dictionary<string, int>();

            for (var i = 1; i <= 5; i++)
            {
                var statName = defaultStats[i - 1];
                var statValue = int.Parse(inputArgs[i]);

                if (statValue > 100 || statValue < 0)
                {
                    throw new ArgumentException($"{statName} should be between 0 and 100.");
                }

                stats.Add(statName, statValue);
            }

            var player = new Player(name, stats);
            return player;
        }
    }
}