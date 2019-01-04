namespace P06_OnlineRadioDatabase.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InvalidSongMinutesException : InvalidSongLengthException
    {
        public InvalidSongMinutesException() 
            : base("Song minutes should be between 0 and 14.")
        {
        }
    }
}