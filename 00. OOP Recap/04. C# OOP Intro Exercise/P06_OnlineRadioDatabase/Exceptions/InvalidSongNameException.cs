namespace P06_OnlineRadioDatabase.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InvalidSongNameException : InvalidSongException
    {
        public InvalidSongNameException() 
            : base("Song name should be between 3 and 30 symbols.")
        {
        }
    }
}