namespace P05_FootballTeamGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Team
    {
        private readonly string name;
        private readonly Dictionary<string, Player> players;

        public Team(string name)
        {
            this.name = name;
            this.players = new Dictionary<string, Player>();
        }

        public string Name => this.name;

        public int Rating()
        {
            var result = 0;

            if (this.players.Count > 0)
            {
                result = (int) Math.Round(this.players.Average(x => x.Value.SkillLevel));
            }

            return result;
        }

        public void AddPlayer(Player player)
        {
            this.players.Add(player.Name, player);
        }

        public void RemovePlayer(string playerName)
        {
            if (!this.players.ContainsKey(playerName))
            {
                throw new ArgumentException($"Player {playerName} is not in {this.Name} team.");
            }

            this.players.Remove(playerName);
        }
    }
}