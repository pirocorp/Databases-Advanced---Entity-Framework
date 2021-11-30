namespace VaporStore.Common
{
    public static class ValidationConstants
    {
        public const string DateFormat = "yyyy-MM-dd";

        public const string DateTimeFormat = "dd/MM/yyyy HH:mm";

        public static class User
        {
            public const int UsernameMinLength = 3;

            public const int UsernameMaxLength = 20;

            public const int AgeMinValue = 3;

            public const int AgeMaxValue = 103;
        }

        public static class Card
        {
            public const int CvcLength = 3;
        }

        public static class Game
        {
            public const double PriceMinValue = 0;
        }
    }
}
