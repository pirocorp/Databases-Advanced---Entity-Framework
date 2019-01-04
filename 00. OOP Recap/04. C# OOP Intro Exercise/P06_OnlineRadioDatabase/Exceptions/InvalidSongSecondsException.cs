namespace P06_OnlineRadioDatabase.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InvalidSongSecondsException : InvalidSongLengthException
    {
        public InvalidSongSecondsException() 
            : base("Song seconds should be between 0 and 59.")
        {
        }
    }
}