namespace P06_OnlineRadioDatabase.Exceptions
{
    using System;

    public class InvalidSongException : Exception
    {
        public InvalidSongException(string message = "Invalid song.")
            :base(message)
        {
            
        }
    }
}