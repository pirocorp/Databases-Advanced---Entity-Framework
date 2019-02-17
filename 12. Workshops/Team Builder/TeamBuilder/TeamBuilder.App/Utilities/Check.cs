namespace TeamBuilder.App.Utilities
{
    using System;
    using System.Linq;

    public static class Check
    {
        public static void CheckLength(int expectedLength, string[] array)
        {
            if (expectedLength != array.Length)
            {
                throw new FormatException(Constants.ErrorMessages.InvalidArgumentsCount);
            }
        }

        public static void CheckName(string name)
        {
            if (!name.All(char.IsLetter))
            {
                throw new FormatException(string.Format(Constants.ErrorMessages.NameContainsNotLetters, name));
            }

            if (name.Length > Constants.MaxNameLength)
            {
                throw new FormatException(Constants.ErrorMessages.IncorrectNameLength);
            }
        }
    }
}