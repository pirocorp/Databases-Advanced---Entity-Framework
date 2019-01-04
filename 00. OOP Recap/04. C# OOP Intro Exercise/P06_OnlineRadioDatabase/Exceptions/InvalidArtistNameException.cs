namespace P06_OnlineRadioDatabase.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InvalidArtistNameException : InvalidSongException
    {
        public InvalidArtistNameException() 
            : base("Artist name should be between 3 and 20 symbols.")
        {
        }
    }
}