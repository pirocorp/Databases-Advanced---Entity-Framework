namespace P06_OnlineRadioDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Exceptions;

    public class Engine
    {
        private readonly List<Song> songs;

        public Engine()
        {
            this.songs = new List<Song>();
        }

        public void Run()
        {
            var n = int.Parse(Console.ReadLine());

            for (var i = 0; i < n; i++)
            {
                try
                {
                    this.ReadSong();
                    Console.WriteLine($"Song added.");
                }
                catch (InvalidSongException ise)
                {
                    Console.WriteLine(ise.Message);
                }
            }

            var totalMinutes = this.songs.Sum(s => s.Minutes);
            var totalSeconds = this.songs.Sum(s => s.Seconds);

            var ts = new TimeSpan(0, totalMinutes, totalSeconds);

            Console.WriteLine($"Songs added: {this.songs.Count}");
            Console.WriteLine($"Playlist length: {ts.Hours}h {ts.Minutes}m {ts.Seconds}s");
        }

        private void ReadSong()
        {
            var songTokens = Console.ReadLine().Split(new []{";"}, StringSplitOptions.RemoveEmptyEntries);

            if (songTokens.Length != 3)
            {
                throw new InvalidSongException();
            }

            var artistName = songTokens[0];
            var songName = songTokens[1];

            var timeTokens = songTokens[2].Split(new[] {":"}, StringSplitOptions.None);

            if (timeTokens.Length != 2)
            {
                throw new InvalidSongLengthException();
            }

            var minutes = int.Parse(timeTokens[0]);
            var seconds = int.Parse(timeTokens[1]);

            var newSong = new Song(artistName, songName, minutes, seconds);
            this.songs.Add(newSong);
        }
    }
}